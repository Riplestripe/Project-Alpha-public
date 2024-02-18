using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonesOfHearing : MonoBehaviour
{   public LayerMask mask;
    public float radiusNear;
    public float radiusWalk;
    public float radiusSprint;
    public bool playerNear;
    public bool playerSprint;
    public bool playerWalk;
    private void Awake()
    {
        radiusNear = GetComponent<FieldOfView>().radius;
        radiusSprint = GetComponent<FieldOfView>().radiusSprinting;
        radiusWalk = GetComponent<FieldOfView>().radiusWalking;
    }

    private void Update()
    {
        Collider[] rangeCheckNear = Physics.OverlapSphere(transform.position, radiusNear, mask);
        if (rangeCheckNear.Length !=0 ) playerNear = true;
        else playerNear = false;
        Collider[] rangeCheckSprint = Physics.OverlapSphere(transform.position, radiusSprint, mask);
        if (rangeCheckSprint.Length != 0) playerSprint = true;
        else playerSprint = false;
        Collider[] rangeCheckWalk = Physics.OverlapSphere(transform.position, radiusWalk, mask);
        if (rangeCheckWalk.Length != 0) playerWalk = true;
        else playerWalk = false;
    }
}
