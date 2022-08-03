using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    // variable for move speed. Editable in component for designers
    public float moveSpeed;
    // variable for turn speed. Editable in component for designers
    public float turnSpeed;
    // variable to hold the mover
    public Mover mover;
    // rate of fire
    public float fireRate;
    // Variable to hold the shooter
    public Shooter shooter;
    // public variable for the shell prefab
    public GameObject shellProjectile;
    // the force (velocity) of firing a projectile
    public float fireForce;
    // float for damage done to an object
    public float damageDone;
    // how long the shell exists if it doesn't collide with anything
    public float shellLifespan;

    // Start is called before the first frame update
    public virtual void Start()
    {
        mover = GetComponent<Mover>();
        shooter = GetComponent<Shooter>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
    }

    // functions for Pawn actions
    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();
    public abstract void Shoot();

};

