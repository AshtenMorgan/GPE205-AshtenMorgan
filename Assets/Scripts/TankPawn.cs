using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    // Start called before first frame update
    public override void Start()
    {
        nextShotDelay = 0;
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Calls Start from parent class Pawn
        base.Start();
        nextShotDelay -= Time.deltaTime;
        nextShotDelay = Mathf.Clamp(nextShotDelay, 0, shotCooldown);
    }
    // LIST OF ACTIONS FOR TANK PAWN
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
    public override void RotateTowards(Vector3 targetPosition)
    {
        Vector3 vectorToTarget = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    public float shotCooldown = 1.0f;
    private float nextShotDelay;
    public override void Shoot()
    {
        if (nextShotDelay <= 0)
        {
            shooter.Shoot(shellProjectile, fireForce, damageDone, shellLifespan);
            nextShotDelay = shotCooldown;
        }
        else if (nextShotDelay > 0)
        {
        }

    }
}