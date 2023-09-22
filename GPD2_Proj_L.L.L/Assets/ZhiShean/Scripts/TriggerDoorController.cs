using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    public Animator doorAnimator;

    public AudioSource SFX;
    public AudioClip[] DoorSounds;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SFX.PlayOneShot(DoorSounds[0]);
            doorAnimator.SetBool("DoorOpen", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SFX.PlayOneShot(DoorSounds[1]);
            doorAnimator.SetBool("DoorOpen", false);
        }
    }
}
