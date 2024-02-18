using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasinfAfterSeen : MonoBehaviour
{
    public GameObject Enemy;
    Collider Collider;
    private Camera cam;
    Plane[] planes;
    public bool isChasing;
    public NavMeshAgent agent;
    public Transform player;

   

    // Update is called once per frame
    void Update()
    {
        agent = Enemy.GetComponent<NavMeshAgent>();
        cam = Camera.main;
        planes = GeometryUtility.CalculateFrustumPlanes(cam);
        Collider = GetComponent<Collider>();
        isChasing = false;
        if(GeometryUtility.TestPlanesAABB(planes, Collider.bounds))
        {
           isChasing = true;
            
        }
        
        if(isChasing)
        {
            ChasePlayer();
        }
    }
    public void ChasePlayer()
    {
        Vector3 distanceToPlayetLocation = transform.position - player.transform.position;
        agent.SetDestination(player.position);
    }
   
    }
