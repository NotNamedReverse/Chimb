using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class MonkeyLobbyJoin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "HandTag")
        {
            if (!PhotonNetwork.InRoom)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            
        }
    }
}
