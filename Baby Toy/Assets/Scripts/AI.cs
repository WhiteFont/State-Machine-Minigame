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


    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        currentState = new Patrol(this.gameObject, anim, player, ppointLeft, ppointRight);
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();
    }
}
