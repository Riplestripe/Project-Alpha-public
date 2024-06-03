using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth = 100f;
    public Slider frontHealthBar;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        health = Mathf.Clamp(health,0,maxHealth);
        UpdateHealthUI();
    }
    public void UpdateHealthUI()
    {
        frontHealthBar.value = health;


    }

    public void TakeDamage(float Damage)
    {
        health -= Damage;
    }

    public void RestoreHealth(float heal)
    {
        health += heal;

    }
}
