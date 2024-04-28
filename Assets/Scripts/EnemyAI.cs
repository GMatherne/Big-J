using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Game Objects")]
    public NavMeshAgent agent;

    public Transform player;

    public GameObject projectile;

    [Header("Visual")]
    public GameObject body;
    private Renderer enemyRenderer;
    private Color32 enemyColor;
    public bool changeColorWhenHit;
    public bool changeColor;

    [Header("Layers")]
    public LayerMask ground;
    public LayerMask playerLayer;

    [Header("Attacking")]
    public Vector3 searchPoint;
    public Transform attackPoint;
    private bool searchPointSet;
    public float searchRange;

    public float fireRate;
    private bool alreadyAttacked;

    public float sightRange;
    public float attackRange;

    public float projectilePower;

    private bool playerInSightRange;
    private bool playerInAttackRange;

    [Header("Stats")]
    public float totalHealth;
    private float health;
    public bool moves;
    public bool attacks;
    public bool lookAtPlayer;

    [Header("Stages")]
    public bool stages;
    public Material stage1;
    public Material stage2;
    public Material stage3;
    private float threshold1;
    private float threshold2;
    private float threshold3;
    public float bulletHealth1;
    public float bulletHealth2;
    public float bulletHealth3;
    public float fireRate1;
    public float fireRate2;
    public float fireRate3;
    public float fireRateChangeDelay;
    private Bullet bullet;

    private void Awake(){

        bullet = projectile.GetComponent<Bullet>();

        health = totalHealth;

        threshold1 = health / 3f * 2f;
        threshold2 = health / 3f;
        threshold3 = 0;

        player = GameObject.Find("Player").transform;

        if(moves){
            agent = GetComponent<NavMeshAgent>();
        }

        enemyRenderer = body.GetComponent<Renderer>();
        enemyColor = enemyRenderer.material.color;

        if(stages){
            enemyRenderer.material = stage1;
            bullet.health = bulletHealth1;
            fireRate = fireRate1;
        }
    }

    private void Update(){

        if(!(moves || attacks)){
            return;
        }

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if(!playerInSightRange && !playerInAttackRange && moves){
            Searching();
        }else if(playerInSightRange && !playerInAttackRange && moves){
            ChasePlayer();
        }else if(attacks && playerInAttackRange){
            AttackPlayer();
        }
    }

    private void Searching(){

        if(!searchPointSet){
            SearchPoint();
        }

        if(searchPointSet){
            agent.SetDestination(searchPoint);
        }

        Vector3 distanceTosearchPoint = transform.position - searchPoint;

        if(distanceTosearchPoint.magnitude < 1f){
            searchPointSet = false;
        }
    }

    private void SearchPoint(){

        float randomZ = Random.Range(-searchRange, searchRange);
        float randomX = Random.Range(-searchRange, searchRange);

        searchPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
   
        if(Physics.Raycast(searchPoint, -transform.up, 2f, ground)){
            searchPointSet = true;
        }
    }

    private void ChasePlayer(){
        agent.SetDestination(player.position);
    }

    private void AttackPlayer(){

        if(moves){
            agent.SetDestination(transform.position);
        }

        if(lookAtPlayer){
            transform.LookAt(player);
        }else{
            attackPoint.LookAt(player);
        }

        if(!alreadyAttacked){

            Rigidbody rigidBody = Instantiate(projectile, attackPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
           
            rigidBody.AddForce(attackPoint.forward * projectilePower, ForceMode.Impulse);

            alreadyAttacked = true;

            Invoke(nameof(ResetAttack), fireRate);
        }
    }

    private void ResetAttack(){
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage){

        health -= damage;

        if(stages){
            if(health >= threshold1){

                enemyRenderer.material = stage1;
                bullet.health = bulletHealth1;

                if(fireRate != fireRate1){
                    fireRate = fireRate1;
                }

            }else if(health >= threshold2){

                enemyRenderer.material = stage2;
                bullet.health = bulletHealth2;
                
                if(fireRate != fireRate2){
                    StartCoroutine(ChangeFireRate(fireRate2, fireRateChangeDelay));
                }

            }else if(health >= threshold3){

                enemyRenderer.material = stage3;
                bullet.health = bulletHealth3;
                
                if(fireRate != fireRate3){
                    StartCoroutine(ChangeFireRate(fireRate3, fireRateChangeDelay));
                }

            }
        }

        if(health <= 0){
            health = 0;
            Invoke(nameof(DestroyEnemy), .1f);
        }

        if(!changeColorWhenHit){
            return;
        }

        enemyRenderer.material.SetColor("_Color", new Color(1f, 0f, 0f));

        Invoke(nameof(ChangeColor), .1f);
    }

    private IEnumerator ChangeFireRate(float f, float waitTime){
        yield return new WaitForSeconds(waitTime);
        fireRate = f;
    }

    private void ChangeColor(){

        if(changeColor){
            enemyColor.r = (byte)((1f - health / totalHealth) * 255f);
            enemyColor.g = (byte)(health / totalHealth * enemyColor.g);
            enemyColor.b = (byte)(health / totalHealth * enemyColor.b);
        }
        
        enemyRenderer.material.SetColor("_Color", new Color(enemyColor.r / 255f, enemyColor.g / 255f, enemyColor.b / 255f));
    }

    private void DestroyEnemy(){
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}