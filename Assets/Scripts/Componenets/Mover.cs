using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : MonoBehaviour
{
    // Start is called before the first frame update
    public abstract void Start();
    // Vectors that can be accessed by any future scripts to create movement for all varieties of child classes
    public abstract void Move(Vector3 direction, float speed);
    // Rotate function with a float for rotational speed. Can be inherited by other scripts to create different styles of rotation
    public abstract void Rotate(float turnSpeed);
}
