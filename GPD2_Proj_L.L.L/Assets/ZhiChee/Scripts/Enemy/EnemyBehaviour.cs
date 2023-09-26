using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : EnemyStats
{
    protected override void Awake()
    {
        base.Awake();
        
        _currentHP = 4;
        _rotationSpeed = 35;
        _maxSpeed = 5;

        _currentState = _wanderState;
        _currentState.ForStateEnter(this);
    }

    private void Update()
    { 
        if (_currentState != null)
        { 
            _currentState.ForStateUpdate();
        }

        if (_isNearby && !_isAttack && _currentState != _seekState)
        {
           SwitchState(EnemyState.Seek);
        }
        if (_isNearby && _isAttack && _currentState != _attackState) 
        {
            SwitchState(EnemyState.Attack);
        }
        if (!_isNearby && _currentState != _wanderState)
        {
            SwitchState(EnemyState.Wander);
        }

        if (_currentHP <= 0)
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        { 
            _currentHP -= _objInt.damage;
        }
    }
}
