using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Photon.Pun;


public class ScrapRecycle : MonoBehaviour
{
    
    private void AddScrap(int coinsAmt = 2)
    {
        var request = new AddUserVirtualCurrencyRequest 
        {
            VirtualCurrency = "SC",
            Amount = coinsAmt
        };
        PlayFabClientAPI.AddUserVirtualCurrency(request, OnAddCoinsSuccess, OnError);
    }

    void OnAddCoinsSuccess(ModifyUserVirtualCurrencyResult result)
    {
        Debug.Log("SUCCESSFULLY RECYCLED SCRAP!");
        Playfablogin.instance.GetVirtualCurrencies();
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error: " + error.ErrorMessage);
        AddScrap(2);
    }

    public string HandTag = "HandTag";

    public AudioSource RecycleSound;

    public PhotonView View;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == HandTag)
        {
            Debug.Log("Hit");
            GameObject manager = GameObject.FindWithTag("CurrencyManager");

            if (manager){
                Debug.Log(manager.GetComponent<CurrencyManager>().Collected);
                if (manager.GetComponent<CurrencyManager>().Collected > 0)
                {
                    Debug.Log("has enough");
                    manager.GetComponent<CurrencyManager>().Collected -= 1f;

                    AddScrap(2);

                    View.RPC("PlayGlobalSound",RpcTarget.All);
                }
            }
        }
    }

    [PunRPC]
    private void PlayGlobalSound()
    {
        RecycleSound.Play();
    }
}
