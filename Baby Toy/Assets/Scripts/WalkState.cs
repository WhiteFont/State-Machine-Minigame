using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : State
{
    public WalkState(StateController stateController) : base(stateController) { }

    public override void CheckTransitions()
    {
        //if (stateController.CheckIfInRange("Player"))
        //{
        //    stateController.SetState(new ChaseState(stateController));
        //}
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    stateController.SetState(new WalkState(stateController));
        //}
        if (Input.GetKeyDown(KeyCode.W))
        {
            stateController.SetState(new ChaseState(stateController));
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            stateController.SetState(new PickUpState(stateController));
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            stateController.SetState(new StandingState(stateController));
        }
    }

    public override void Act()
    {
        //if (stateController.destination == null || stateController.ai.DestinationReached())
        //{
        //    stateController.destination = stateController.GetNexNavPoint();
        //    stateController.ai.SetTarget(stateController.destination);
        //}
    }

    public override void OnStateEnter()
    {
        stateController.isChasing = false;
        stateController.isPickingUp = false;
        stateController.isStanding = false;

        //stateController.destination = stateController.GetNextNavPoint();

        //if (stateController.ai.agent != null)
        //{
        //    stateController.ai.agent.speed = 1f;
        //}

        //stateController.ai.SetTarget(stateController.destination);

        stateController.isWalking = true;
    }
}
