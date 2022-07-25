using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    // Start called before first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Calls Start from parent class Pawn
        base.Start();
    }
    // LIST OF MOVEMENTS FOR TANK PAWN)
    public override void MoveForward()
    {
        mover.Move(transform.forward, moveSpeed);
    }
    public override void MoveBackward()
    {
        mover.Move(transform.forward, -moveSpeed);
    }
    public override void RotateClockwise()
    {
        mover.Rotate(turnSpeed);
    }
    public override void RotateCounterClockwise()
    {
        mover.Rotate(-turnSpeed);
    }
    // Function for firing tank projectiles
    //public void FireShell()
    //{
    //    GameObject newPawnObj = Instantiate(tankShellPrefab, Vector3.zero, Quaternion.identity) as GameObject;
    //    void ShotCooldown()
    //    {
    //        float cooldownTime = 1.0f;
    //        float nextShellReady = 1;
    //        if (Time.time >= nextShellReady)
    //        {
    //            nextShellReady = Time.time + cooldownTime;
    //        }
    //    }
    //}
    //public GameObject tankShellPrefab;
}
