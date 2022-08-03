using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inherits Move Vector3 from Mover parent class to calculate movements for the Tank child class
public class TankMover : Mover
{
    // instantiates rbody as the rigidbody component
    private Rigidbody rbody;

    // Start is called before the first frame update
    public override void Start()
    {
        // Gets the rigidbody component so rbody can be referenced for vector movement
        rbody = GetComponent<Rigidbody>();
    }

    // function that calcuates vectors for rigidbody as the tank moves
    public override void Move(Vector3 direction, float speed)
    {
        // moveVector that uses Time instead of Update() so that movement is independent of framerate
        Vector3 moveVector = direction.normalized * speed * Time.deltaTime;

        // adds moveVector values to current rigidbody position to create movement
        rbody.MovePosition(rbody.position + moveVector);
    }
    public override void Rotate(float turnSpeed)
    {
        // creates float that calculates vectors for rotation based on time instead of framerate
        float rotationVector = turnSpeed * Time.deltaTime;
        // applies transform on the Y axis of rotation based on float
        transform.Rotate(0, rotationVector, 0);
    }

}
