using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePrefabBehaviour : MonoBehaviour
{

    public Transform _target;
    public GameObject _targetedOBJ;

    Vector3 _targetPos;

    float _speed = 3f;

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
}
