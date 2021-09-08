using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public ChaseState(StateController stateController) : base(stateController) { }

    public override void CheckTransitions()
    {
        //if (!stateController.CheckIfInRange("Player"))
        //{
        //    stateController.SetState(new CryState(stateController));
        //}
        if (Input.GetKeyDown(KeyCode.Q))
        {
            stateController.SetState(new WalkState(stateController));
        }
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    stateController.SetState(new ChaseState(stateController));
        //}
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
        //if (stateController.enemyToChase != null)
        //{
        //    stateController.destination = stateController.enemyToChase.transform;
        //    stateController.ai.SetTarget(stateController.destination);
        //}
    }
    public override void OnStateEnter()
    {
        stateController.isWalking = false;
        stateController.isPickingUp = false;
        stateController.isStanding = false;

        stateController.isChasing = true;
    }
}