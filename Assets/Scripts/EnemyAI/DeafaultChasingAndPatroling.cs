using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class DeafaultChasingAndPatroling : MonoBehaviour
{
    [Header("Рефы")]
   public NavMeshAgent agent;
   public GameObject player;
   public LayerMask ground;

    [Header("Патрулирование")]
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public float searchingTime;
    private bool searchPointSet;
    public float rotationSpeed;

    [Header("Состояния монстра")]
    public bool isSearching;
    public bool isPatroling;
    public bool isChasing;

    [Header("Состояния персонажа")]
    private InputManager inputManager;
    public bool canSeePlayer;
    public bool playerIsAroundSprinting;
    public bool playerIsAroundWalking;
    public bool playerIsAround;
    public PlayerMovement movement;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        inputManager = GetComponent<InputManager>();
        movement = GetComponent<PlayerMovement>();

    }
    private void Update()
    {
        playerIsAroundSprinting = GetComponent<ZonesOfHearing>().playerSprint;
        playerIsAroundWalking = GetComponent<ZonesOfHearing>().playerWalk;
        playerIsAround = GetComponent<ZonesOfHearing>().playerNear;
        canSeePlayer = GetComponent<FieldOfView>().canSeePlayer;
        if (isPatroling)
        {
            animator.SetBool("isChasing", true);
            Patroling();
        }
        if (isChasing)
        {
            animator.SetBool("isChasing", true);
            isPatroling = false;
            ChasePlayer();
        }

        if (isChasing || !canSeePlayer) isPatroling = true;
        if (canSeePlayer || (playerIsAroundSprinting && movement.sprinting)) ChasePlayer();
       if (playerIsAroundWalking && inputManager.walkPressed && !movement.crouching) ChasePlayer();
        if (playerIsAround) ChasePlayer();



    }
    private void Patroling()
    {
        if (!walkPointSet && !isSearching) SearchWalkPoint();
        if(walkPointSet && !isSearching) 
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //����� ������ ����
        if (distanceToWalkPoint.magnitude < 1f && !isSearching)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.y + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, ground)) 
            walkPointSet = true;

    }

    public void ChasePlayer()
    {
        Vector3 distanceToPlayetLocation = transform.position - player.transform.position;
        agent.SetDestination(player.transform.position);
        if (distanceToPlayetLocation.magnitude < 1f && !canSeePlayer)
        {
            StartCoroutine(SearchForPlayer()); 
        }
    }

    private IEnumerator SearchForPlayer()
    {
        isSearching = true;

        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        yield return new WaitForSeconds(searchingTime);
        isSearching = false;

    }
        private IEnumerator ChaseRoutine()
    {
       
        isPatroling = false;
        isChasing = true;
        yield return new WaitForSeconds(searchingTime);
        isChasing = false;
        isPatroling = true;
        
    }
 

}
    
    