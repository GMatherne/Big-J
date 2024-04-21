using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public GameObject projectile;

    public LayerMask ground;
    public LayerMask playerLayer;

    public Vector3 patrolPoint;
    private bool patrolPointSet;
    public float patrolRange;

    public float timeBetweenAttacks;
    private bool alreadyAttacked;

    public float sightRange;
    public float attackRange;

    private bool playerInSightRange;
    private bool playerInAttackRange;

    public float health;

    private void Awake(){

        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

    }

    private void Update(){

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if(!playerInSightRange && !playerInAttackRange){
            Patrolling();
        }else if(playerInSightRange && !playerInAttackRange){
            ChasePlayer();
        }else{
            AttackPlayer();
        }
    }

    private void Patrolling(){

        if(!patrolPointSet){
            SearchPatrolPoint();
        }

        if(patrolPointSet){
            agent.SetDestination(patrolPoint);
        }

        Vector3 distanceToPatrolPoint = transform.position - patrolPoint;

        if(distanceToPatrolPoint.magnitude < 1f){
            patrolPointSet = false;
        }
    }

    private void SearchPatrolPoint(){
        float randomZ = Random.Range(-patrolRange, patrolRange);
        float randomX = Random.Range(-patrolRange, patrolRange);

        patrolPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
   
        if(Physics.Raycast(patrolPoint, -transform.up, 2f, ground)){
            patrolPointSet = true;
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
            rigidBody.AddForce(transform.up * 8f, ForceMode.Impulse);

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
            Invoke(nameof(DestroyEnemy), .5f);
        }

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