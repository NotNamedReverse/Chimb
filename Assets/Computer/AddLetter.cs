using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLetter : MonoBehaviour
{
    public NameScript nameScript;
    public string Player;
    public string Letter;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == Player) 
        {
            nameScript.NameVar += Letter;
        }
    }
}
