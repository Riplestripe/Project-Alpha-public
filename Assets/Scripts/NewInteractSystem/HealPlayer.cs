using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayer : Interactble
{
    public float heal;
    private PlayerHealth playerhealth;

    void Start()
    {
        playerhealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

    }

    protected override void Interact()
    {
        playerhealth.RestoreHealth(heal);
    }
}
