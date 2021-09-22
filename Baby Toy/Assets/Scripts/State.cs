using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    public enum STATE
    {
        PATROL, CHASE, PICKUP, HOLD, DROP, STUN
    };

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    public STATE name;
    protected EVENT stage;
    protected GameObject npc;
    protected Animator anim;
    protected Transform player;
    protected State nextState;
    protected Transform ppointLeft;
    protected Transform ppointRight;

    public bool found = false;
    public bool picked = false;

    public State(GameObject _npc, Animator _anim, Transform _player, Transform _ppointLeft, Transform _ppointRight)
    {
        npc = _npc;
        anim = _anim;
        stage = EVENT.ENTER;
        player = _player;
        ppointLeft = _ppointLeft;
        ppointRight = _ppointRight;
    }

    public virtual void Enter()
    {
        stage = EVENT.UPDATE;
    }

    public virtual void Update()
    {
        stage = EVENT.UPDATE;
    }

    public virtual void Exit()
    {
        stage = EVENT.EXIT;
    }

    public State Process()
    {
        if (stage == EVENT.ENTER)
        {
            Enter();
        }
        if (stage == EVENT.UPDATE)
        {
            Update();
        }
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }
    public bool CanSeePlayer()
    {
        if (found)
        {
            return true;
        }
        return false;
    }
}


public class Patrol : State
{
    private bool facingLeft = true;
    public Patrol(GameObject _npc, Animator _anim, Transform _player, Transform _ppointLeft, Transform _ppointRight) : base(_npc, _anim, _player, _ppointLeft, _ppointRight)
    {
        name = STATE.PATROL;
    }

    public override void Enter()
    {
        anim.SetTrigger("Patrol");
        Debug.Log("Entered Patrol");
        base.Enter();
    }

    public override void Update()
    {
        if (CanSeePlayer())
        {
            Debug.Log("found baby");
            nextState = new Chase(npc, anim, player, ppointLeft, ppointRight);
            stage = EVENT.EXIT;
        }

        if ((ppointLeft.transform.position.x - npc.transform.position.x) < 0.2f && facingLeft)
        {
            npc.transform.Translate((-Time.deltaTime) * 2, 0, 0);
            //Debug.Log("moving left");
        }
        else if (facingLeft)
        { 
            facingLeft = false;
            npc.transform.Rotate(0,180,0);
            
            Debug.Log("turning right");
        }

        if ((npc.transform.position.x - ppointRight.transform.position.x) < 0.2f && !facingLeft)
        {
            npc.transform.Translate(-(Time.deltaTime) * 2, 0, 0);
            //Debug.Log("moving right");
        }
        else if (!facingLeft)
        {
            facingLeft = true;
            npc.transform.Rotate(0,180,0);
            Debug.Log("turning left");
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("Patrol");
        base.Exit();
    }
}

public class Chase : State
{
    public Chase(GameObject _npc, Animator _anim, Transform _player, Transform _ppointLeft, Transform _ppointRight) : base(_npc, _anim, _player, _ppointLeft, _ppointRight)
    {
        name = STATE.CHASE;
    }

    public override void Enter()
    {
        //anim.SetTrigger("Found");
        anim.SetTrigger("Chase");
        base.Enter();
    }

    public override void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Chase") && (npc.transform.position.x - player.transform.position.x) > 0.2f)
        {
            npc.transform.Translate(-(Time.deltaTime) * 3, 0, 0);
            //Debug.Log("chasing left");
            if ((npc.transform.position.x - player.transform.position.x) < 3f)
            {
                nextState = new PickUp(npc, anim, player, ppointLeft, ppointRight);
                stage = EVENT.EXIT;
            }
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Chase") && (player.transform.position.x - npc.transform.position.x) > 0.2f)
        {
            npc.transform.Translate(-(Time.deltaTime) * 3, 0, 0);
            //Debug.Log("chasing right");
            if ((player.transform.position.x - npc.transform.position.x) < 3f)
            {
                nextState = new PickUp(npc, anim, player, ppointLeft, ppointRight);
                stage = EVENT.EXIT;
            }
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("Chase");
        base.Exit();
    }
}

public class PickUp : State
{
    public GameObject holdBaby;
    public Rigidbody2D rb;
    public PlayerController pmScript;
    public PickUp(GameObject _npc, Animator _anim, Transform _player, Transform _ppointLeft, Transform _ppointRight) : base(_npc, _anim, _player, _ppointLeft, _ppointRight)
    {
        name = STATE.PICKUP;
    }

    public override void Enter()
    {
        holdBaby = npc.transform.Find("HoldBaby").gameObject;
        
        rb = player.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        pmScript = player.GetComponent<PlayerController>();
        pmScript.Grabbed();
        
        anim.SetTrigger("PickUp");
        anim.SetTrigger("Hold");
        base.Enter();
    }

    public override void Update()
    {
        player.transform.position = holdBaby.transform.position;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hold"))
        {
            nextState = new Hold(npc, anim, player, ppointLeft, ppointRight);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("PickUp");
        base.Exit();
    }
}

public class Hold : State
{
    public GameObject holdBaby;
    public Hold(GameObject _npc, Animator _anim, Transform _player, Transform _ppointLeft, Transform _ppointRight) : base(_npc, _anim, _player, _ppointLeft, _ppointRight)
    {
        name = STATE.HOLD;
    }

    public override void Enter()
    {
        holdBaby = npc.transform.Find("HoldBaby").gameObject;

        anim.SetTrigger("Hold");
        base.Enter();
    }

    public override void Update()
    {
        player.transform.position = holdBaby.transform.position;

        if (npc.transform.position.x > -31f)
        {
            npc.transform.Translate(-(Time.deltaTime) * 3, 0, 0);
        }
        else if (npc.transform.position.x < -31f)
        {
            nextState = new Drop(npc, anim, player, ppointLeft, ppointRight);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("Hold");
        base.Exit();
    }
}

public class Drop : State
{
    public Rigidbody2D rb;
    public PlayerController pmScript;
    public Drop(GameObject _npc, Animator _anim, Transform _player, Transform _ppointLeft, Transform _ppointRight) : base(_npc, _anim, _player, _ppointLeft, _ppointRight)
    {
        name = STATE.DROP;
    }

    public override void Enter()
    {
        rb = player.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(Vector2.left * 100);

        pmScript = player.GetComponent<PlayerController>();
        pmScript.Dropped();

        npc.transform.Rotate(0, 180, 0);

        Debug.Log("turning right");
        anim.SetTrigger("Stun");
        base.Enter();
    }

    public override void Update()
    {
        if (npc.transform.position.x  <= -31f)
        {
            npc.transform.Translate(-(Time.deltaTime) * 2, 0, 0);
        }
        else
        {
            anim.SetTrigger("Patrol");
            nextState = new Patrol(npc, anim, player, ppointLeft, ppointRight);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("Stun");
        base.Exit();
    }
}

public class Stun : State
{
    public Stun(GameObject _npc, Animator _anim, Transform _player, Transform _ppointLeft, Transform _ppointRight) : base(_npc, _anim, _player, _ppointLeft, _ppointRight)
    {
        name = STATE.STUN;
    }

    public override void Enter()
    {
        anim.SetTrigger("Stun");
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        anim.ResetTrigger("Stun");
        base.Exit();
    }
}
