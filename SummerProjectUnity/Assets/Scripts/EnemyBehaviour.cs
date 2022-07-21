using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyBehaviour : MonoBehaviour, IDamageable
{
    #region Fields
    [SerializeField] private GameObject prefabParent;
    [SerializeField] private GameObject attackProjectile;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float health;
    [SerializeField] private float walkPointRange;
    [SerializeField] private float sightRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float timeBetweenAttacks;
    private NavMeshAgent agent;
    private Transform player;

    //Patrolling
    private bool walkPointSet;
    private Vector3 walkPoint;
    //Attacking
    private bool alreadyAttacked;
    //States
    private bool playerInSightRange;
    private bool playerInAttackRange;
    #endregion
    #region LifeCycle Methods;
    void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>(); 
    }
    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInAttackRange && !playerInSightRange)
        {
            Patrolling();
        }
        if (!playerInAttackRange && playerInSightRange)
        {
            ChasePlayer();
        }
        if (playerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }
        if (health <= 0)
        {
            DestroyEnemy();
        }
    }
    #endregion
    #region Methods
    #region AI Movement
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer))
        {
            walkPointSet = true;
        }
    }
    private void Patrolling()
    {
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
        else
        {
            SearchWalkPoint();           
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    #endregion
    #region Attacking
    private void AttackPlayer()
    {
        transform.LookAt(player);
        if (!alreadyAttacked)
        {
            //Attack code here
            Attack(attackProjectile);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void Attack(GameObject pfProjectile)
    {
        Quaternion quaternion = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        GameObject projectile = Instantiate(pfProjectile, transform.position, quaternion);
        Vector3 shootDir = (player.transform.position - transform.position).normalized;
        projectile.GetComponent<IShootable>().InitializeProjectile(shootDir, "Enemy");
        projectile.transform.SetParent(prefabParent.transform);
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    #endregion
    public void TakeDamage(float damage)
    {
        health -= damage;  
    }
   
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    #endregion;
}
