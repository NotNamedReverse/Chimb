using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public float amt = 2;
    public GameObject cosmeticPoint;

    private GameObject currentEnabled;

    private float enabledNum = 2;

    public bool debugHit;

    private void Do(){
        foreach (Transform C in cosmeticPoint.transform){
            if (C.gameObject.activeInHierarchy){
                C.gameObject.SetActive(false);
                enabledNum = (float.Parse(C.name) + amt);

                if (enabledNum <= -1){
                    enabledNum = (cosmeticPoint.transform.childCount)-1;
                }

                if (enabledNum >= (cosmeticPoint.transform.childCount)){
                    enabledNum = 0;
                }

                Debug.Log(cosmeticPoint.transform.GetChild((int) (enabledNum)).GetChild(0).transform.name);
                if (PlayerPrefs.GetInt(cosmeticPoint.transform.GetChild((int) (enabledNum)).GetChild(0).transform.name) == 1){
                    break;
                } else{
                    enabledNum += 1;
                    cosmeticPoint.transform.GetChild((int) (enabledNum)).gameObject.SetActive(true);
                    //enabledNum += 1;
                }
                
            }
        } 

        Debug.Log("-----");

        Debug.Log(enabledNum);

        

        Debug.Log(enabledNum);

        cosmeticPoint.transform.GetChild((int) (enabledNum)).gameObject.SetActive(true);

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HandTag"){
            
            Do();
        }
    }

    private void Update(){
        if (debugHit){
            debugHit = false;
            Do();
        }
    }
}
