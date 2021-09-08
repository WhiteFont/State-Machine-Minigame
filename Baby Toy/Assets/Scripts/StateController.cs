using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public State currentState;

    public bool isWalking = false;
    public bool isChasing = false;
    public bool isPickingUp = false;
    public bool isStanding = false;

    void Start()
    {
        SetState(new WalkState(this));
    }

    // Update is called once per frame
    void Update()
    {
        currentState.CheckTransitions();
        currentState.Act();
    }

    public void SetState(StateController state)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = state;
        gameObject.name = "AI agent in state " + state.GetType().Name;

        if (currentState != null)
        {
            currentState.OnStateEnter();
        }
    }
}
