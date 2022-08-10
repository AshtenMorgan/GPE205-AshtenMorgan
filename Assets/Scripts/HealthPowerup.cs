using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class HealthPowerup : Powerup
{
    public float healthToAdd;
    public override void Apply(PowerupManager target)
    {
        Health targetHealth = target.GetComponent<Health>(); // get Health component from the target Object and create variable we can use to change data in the target Health component
        if (targetHealth != null) // If the target's Health component ("targetHealth" in this script) exists
        {
            targetHealth.Heal(healthToAdd, target.GetComponent<Pawn>()); // Call the Heal function from Health component to add health to the target pawn)
        }
    }

    public override void Remove(PowerupManager target)
    {
        Health targetHealth = target.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(healthToAdd, target.GetComponent<Pawn>());
        }
    }
}
