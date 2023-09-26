using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Detector : MonoBehaviour
{
    Spawn_Controller controller;
    public GameObject _spawner;

    private void Start()
    {
        controller = _spawner.GetComponent<Spawn_Controller>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Entered");
            controller._enemySpawnCount = 4;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            controller._enemySpawnCount = 2;
        }
    }
}
