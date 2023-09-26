using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Controller : MonoBehaviour
{
    #region Declaration
    [Header("To Count The Enemy Spawned Around the Spawner")]
    public int _enemySpawnCount;
    public Transform _mobSpawn;
    List<Transform> _mobAlive;
    bool _isSpawningEnded;

    [Header("To Spawn Enemy")]
    public Transform _spawnPosition;
    public Transform _enemyPrefab;
    Transform _spawnedEnemy;
    Vector3 _spawnPos;
    float _randRadius;
    float _randAngle;

    [Header("To Play Sound")]
    public AudioSource _audioSource;
    public AudioClip _spawnSFX;
    #endregion

    void Start()
    {
        _mobAlive = new List<Transform>();
        _isSpawningEnded = true;
        _enemySpawnCount = 2;
        _randRadius = 2;
    }

    void Update()
    {
        HowManyEnemySpawned();

        if (_mobAlive.Count < _enemySpawnCount && _isSpawningEnded)
        {
            StartCoroutine(EnemySpawningMechanics());
        }
    }

    void HowManyEnemySpawned() 
    {
        foreach (Transform child in _mobSpawn)
        {
            if (child.CompareTag("Enemy"))
            {
                if (!_mobAlive.Contains(child))
                {
                    _mobAlive.Add(child);
                }
            }
            if (_mobAlive.Contains(child) && !child.gameObject.activeSelf)
            { 
                _mobAlive.Remove(child);
            } 
        }
    }

    IEnumerator EnemySpawningMechanics()
    {
        _isSpawningEnded = false;

        yield return new WaitForSeconds(3);

        _randAngle = Random.Range(0f, 360f);
        _spawnPos = _spawnPosition.position + Quaternion.Euler(0, _randAngle, 0) * Vector3.right * Random.Range(0f, _randRadius);
        _spawnedEnemy = Instantiate(_enemyPrefab, _spawnPos, Quaternion.identity);
        _audioSource.PlayOneShot(_spawnSFX);
        _spawnedEnemy.SetParent(_mobSpawn);

        Debug.Log("Enemy Spawned");
        _isSpawningEnded = true;
    }
}
