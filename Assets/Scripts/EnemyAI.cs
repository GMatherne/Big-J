using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public GameObject projectile;

    public GameObject enemy;
    private Renderer enemyRenderer;
    private float redValue;

    public LayerMask ground;
    public LayerMask playerLayer;

    public Vector3 searchPoint;
    private bool searchPointSet;
    public float searchRange;

    public float timeBetweenAttacks;
    private bool alreadyAttacked;

    public float sightRange;
    public float attackRange;

    private bool playerInSightRange;
    private bool playerInAttackRange;

    private float health;
    public float totalHealth;

    private void Awake(){

        redValue = 0f;
        health = totalHealth;
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        enemyRenderer = enemy.GetComponent<Renderer>();

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

        transform.LookAt(player);

        if(!alreadyAttacked){

            Rigidbody rigidBody = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
           
            rigidBody.AddForce(transform.forward * 32f, ForceMode.Impulse);

            alreadyAttacked = true;

            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack(){
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage){

        enemyRenderer.material.SetColor("_Color", new Color(1f, 0f, 0f));

        health -= damage;

        if(health <= 0){
            health = 0;
            Invoke(nameof(DestroyEnemy), .1f);
        }

        Invoke(nameof(ChangeColor), .1f);
    }

    private void ChangeColor(){
        redValue = 1 - health/totalHealth;
        enemyRenderer.material.SetColor("_Color", new Color(redValue, 0f, 0f));
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