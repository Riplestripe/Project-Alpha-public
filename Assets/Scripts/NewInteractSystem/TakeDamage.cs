using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : Interactble
{
    public float damage;
    private PlayerHealth playerhealth;
    private void Start()
    {
        playerhealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    protected override void Interact()
    {
        playerhealth.TakeDamage(damage);
    }
}
