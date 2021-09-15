using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    public enum STATE
    {
        PATROL, CHASE, PICKUP, HOLD, STUN
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
        //Vector3 direction = player.position - npc.transform.position;
        //float angle = Vector3.Angle(direction, npc.transform.forward);

        if ((npc.transform.position.x - player.position.x) < 8)
        {
            Debug.Log("found you");
            return true;
        }
        Debug.Log("where baby");
        return false;

        //if (direction.magnitude < visDist && angle < visAngle)
        //{
        //    return true;
        //}
    }

    public bool CanPickUp()
    {
        if ((npc.transform.position.x - player.position.x) < 4)
        {
            Debug.Log("i get you now");
            return true;
        }
        Debug.Log("i see baby");
        return false;
    }

    public bool PickedUp()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            return true;
        }
        return false;
    }

    public bool Holding()
    {
        //if (true)
        //{

        //    return true;
        //}
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
        base.Enter();
    }

    public override void Update()
    {
        //if (Random.Range(0, 100000000000) < 10)
        //{
        //    nextState = new Chase(npc, anim, player, ppointLeft, ppointRight);
        //    stage = EVENT.EXIT;
        //}

        if ((ppointLeft.transform.position.x - npc.transform.position.x) < 0.2f && facingLeft)
        {
            npc.transform.Translate((-Time.deltaTime) * 2, 0, 0);
            Debug.Log("moving left");
        }
        else if (facingLeft)
        { 
            anim.SetTrigger("TurnRight");
            facingLeft = false;
            Debug.Log("turning right");
        }

        if ((npc.transform.position.x - ppointRight.transform.position.x) < 0.2f && !facingLeft)
        {
            npc.transform.Translate((Time.deltaTime) * 2, 0, 0);
            Debug.Log("moving right");
        }
        else if (!facingLeft)
        {
            anim.SetTrigger("TurnLeft");
            facingLeft = true;
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
        anim.SetTrigger("Chase");
        base.Enter();
    }

    public override void Update()
    {
        if (CanPickUp())
        {
            nextState = new PickUp(npc, anim, player, ppointLeft, ppointRight);
            stage = EVENT.EXIT;
        }
        else if (!CanSeePlayer())
        {
            nextState = new Patrol(npc, anim, player, ppointLeft, ppointRight);
            stage = EVENT.EXIT;
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
    public PickUp(GameObject _npc, Animator _anim, Transform _player, Transform _ppointLeft, Transform _ppointRight) : base(_npc, _anim, _player, _ppointLeft, _ppointRight)
    {
        name = STATE.PICKUP;
    }

    public override void Enter()
    {
        anim.SetTrigger("PickUp");
        base.Enter();
    }

    public override void Update()
    {
        if (PickedUp())
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
    public Hold(GameObject _npc, Animator _anim, Transform _player, Transform _ppointLeft, Transform _ppointRight) : base(_npc, _anim, _player, _ppointLeft, _ppointRight)
    {
        name = STATE.HOLD;
    }

    public override void Enter()
    {
        anim.SetTrigger("Hold");
        base.Enter();
    }

    public override void Update()
    {
        if (!Holding())
        {
            nextState = new Stun(npc, anim, player, ppointLeft, ppointRight);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("Hold");
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
