using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using UnityEngine;

public class AttackingState : BaseState
{
    Vector3 localDirection = Vector3.forward;
    float raylength = 5f;
   

    public override void ForStateEnter(EnemyStats _manager)
    {
        curEnmy= _manager;
        Debug.Log("Now Is Attack");
    }

    public override void ForStateUpdate()
    {
        Vector3 worldDirection = curEnmy.transform.TransformDirection(localDirection.normalized) * raylength;
        Vector3 endPoint = curEnmy.transform.position + worldDirection;
        Debug.DrawRay(curEnmy.transform.position, worldDirection, Color.red);

        if (curEnmy._isNearby && Vector3.Distance(curEnmy.transform.position, curEnmy._target.position) > 6)
        {
            curEnmy._isAttack = false;
        }

        if (curEnmy._rbody.velocity.sqrMagnitude < 0.01f)
        {
            curEnmy._rbody.velocity = Vector3.zero;
            curEnmy._speed = 0;
        }
        else
        {
            curEnmy._rbody.velocity -= curEnmy._rbody.velocity.normalized * 2.5f * Time.deltaTime;
        }

        //Attack
        curEnmy.transform.LookAt(curEnmy._target.position);
        curEnmy.FireMechanics();
    }

    public override void ForStateExit()
    {

    }

}
