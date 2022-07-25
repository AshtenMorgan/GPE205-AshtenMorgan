using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    // variable for move speed. Editable in component for designers
    public float moveSpeed;
    // variable for turn speed. Editable in component for designers
    public float turnSpeed;
    public Mover mover;
    public float fireRate;

    // Start is called before the first frame update
    public virtual void Start()
    {
        mover = GetComponent<Mover>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
    }

    // functions for Pawn movements
    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();

};

