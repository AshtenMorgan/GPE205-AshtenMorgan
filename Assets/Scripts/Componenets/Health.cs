using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    // Start is called before the first frame update
    public void Start()
    {
        // Sets currentHealth to the value of maxHealth at the start of the game
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void TakeDamage(float damageAmount, Pawn damageSource)
    {
        currentHealth = currentHealth - damageAmount;
        Debug.Log(damageSource.name + " did " + damageAmount + " damage to " + gameObject.name);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (currentHealth <= 0)
        {
            Die(damageSource);
        }
    }

    public void Heal(float healingAmount, Pawn healingSource)
    {
        currentHealth = currentHealth + healingAmount;
        Debug.Log(healingSource.name + " healed " + gameObject.name + " by " + healingAmount + " health points ");
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void Die(Pawn damageSource)
    {
        Destroy(gameObject);
    }
}
