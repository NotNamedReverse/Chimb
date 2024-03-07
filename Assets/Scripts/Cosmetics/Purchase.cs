using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public class Purchase : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("EquipCosmeticScripts")]
    public ChangeHeadCosmetic HeadCos;
    public ChangeBodyCosmetic BodyCos;
    public ChangeRightCosmetic RightCos;
    public ChangeLeftCosmetic LeftCos;

    [Header("COSMETICS")]
    public GameObject enable;
    public GameObject disable;

    public GameObject enable2;
    public GameObject disable2;

    [Header("BUY")]
    public string CosmeticName;
    public int coinsPrice;
    public Playfablogin playfablogin;

    [Header("Purchase Sign")]
    public TextMeshPro purchaseSign;

    [Header("Cosmetic Visual")]
    public Transform point;
    private GameObject cosmetic;
    private GameObject Prefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HandTag")
        {
            if (playfablogin.coins >= coinsPrice)
            {
                if (PlayerPrefs.GetInt(CosmeticName) != 1)
                {
                    PlayerPrefs.SetInt(CosmeticName, 1);
                    BuyItem();
                }
                if (PlayerPrefs.GetInt(CosmeticName) == 1)
                {
                    enable.SetActive(true);
                    disable.SetActive(true);
                    if (disable2){
                        disable2.SetActive(true);
                    }
                    if (enable2){
                        enable2.SetActive(true);
                    }
                    gameObject.SetActive(false);
                }
            }
        }

    }



    public void BuyItem()
    {
        var request = new SubtractUserVirtualCurrencyRequest
        {
            VirtualCurrency = "TK",
            Amount = coinsPrice
        };
        PlayFabClientAPI.SubtractUserVirtualCurrency(request, OnSubtractCoinsSuccess, OnError);
    }

    void OnSubtractCoinsSuccess(ModifyUserVirtualCurrencyResult result)
    {
        Debug.Log("Bought item! " + CosmeticName);
        Playfablogin.instance.GetVirtualCurrencies();
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error: " + error.ErrorMessage);
    }

    private void Start()
    {

        Prefab = Resources.Load<GameObject>("Cosmetics/"+CosmeticName);
        cosmetic = (GameObject)GameObject.Instantiate(Prefab, point.position, Quaternion.identity);
        cosmetic.transform.parent = point;
        cosmetic.SetActive(true);

        if (HeadCos){
            HeadCos.Cosmetic = CosmeticName;
        }
        if (BodyCos){
            BodyCos.Cosmetic = CosmeticName;
        }
        if (LeftCos){
            LeftCos.Cosmetic = CosmeticName;
        }
        if (RightCos){
            RightCos.Cosmetic = CosmeticName;
        }


        purchaseSign.text = "Buy-" + CosmeticName + " (" + coinsPrice + " TK)";

        if (PlayerPrefs.GetInt(CosmeticName) == 1)
        {
            enable.SetActive(true);
            disable.SetActive(true);
            if (disable2){
                disable2.SetActive(true);
            }
            if (enable2){
                enable2.SetActive(true);
             }
            gameObject.SetActive(false);
        }
    }
}