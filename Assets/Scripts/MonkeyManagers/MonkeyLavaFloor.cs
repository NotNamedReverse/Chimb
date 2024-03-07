using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MonkeyLavaFloor : MonoBehaviourPunCallbacks
{
    // hey skid hows it goin :)

    [Header("Settings")]
    public float LavaSpeed = 0.1f;

    public float LavaEndAmt = 2f;
    public float LavaStartHeight = 1f;
    public float LavaEndWaitTime = 1f;

    [Header("Eruption Stuff")]
    public GameObject Lava;
    public float EruptionPercent = 0f;
    public float MaxEruptionPercent = 15f;
    public float EruptionDissipateRate = 0.1f;

    private float LocalEruptionPercent = 0f;

    [Header("Trigger Stuff")]

    public string TriggerTag = "LavaTriggerRock";
    public float TriggerHeat = 1.5f;

    [Header("Debug Stuff")]
    public bool isRising = false;

    [Header("Networking")]

    public PhotonView view;

    [Header("Sounds")]

    public AudioSource StartSound;
    public AudioSource ActiveSound;
    public AudioSource DrainSound;

    [Header("Security Door")]
    public MonkeySecurityDoor door;

    [Header("Particles")]
    public GameObject LavaParticle;

    void Start()
    {
        // reset lava height aka Y position

        Lava.transform.position = new Vector3(Lava.transform.position.x, LavaStartHeight, Lava.transform.position.z);
        StartCoroutine(DisipateEruptionPercent());
    }

    IEnumerator DisipateEruptionPercent()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (PhotonNetwork.IsMasterClient)
            {
                if (EruptionPercent > 0)
                {
                    EruptionPercent -= EruptionDissipateRate;
                }
            }
        }
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {

            if (EruptionPercent < 0)
            {
                EruptionPercent = 0;
                LavaParticle.SetActive(false);
            }

            if (EruptionPercent >= MaxEruptionPercent && !isRising){
                view.RPC("StartLavaRise", RpcTarget.All);
            }

            if (EruptionPercent > 0){
                LavaParticle.SetActive(true);
            }

            // Update Eruption Percent for all clients
            if (!isRising){
                view.RPC("UpdateEruptionPercent", RpcTarget.All, EruptionPercent);
            }
        }
    }

    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == TriggerTag){
            Debug.Log("HIT BY TRIGGER");
            EruptionPercent += TriggerHeat;
            other.gameObject.SetActive(false);
        }
    }


    [PunRPC]
    void UpdateEruptionPercent(float amt)
    {
        EruptionPercent = amt;
    }

    [PunRPC]
    void StartLavaRise(){
        // set variables
        isRising = true;
        door.doorOpen = false;

        EruptionPercent = 0;

        // remove particles

        LavaParticle.SetActive(false);
        
        // play sounds
        StartSound.Play();
        DrainSound.Play();

        // start coroutine so we can have delays

        StartCoroutine(LavaRiseInit());
        
    }

    private IEnumerator LavaRiseInit(){
        Debug.Log("Started Rise Init");

        yield return new WaitForSeconds(1);

        // start lava sound

        ActiveSound.Play();

        yield return new WaitForSeconds(1);

        // start lava

        for (int i = 0; i < LavaEndAmt; i++)
        {
            yield return new WaitForSeconds(LavaSpeed);
            Lava.transform.position += new Vector3(0,0.01f,0);
        }

        // end round

        yield return new WaitForSeconds(LavaEndWaitTime);

        ActiveSound.Stop();
        StartSound.Play();
        DrainSound.Play();

        for (int i = 0; i < LavaEndAmt; i++)
        {
            yield return new WaitForSeconds(0.03f);
            Lava.transform.position -= new Vector3(0,0.01f,0);
        }

        DrainSound.Stop();
        door.doorOpen = true;
        isRising = false;
    }
}
