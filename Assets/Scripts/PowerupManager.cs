using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public GameObject pickupPrefab;
    public float spawnDelay;
    private float nextSpawnTime;
    private Transform tf;
    private GameObject spawnedPickup;
    public List<Powerup> powerups;
    private List<Powerup> removedPowerupsQueue;
    // Start is called before the first frame update
    void Start()
    {
        powerups = new List<Powerup>();
        removedPowerupsQueue = new List<Powerup>();
        nextSpawnTime = Time.time + spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Powerup powerup in powerups) // Put each powerup in the powerups List into the powerup variable and perform the if loop on it one powerup at a time
        {
            powerup.duration -= Time.deltaTime; // Subtract the time it took to draw the frame from the powerup's duration
            if (powerup.duration <= 0 && powerup.isPermanent == false) // If the duration of the powerup reaches zero and it's not a permanent powerup
            {
                Remove(powerup); // Remove the powerup
            }
        }
        if (spawnedPickup == null)  // if the spawned pickup doesn't exist
        {
            if (Time.time > nextSpawnTime)  // AND it's time for the next spawn
            {
                spawnedPickup = Instantiate(pickupPrefab, transform.position, transform.rotation) as GameObject; // Create the pickup
                nextSpawnTime = Time.time + spawnDelay; // And reset the timer
            }
        }
        else
        {
            nextSpawnTime = Time.time + spawnDelay; // otherwise, the pickup already exists, so keep pushing the next spawn time forward for now.
        }
    }
    private void LateUpdate()
    {
        ApplyRemovedPowerupsQueue();
    }
    public void Add(Powerup powerupToAdd) // Function for adding powerups to powerups List
    {
        powerupToAdd.Apply(this);
        powerups.Add(powerupToAdd);
    }
    public void Remove(Powerup powerupToRemove) // Function will eventually add a powerup
    {
        powerupToRemove.Remove(this); // Remove the powerupToRemove
        removedPowerupsQueue.Add(powerupToRemove); // Add the removed powerup to the removedPowerupQueue List
    }
    public void ApplyRemovedPowerupsQueue()
    {
        if (removedPowerupsQueue.Count != 0)
        {
            foreach (Powerup powerup in removedPowerupsQueue) // Remove each powerup in our temporary list one at a time
            {
                powerups.Remove(powerup); // Remove the powerup
            }
            removedPowerupsQueue.Clear(); // Reset list
        }
    }
}
