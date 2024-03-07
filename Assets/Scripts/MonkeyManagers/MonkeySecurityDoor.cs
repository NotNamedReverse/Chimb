using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeySecurityDoor : MonoBehaviour
{
    private bool lastOpen = true;
    public bool doorOpen = true;

    public AudioSource doorSound;

    public float closeAmt;

    

    private IEnumerator moveDoor(bool isActive)
    {
        for (int i = 0; i < closeAmt; i++)
        {
            yield return new WaitForSeconds(0.1f);
            if (isActive){
                gameObject.transform.position += new Vector3(0,0.2f,0);
            }else{
                gameObject.transform.position -= new Vector3(0,0.2f,0);
            }
        }
    }

    void Update()
    {
        if (lastOpen != doorOpen)
        {
            lastOpen = doorOpen;

            Debug.Log("changed");
            doorSound.Play();
            StartCoroutine(moveDoor(doorOpen));
        }
    }
}
