using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GorillaLocomotion;

public class BreakableFloor : MonoBehaviour
{
    public string HandTag = "HandTag";

    public AudioSource BreakSound;

    public Player chimpPlayer;

    public MeshRenderer Renderer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == HandTag)
        {
            BreakSound.Play();
            chimpPlayer.isRespawning = true;
            chimpPlayer.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,-0.2f,0);
            chimpPlayer.gameObject.GetComponent<Rigidbody>().position = gameObject.transform.position - new Vector3(0,4,0);

            StartCoroutine(respawn());
        }
    }

    IEnumerator respawn()
    {
        yield return new WaitForSeconds(1);

        chimpPlayer.isRespawning = false;
    }
}
