using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticsTab : MonoBehaviour
{
    [Header("Stuff to enable")]

    public GameObject Buttons;
    public GameObject CosmeticPoint;
    
    [Header ("Other Buttons")]

    public GameObject OtherButtons1;
    public GameObject OtherButtons2;

    [Header("Other Cosmetic Points")]

    public GameObject OtherCosmeticPoint1;
    public GameObject OtherCosmeticPoint2;

    [Header("Debug")]

    public bool clicked;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HandTag"){
            OtherButtons1.SetActive(false);
            OtherButtons2.SetActive(false);

            OtherCosmeticPoint1.SetActive(false);
            OtherCosmeticPoint2.SetActive(false);

            Buttons.SetActive(true);
            CosmeticPoint.SetActive(true);
        }
    }

    private void Update(){
        if (clicked){
            clicked = false;
            OtherButtons1.SetActive(false);
            OtherButtons2.SetActive(false);

            OtherCosmeticPoint1.SetActive(false);
            OtherCosmeticPoint2.SetActive(false);

            Buttons.SetActive(true);
            CosmeticPoint.SetActive(true);
        }
    }
}
