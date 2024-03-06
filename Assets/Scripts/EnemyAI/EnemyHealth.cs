using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100;
    public float currentHealth;

    private void Start()
    {
        currentHealth = health;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
    private void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
