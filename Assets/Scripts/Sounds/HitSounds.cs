using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSounds : MonoBehaviour
{
    public AudioSource Hitsound;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "HandTag")
        {
            Hitsound.Play();
        }

    }
}
