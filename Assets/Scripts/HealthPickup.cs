using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public HealthPowerup powerup;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 1, 0);
    }
    public void OnTriggerEnter(Collider other)
    {
        PowerupManager powerupManager = other.GetComponent<PowerupManager>();   // grabs and stores "other" object's PowerupManager so we can access it here
        if (powerupManager != null) // if the object has a Powerup Manager
        {
            powerupManager.Add(powerup); // Add the powerup to the object
            Destroy(gameObject); // And destroy the object this component is attached to
        }
    }
}
