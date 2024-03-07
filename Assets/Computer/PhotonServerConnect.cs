using Photon.VR;
using Photon.Pun;
using UnityEngine;

public class PhotonServerConnect : MonoBehaviour
{
    [Header("MADE BY BROWNIETHEDEV!")]
    [Header("NO CREDITS NEEDED!")]
    public string AppID;
    public string VoiceID;
    public string HandTag;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(HandTag))
        {
            PhotonVRManager.ChangeServers(AppID, VoiceID);
        }
    }
}