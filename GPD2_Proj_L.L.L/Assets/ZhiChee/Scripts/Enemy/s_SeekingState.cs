using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SeekingState : BaseState
{
    Vector3 localDirection = Vector3.forward;
    float raylength = 5f;

    public override void ForStateEnter(EnemyStats _manager)
    {
        curEnmy = _manager;
        Debug.Log("Now Is Seek");
    }

    public override void ForStateUpdate()
    {
        Vector3 worldDirection = curEnmy.transform.TransformDirection(localDirection.normalized) * raylength;
        Vector3 endPoint = curEnmy.transform.position + worldDirection;
        Debug.DrawRay(curEnmy.transform.position, worldDirection, Color.blue);


        if (curEnmy._isNearby && Vector3.Distance(curEnmy.transform.position, curEnmy._target.position) < 8)
        {
            curEnmy._isAttack = true;
        }

        //To Calculate Desired Velocity
        curEnmy._targetPos = curEnmy._target.position;
        curEnmy._targetDir = (curEnmy._targetPos - curEnmy.transform.position).normalized;
        curEnmy._targetDir.y = 0;

        //To Calculate Current Velocity
        curEnmy._currentDir = 
            Vector3.RotateTowards(curEnmy.transform.forward, curEnmy._targetDir, 5 * Time.deltaTime, 0.0f);
        curEnmy.transform.rotation = 
            Quaternion.LookRotation(curEnmy._currentDir);

        //To Acceleration to Max Speed
        if (curEnmy._speed < curEnmy._maxSpeed)
        {
            curEnmy._speed = curEnmy._speed + curEnmy._accelerationRate * Time.deltaTime;
        }
        else
        {
            curEnmy._speed = curEnmy._maxSpeed;
        }

        //To Add Steering Force When Turning
        curEnmy._steerDir = (curEnmy._targetDir - curEnmy._currentDir);
        curEnmy._rbody.AddForce(curEnmy._steerDir * curEnmy._steeringForce);
        curEnmy._rbody.velocity = (curEnmy._speed * curEnmy._currentDir);
    }

    public override void ForStateExit()
    {
     
    }
}
