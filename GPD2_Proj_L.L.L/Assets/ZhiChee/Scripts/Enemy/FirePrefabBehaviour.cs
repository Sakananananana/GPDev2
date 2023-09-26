using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePrefabBehaviour : MonoBehaviour
{
    //Game Object & Transform Reference
    public Transform _target;
    public GameObject _targetedOBJ;
    public GameObject _impactEffect;

    //Vector 3
    Vector3 _targetPos;

    //Float
    float _speed = 3f;

    //Boolean
    bool _isCollided;

    void Awake()
    {
        _targetedOBJ = GameObject.FindWithTag("Player");
        if (_targetedOBJ != null)
        {
            _target = _targetedOBJ.transform;
        }

        _targetPos = (_target.position - transform.position).normalized;
    }

    void Update()
    {
        transform.Translate(_targetPos * _speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "EnemyAttack" && collision.gameObject.tag != "Enemy" && !_isCollided)
        {
            _isCollided = true;

            var impact = Instantiate(_impactEffect, collision.contacts[0].point, Quaternion.identity) as GameObject;

            Destroy(impact, 0.5f);

            Destroy(gameObject);
        }
    }
}
