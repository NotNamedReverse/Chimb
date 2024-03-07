using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrapCollect : MonoBehaviour
{
    public string HandTag = "HandTag";

    public GameObject Main;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == HandTag)
        {
            GameObject manager = GameObject.FindWithTag("CurrencyManager");
            GameObject scrapManager = GameObject.FindWithTag("ScrapManager");

            if (manager && scrapManager){
                manager.GetComponent<CurrencyManager>().Collected += 1f;
                scrapManager.GetComponent<ScrapManager>().SpawnedScrap.Remove(Main);
                Destroy(Main);
            }
        }
    }
}
