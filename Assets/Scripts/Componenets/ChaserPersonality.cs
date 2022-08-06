using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserPersonality : AIController
{
    public TankPawn Pawn;
    public GameObject Target;
    // Start is called before the first frame update
    public override void Start()
    {
        Pawn = gameObject.GetComponent<TankPawn>();
    }

    // Update is called once per frame
    public override void Update()
    {
        MakeDecisions();
    }

    public override void MakeDecisions()
    {
        switch (currentState)
        {
            case AIStates.Idle:
                DoIdleState();

                break;
        }
    }

    public override void DoAttackState()
    {
        Pawn.Shoot();
    }
}