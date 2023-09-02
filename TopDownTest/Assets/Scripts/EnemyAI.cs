using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    [Inject] private DataManager config;
    [Inject] private PlayerController playerController;
    
    public NavMeshAgent agent;
    public Animator animator;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;
    
    //Patrolling 
    public Vector3 walkPoint;
    private bool walkPointSet;
    [SerializeField] private float walkPointRange;
    
    //Attacking
    private float timeBetweenAttacks;
    private bool alreadyAttacked;
    
    //States
    private float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = playerController.GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();

        timeBetweenAttacks = config.EnemyAttackSpeed;
        sightRange = config.EnemySightRange;
        attackRange = config.EnemyAttackRange;
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);
        animator.SetBool("isRunning", true);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        
        //Walkpoint reached

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        animator.SetBool("isRunning", true);
    }
    private void AttackPlayer()
    {
        //Make sure enemy doesnt move
        agent.SetDestination(transform.position);
        animator.SetBool("isRunning", false);
        
        
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            animator.SetBool("isShooting", true);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        animator.SetBool("isShooting", false);
        alreadyAttacked = false;
    }
}
