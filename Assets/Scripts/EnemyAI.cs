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

    public float timeBetweenAttacks;
    private bool alreadyAttacked;

    public float sightRange;
    public float attackRange;

    public float projectilePower;

    public bool lookAtPlayer;

    private bool playerInSightRange;
    private bool playerInAttackRange;

    [Header("Stats")]
    public float totalHealth;
    private float health;

    private void Awake(){

        health = totalHealth;

        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

        enemyRenderer = body.GetComponent<Renderer>();
        enemyColor = enemyRenderer.material.color;
    }

    private void Update(){

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if(!playerInSightRange && !playerInAttackRange){
            Searching();
        }else if(playerInSightRange && !playerInAttackRange){
            ChasePlayer();
        }else{
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

        agent.SetDestination(transform.position);

        if(lookAtPlayer){
            transform.LookAt(player);
        }else{
            attackPoint.LookAt(player);
        }

        if(!alreadyAttacked){

            Rigidbody rigidBody = Instantiate(projectile, attackPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
           
            rigidBody.AddForce(attackPoint.forward * projectilePower, ForceMode.Impulse);

            alreadyAttacked = true;

            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack(){
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage){

        health -= damage;

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