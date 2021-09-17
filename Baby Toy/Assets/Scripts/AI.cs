using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    Animator anim;
    public Transform player;
    public Transform ppointLeft;
    public Transform ppointRight;
    public GameObject npc;
    State currentState;

    public Transform hold;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        currentState = new Patrol(this.gameObject, anim, player, ppointLeft, ppointRight);
    }

    public void SeeBaby()
    {
        currentState.found = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();
    }
}
