using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MasterController
{
    public GameObject target;
    public enum AIStates {Idle, Chase, Attack, Flee, Patrol, ChooseTarget};
    public AIStates currentState;
    public float fleeDistance;
    public Transform[] waypoints;
    public float waypointStopDistance;
    public float hearingDistance;
    public bool isTankPatroller;
    public bool canPatrolLoop;
    private float lastStateChangeTime;
    private int currentWaypoint = 0;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        ChangeState(AIStates.Idle);
        
        //var currentHealthRef = new Health();
        //currentHealthRef.currentHealth = 100;
        //currHealth = currentHealthRef.currentHealth;
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        MakeDecisions();
        if (CanHear(target) == true)
        {
            Debug.Log("The enemy heard that!");
        }
    }
    // Function for switching states
    public void MakeDecisions()
    {
        // change current state (AIStates...Idle,Chase,Flee,Patrol,Seek)
        switch (currentState)
        {
            case AIStates.Idle: // If AIStates is set to Idle
                DoIdleState(); // Call DoIdleState function, which makes the player remain still
                if (IsDistanceLessThan(target, 16)) // if distance is less than 5 meters from target
                {
                    ChangeState(AIStates.Chase); // change state to Chase
                }
                else if (isTankPatroller == true)
                {
                    ChangeState(AIStates.Patrol);
                }
                //else if (currentHealth <= maxHealth/2) // if health is less than half of max health
                //{
                //    ChangeState(AIStates.Flee); // change state to Flee
                //}
                break;
            case AIStates.Chase: // If AIStates is set to Chase
                DoChaseState(); // Call DoChaseState function, which makes the pawn seek a target
                if (IsHasTarget() == false)
                {
                    ChangeState(AIStates.ChooseTarget);
                }
                if (!IsDistanceLessThan(target, 16)) // if distance isn't less than float distance from target
                {
                    ChangeState(AIStates.Idle); // change state to Idle
                }
                else if (IsDistanceLessThan(target, 10))
                {
                    ChangeState(AIStates.Attack);
                }
                break;
            case AIStates.Attack:
                DoAttackState();
                if (IsHasTarget() == false)
                {
                    ChangeState(AIStates.ChooseTarget);
                }
                if (!IsDistanceLessThan(target, 10))
                {
                    Debug.Log("Distance is greater than 10!");
                    ChangeState(AIStates.Chase);
                }
                break;
            //case AIStates.Flee: // COME BACK TO FIX
            //    Flee();
            //    if (IsDistanceLessThan(target, 16)) // if health is less than ...
            //    {
            //        ChangeState(AIStates.Chase); // change state to Flee
            //    }
            //    else if (!IsDistanceLessThan(target, 10))
            //    {
            //        ChangeState(AIStates.Attack);
            //    }
            //    break;
            case AIStates.Patrol:                
                Patrol();
                if (IsHasTarget() == false)
                {
                    ChangeState(AIStates.ChooseTarget);
                }
                if (IsDistanceLessThan(target, 12))
                {
                    ChangeState(AIStates.Chase);
                }
                break;
            case AIStates.ChooseTarget:
                TargetPlayerOne();
                if (IsHasTarget() == true)
                {
                    ChangeState(AIStates.Idle);
                }
                break;
        }
    }
    protected bool IsDistanceLessThan(GameObject target, float distance)    // bool that passes in the target and a float for distance
    {
        if (Vector3.Distance(pawn.transform.position, target.transform.position) < distance)    // if distance between pawn and target is less than a given distance
        {
            return true;    // IsDistanceLessThan is true
        }
        else                // otherwise
        {
            return false;   // IsDistanceLessThan is false
        }
    }

    public virtual void ChangeState(AIStates newState) // function for Changing state
    {
        currentState = newState; // newstate is passed in when the function is called and currentState is assigned the value of the state that was passed in ( Example: ChangeState(AIStates.Chase) )
        lastStateChangeTime = Time.time; // tracks time that state was changed last. Time starts counting every time ChangeState() is called
    }
    protected virtual void DoChaseState() // function for the Chase behavior
    {
        Seek(target);
    }
    protected virtual void DoIdleState() // function for making the pawn Idle
    {
        // Remain still
    }
    protected virtual void DoAttackState() // function to perform actions associated with the Attack behavior
    {
        Seek(target);
        Shoot();
    }
    protected virtual void Flee() // COME BACK TO FIX
    {
        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;
        Vector3 vectorAwayFromTarget = -vectorToTarget;
        Vector3 fleeVector = vectorAwayFromTarget.normalized * fleeDistance;
        float targetDistance = Vector3.Distance(target.transform.position, pawn.transform.position);
        float percentOfFleeDistance = targetDistance / fleeDistance;
        percentOfFleeDistance = Mathf.Clamp01(percentOfFleeDistance);
        float flippedPercentOfFleeDistance = 1 - percentOfFleeDistance;
        Seek(pawn.transform.position + fleeVector);

    }
    protected void Patrol() // Function for the Patrol behavior
    {
        if (waypoints.Length > currentWaypoint) // if the number (Length) of waypoints is greater than currentWaypoint. Checks if we have enough waypoints to move to a currentWaypoint
        {
            Seek(waypoints[currentWaypoint]);   // Seeks the current Waypoint
            if (Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].position) < waypointStopDistance)  // If the distance between the pawn and the current waypoint is within the
            {                                                                                                           // waypointStopDistance
                currentWaypoint++; // increment to the next waypoint in the array
            }
        }
        else
        {
            if (canPatrolLoop == true)
            {
                RestartPatrol();
            }
        }
    }

    public void TargetPlayerOne()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.players != null)
            {
                if (GameManager.instance.players.Count > 0)
                {
                    target = GameManager.instance.players[0].pawn.gameObject;
                }
            }
        }
    }
    protected void TargetNearestTank()
    {
        Debug.Log("Targeting the nearest Tank now!");
        Pawn[] allTanks = FindObjectsOfType<Pawn>();
        Pawn closestTank = allTanks[0];
        float closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);

        foreach (Pawn tank in allTanks)
        {
            if (Vector3.Distance(pawn.transform.position, tank.transform.position) <= closestTankDistance)
            {
                closestTank = tank;
                closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);
            }
        }
        target = closestTank.gameObject;
    }
    public bool CanHear(GameObject target)
    {
        NoiseMaker noiseMaker = target.GetComponent<NoiseMaker>();
        if (noiseMaker == null)
        {
            return false;
        }
        if (noiseMaker.volumeDistance <= 0)
        {
            return false;
        }
        float totalDistance = noiseMaker.volumeDistance + hearingDistance;
        if (Vector3.Distance(pawn.transform.position, target.transform.position) <= totalDistance)
        {
            Debug.Log("The Enemy heard that!");
            return true;
        }
        else
        {
            return false;
        }
    }
    protected bool IsHasTarget()
    {
        return (target != null);
    }
    protected void RestartPatrol()
    {
        currentWaypoint = 0; // set currentWaypoint back to 0, thereby creating a loop for the Patrol behavior
    }
    public void Shoot() // Function for AI to Shoot
    {
        pawn.Shoot(); // makes the pawn Shoot
    }
    public void Seek(GameObject target)                 // function that passes in the GameObject and the target
    {
        pawn.RotateTowards(target.transform.position);  // rotate pawn toward the position of the target
        pawn.MoveForward();                             // make the pawn move forward
    }
    public void Seek(Vector3 targetPosition)    // Function for Seeking that passes in a Vector3 of the position of the target
    {
        pawn.RotateTowards(targetPosition);     // Rotate towards the target position
        pawn.MoveForward();                     // Move the pawn forward
    }
    public void Seek(Transform targetTransform) // Function for Seeking that passes in the targetTransform variable
    {
        Seek(targetTransform.position); // Calls Seek function, passing in the position of the targetTransform as the object to seek
    }
    public void Seek(Pawn targetPawn) // Function for Seeking that passes in the target Pawn
    {
        Seek(targetPawn.transform); // Calls Seek function, passing in the transform of the target Pawn
    }
}
