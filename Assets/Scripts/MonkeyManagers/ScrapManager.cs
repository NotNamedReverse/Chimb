using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.VR;
using Photon.Realtime;

public class ScrapManager : MonoBehaviourPunCallbacks
{
    public float SpawnRate = 1f;
    public float ScrapLimit = 7f;
    public float TimeBetweenSpawns = 100f;

    public List<GameObject> SpawnedScrap = new List<GameObject>();

    public Transform[] Spawns;

    public bool InServer = false;
    public bool LeftServer = true;

    private GameObject Prefab;

    public void Start()
    {
        Prefab = Resources.Load<GameObject>("Stuff/ScrapObject");
        
    }

    public void Update()
    {
        if (PhotonNetwork.InRoom && ! InServer && LeftServer)
        {
            InServer = true;
            LeftServer = false;
            OnJoinedRoom();
        }
        else if (!PhotonNetwork.InRoom && !LeftServer && InServer){
            InServer = false;
            LeftServer = true;
            OnLeftRoom();
        }
    }


    public virtual void OnJoinedRoom()
    {
        Debug.Log("JOINED ROOM! SCRAP SYSTEM LOADED!");

        StartCoroutine(SpawnScrap());
    }

    IEnumerator SpawnScrap()
    {
        while (true){
            yield return new WaitForSeconds(TimeBetweenSpawns / SpawnRate);

            int PickedSpawn = Random.Range(0,Spawns.Length);

            GameObject scrap = (GameObject)GameObject.Instantiate(Prefab, Spawns[PickedSpawn].position, Quaternion.identity);
            scrap.transform.parent = Spawns[PickedSpawn];
            scrap.SetActive(true);
            
            SpawnedScrap.Add(scrap);

            Debug.Log(SpawnedScrap.Count);

            if (LeftServer)
            {
                Debug.Log("Left server, stopping scrap spawn");
                SpawnedScrap.Remove(scrap);
                Destroy(scrap);
                break;
            }

            if (SpawnedScrap.Count > ScrapLimit)
            {
                SpawnedScrap.Remove(scrap);
                Destroy(scrap);
            }
        }
    }

    public virtual void OnLeftRoom()
    {
        Debug.Log("Disconnected from room, Clearing spawned scrap");
        for (int index = 0; index < SpawnedScrap.Count; index++)
        {
            SpawnedScrap.Remove(SpawnedScrap[index]);
            Destroy(SpawnedScrap[index]);
        }
    } 

}
