using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerController : MasterController
{
    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;
    public KeyCode rotateClockwiseKey;
    public KeyCode rotateCounterClockwiseKey;
    public KeyCode shootKey;

    public int PlayerScore;

    // Start is called before the first frame update
    public override void Start()
    {
        PlayerScore = 0;
        // If an instance of the GameManger exists
        if (GameManager.instance != null)
            // and if instances of players exist
            if (GameManager.instance.players != null)
                // Instantiate this player controller
                GameManager.instance.players.Add(this);
        // Calls Start() the parent class MasterController
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Function for processing key inputs
        ProcessInputs();
        // Calls Update() from the parent class MasterController
        // base.Update();
    }

    // Function for when this component is destroyed
    public void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.players != null)
            {
                GameManager.instance.players.Remove(this);
            }
        }
    }
    // Function that processes all movements based on key presses
    public override void ProcessInputs()
    {
        if (Input.GetKey(moveForwardKey))
        {
            pawn.MoveForward();
        }
        if (Input.GetKey(moveBackwardKey))
        {
            pawn.MoveBackward();
        }
        if (Input.GetKey(rotateClockwiseKey))
        {
            pawn.RotateClockwise();
        }
        if (Input.GetKey(rotateCounterClockwiseKey))
        {
            pawn.RotateCounterClockwise();
        }
        if (Input.GetKeyDown(shootKey))
        {
            pawn.Shoot();
        }
    }

    public void AddToScore()
    {
        PlayerScore = PlayerScore + 10;
    }
}
