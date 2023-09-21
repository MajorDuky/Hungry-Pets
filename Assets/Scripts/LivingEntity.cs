using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] protected Transform EntityTransform;
    protected int Health
    {
        get { return _health; }
        set { if (value.GetType() == typeof(int)) { _health = value; } }
    }
    protected int MaxHealth
    {
        get { return _maxHealth; }
        set { if (value.GetType() == typeof(int)) { _maxHealth = value; } }
    }
    protected float Speed
    {
        get { return _speed; }
        set { if (value.GetType() == typeof(float)) { _speed = value; } }
    }
    protected int Damage
    {
        get { return _damage; }
        set { if (value.GetType() == typeof(int)) { _damage = value; } }
    }

    /// <summary>
    /// Method that decreases the life of the entity and returns a boolean isAlive.
    /// </summary>
    /// <param name="damage">Amount of damages dealt to the entity</param>
    /// <returns>Boolean isAlive</returns>
    protected bool TakeDamage(int damage)
    {
        Health -= damage;
        bool isAlive = Health <= 0 ? false : true;
        return isAlive;
    }

    /// <summary>
    /// Method that heals the entity based on a fixed healAmount
    /// </summary>
    /// <param name="healAmount">Amount of health to recover</param>
    protected void RegainHealth(int healAmount)
    {
        if(healAmount + Health > MaxHealth || Health == MaxHealth)
        {
            Health = MaxHealth;
        }
        else
        {
            Health += healAmount;
        }
    }
}
