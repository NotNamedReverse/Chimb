using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideShowTouch : MonoBehaviour
{
    public string Handtag;

    public GameObject toVisible;
    public GameObject toHide;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == Handtag)
        {
            toVisible.SetActive(true);
            toHide.SetActive(false);
        }
    }
}
