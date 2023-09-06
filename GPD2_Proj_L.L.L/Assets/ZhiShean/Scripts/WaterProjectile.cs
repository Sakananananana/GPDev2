using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterProjectile : MonoBehaviour
{
    public GameObject impactEffect;

    private bool collided;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Player" && !collided)
        {
            collided = true;

            var impact = Instantiate(impactEffect, collision.contacts[0].point, Quaternion.identity) as GameObject;

            Destroy(impact, 0.5f);

            Destroy(gameObject);
        }
    }
}
