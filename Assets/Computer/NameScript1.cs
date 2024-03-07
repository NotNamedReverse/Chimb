using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.VR;
using TMPro;

public class NameScript : MonoBehaviour
{
    public string NameVar;
    public TextMeshPro NameText;

    private float blankTimer = 0f;
    private const float nameSetDelay = 5f;

    public void SetPlayerName()
    {
        NameVar = "TAMARIN" + UnityEngine.Random.Range(1000, 10000).ToString();

        UnityEngine.Debug.Log("Player Name: " + NameVar);
    }

    private void Update()
    {
        if (NameVar.Length > 12)
        {
            NameVar = NameVar.Substring(0, 12);
        }

        if (string.IsNullOrEmpty(NameVar) || NameVar.Length < 3)
        {
            blankTimer += Time.deltaTime;

            if (blankTimer >= nameSetDelay)
            {
                SetPlayerName();
                blankTimer = 0f;
            }
        }
        else
        {
            blankTimer = 0f;
        }

        NameText.text = NameVar;
        PhotonVRManager.SetUsername(NameVar);
    }
}