using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.VR;
using Photon.VR.Cosmetics;

public class CosmeticTab : MonoBehaviour
{
    public GameObject cosmeticPoint;

    public enum CT {Head, Body, LeftHand, RightHand}
    public CT Type;

    private string Cosmetic;

    public bool debugHit;

    private void Do(){
        foreach (Transform C in cosmeticPoint.transform){
            if (C.gameObject.activeInHierarchy){
                Cosmetic = C.GetChild(0).name;
                Debug.Log(Cosmetic);
                break;
            }
         } 
         if (Type == CT.Head){
            PhotonVRManager.SetCosmetic(CosmeticType.Head, Cosmetic);
         }else if (Type == CT.Body){
            PhotonVRManager.SetCosmetic(CosmeticType.Body, Cosmetic);
         }else if (Type == CT.LeftHand){
            PhotonVRManager.SetCosmetic(CosmeticType.LeftHand, Cosmetic);
         }else if (Type == CT.RightHand){
            PhotonVRManager.SetCosmetic(CosmeticType.RightHand, Cosmetic);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HandTag"))
        {
            Do();
        }
    }

    private void Update(){
        if (debugHit == true){
            debugHit = false;
            Do();
        }
    }
}