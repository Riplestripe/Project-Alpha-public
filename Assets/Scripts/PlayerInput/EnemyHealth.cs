using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int _maxHealth = 100;
    [SerializeField]
    private int _health;
    public int CurrentHealth { get => _health; set => _health = value; }

    public int MaxHealth { get => _maxHealth; private set => _maxHealth = value; }

    public event IDamageable.TakeDamageEvent OnTakeDamage;
    public event IDamageable.DeathEvent OnDeath;

    private void OnEnable()
    {
        CurrentHealth = _maxHealth;
    }

    public void TakeDamage(int Damage)
    {
        int damageTaken = Mathf.Clamp(Damage, 0, CurrentHealth);

        CurrentHealth -= damageTaken;

        if(damageTaken != 0)
        {
            OnTakeDamage?.Invoke(damageTaken);
        }

        if(CurrentHealth == 0 && damageTaken != 0) 
        {
            GameObject.Destroy(gameObject);
            OnDeath?.Invoke(transform.position);
        }
    }
}
