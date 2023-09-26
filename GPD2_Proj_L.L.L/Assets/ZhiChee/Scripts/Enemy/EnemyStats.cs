using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    #region Declaration
    //Script References
    [HideInInspector] public Rigidbody _rbody;
    [HideInInspector] public ObjectInteraction _objInt;

    //Game Object Reference
    [HideInInspector] public Transform _target;
    [HideInInspector] public Transform _firePort;
    public Transform _firePrefab;
    [HideInInspector] public GameObject _targetedOBJ;

    //Boolean
    public bool _isAttack;
    public bool _isNearby;

    //Variable Declaration
    [HideInInspector] public int _currentHP;

    public float _maxSpeed;
    [HideInInspector] public float _accelerationTimeToMax = 3.0f;
    [HideInInspector] public float _steeringForce;

    public float _speed;
    [HideInInspector] public float _rotationSpeed;
    [HideInInspector] public float _accelerationRate;
    
    [HideInInspector] public Vector3 _currentDir;
    [HideInInspector] public Vector3 _targetPos;
    [HideInInspector] public Vector3 _targetDir;
    [HideInInspector] public Vector3 _steerDir;

    //Fire Mechanics
    [HideInInspector] public AudioSource _audioSource;
    [HideInInspector] public AudioClip _attackClip;
    [HideInInspector] float _FireCD = 2.0f;
    #endregion

    //State Mechines
    protected BaseState _currentState;
    protected BaseState _wanderState;
    protected BaseState _attackState;
    protected BaseState _seekState;

    protected virtual void Awake()
    {
        _targetedOBJ = GameObject.FindWithTag("Player");
        if (_targetedOBJ != null)
        {
            _target = _targetedOBJ.transform;
            _objInt = _targetedOBJ.GetComponent<ObjectInteraction>();
        }

        _wanderState = new WanderingState();
        _attackState = new AttackingState();
        _seekState = new SeekingState();

        _isNearby = false;
        _isAttack = false;

        _rbody = GetComponent<Rigidbody>();

        _accelerationRate = _maxSpeed / _accelerationTimeToMax;
    }

    public void SwitchState(EnemyState _state)
    {
        var newState = _state switch
        {
            EnemyState.Seek => _seekState,
            EnemyState.Attack => _attackState,
            EnemyState.Wander => _wanderState,
            _ => null
        };

        if (_currentState != null)
        {
            _currentState.ForStateExit();
        }
        _currentState = newState;
        _currentState.ForStateEnter(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            _isNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isNearby = false;
        }
    }

    public void FireMechanics()
    {
        if (_FireCD > 0f)
        {
            _FireCD -= Time.deltaTime;
        }
        else
        {
            Instantiate(_firePrefab, _firePort.position, Quaternion.identity);
            _audioSource.PlayOneShot(_attackClip);
            _FireCD = 2.0f;
        }
    }
}
