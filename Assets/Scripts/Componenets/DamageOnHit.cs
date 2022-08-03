using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    public float damageDone;
    public Pawn owner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        // Grabs the Health component of the object with the collider that's being overlapped
        Health otherHealth = other.gameObject.GetComponent<Health>();
        // If the game object does have a Health component
        if (otherHealth != null)
        {
            // Take damage
            otherHealth.TakeDamage(damageDone, owner);
        }
        // Destroys the object this DamageOnHit component is attached to
        Destroy(gameObject);
    }
}
