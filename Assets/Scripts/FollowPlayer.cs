using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public TankPawn tankPawn;

    // Update is called once per frame
    void Update()
    {
        if (tankPawn != null)
        {
            transform.position = player.transform.position + new Vector3(0, 1, -5);
        }
        
    }
}
