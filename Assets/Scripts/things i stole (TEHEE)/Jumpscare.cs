using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GorillaLocomotion;

public class Jumpscare : MonoBehaviour
{
    Collider m_Collider;

    public Transform gorillaPlayer;
    public Player player;
    public Transform respawnPoint;
    public List<GameObject> mapsToDisable;
    public float delayBeforeReEnabling;
    public float timeBeforeTeleport;
    public GameObject jumpscareObject;
    public float jumpscareDuration;
    public bool isJumpscaring = true;
    public AudioSource Hitsound;
    


    void Start()
    {

        m_Collider = GetComponent<Collider>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HandTag") || other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(TeleportPlayer());


            if (isJumpscaring)
            {
                m_Collider.enabled = !m_Collider.enabled;
                isJumpscaring = false;
                Hitsound.Play();
                jumpscareObject.SetActive(true);
                gorillaPlayer.position = respawnPoint.position;

                Invoke("DisableJumpscare", jumpscareDuration);
                
            }

        }
    }

    IEnumerator TeleportPlayer()
    {
        foreach (GameObject x in mapsToDisable)
        {
            x.SetActive(false);
        }

        yield return new WaitForSeconds(timeBeforeTeleport);

        

        foreach (GameObject x in mapsToDisable)
        {
            x.SetActive(true);
        }

        gorillaPlayer.position = respawnPoint.position;
        yield return new WaitForSeconds(delayBeforeReEnabling);
    }

    private void DisableJumpscare()
    {
        jumpscareObject.SetActive(false);
        isJumpscaring = true;
        m_Collider.enabled = !m_Collider.enabled;
    }


}
