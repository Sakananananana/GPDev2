using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingState : BaseState
{
    public override void ForStateEnter(EnemyStats _manager)
    {
        curEnmy = _manager;
        Debug.Log("Now Is Wander");
    }

    public override void ForStateUpdate()
    {
        if (curEnmy._rbody.velocity.sqrMagnitude < 0.01f)
        { 
            curEnmy._rbody.velocity = Vector3.zero;
            curEnmy._speed = 0;
        }
        else
        {
            curEnmy._rbody.velocity -= curEnmy._rbody.velocity.normalized * Time.deltaTime;
        }

        if(curEnmy._rbody.velocity == Vector3.zero)
        {
            curEnmy.transform.Rotate(curEnmy._rotationSpeed * Time.deltaTime * curEnmy.transform.up);
        }
        
    }

    public override void ForStateExit()
    {

    }
}
