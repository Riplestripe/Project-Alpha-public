using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [Header("Радиусы окружностей поля зрения ")]
    public float radius;
    public float radiusWalking;
    public float radiusSprinting;
    [Range(0, 360)]
    public float angleSprint, angleWalk, angleNear;
    public GameObject playerRef;
    public LayerMask targetMask, obstructionMask;
    public bool canSeePlayer;



    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(FovRoutine());
    }

    private IEnumerator FovRoutine()
    {
        WaitForSeconds wait = new(0.2f);
        while (true)
        {
            yield return wait;
            FieldOfviewCheck();
        }
    }

    private void FieldOfviewCheck()
    {
        if(canSeePlayer) { canSeePlayer = false; }
        Collider[] rangeChecksSprint =  Physics.OverlapSphere(transform.position, radiusSprinting, targetMask);
        if (rangeChecksSprint.Length != 0)
        {
            Transform targetSprint = rangeChecksSprint[0].transform;
            Vector3 directionTargetSprint = (targetSprint.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionTargetSprint) < angleSprint / 2)
            {
                float distanceForTargetSprint = Vector3.Distance(transform.position, targetSprint.position);

                if (!Physics.Raycast(transform.position, directionTargetSprint, distanceForTargetSprint, obstructionMask))
                
                    canSeePlayer = true;
                
                else canSeePlayer = false;

            }
        }


        Collider[] rangeChecksWalk = Physics.OverlapSphere(transform.position, radiusWalking, targetMask);
        if (rangeChecksWalk.Length != 0)
        {
            Transform targetWalk = rangeChecksWalk[0].transform;
            Vector3 directionTargetWalk = (targetWalk.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionTargetWalk) < angleWalk / 2)

            {
                float distanceForTargetWalk = Vector3.Distance(transform.position, targetWalk.position);

                if (!Physics.Raycast(transform.position, directionTargetWalk, distanceForTargetWalk, obstructionMask))
                
                    canSeePlayer = true;
                
                else canSeePlayer = false;

            }
        }


        Collider[] rangeChecksNear = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecksNear.Length != 0)
        {

            Transform targetNear = rangeChecksNear[0].transform;
            Vector3 directionTargetNear = (targetNear.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionTargetNear) < angleNear / 2)
            {

                float distanceForTargetNear = Vector3.Distance(transform.position, targetNear.position);

                if (!Physics.Raycast(transform.position, directionTargetNear, distanceForTargetNear, obstructionMask))
                
                    canSeePlayer = true;
                
                else canSeePlayer = false;
            }
        }

    }

}








