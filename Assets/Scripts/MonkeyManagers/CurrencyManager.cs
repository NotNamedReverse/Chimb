using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public float Collected = 0f;

    public TextMeshPro[] CollectedTexts;

    private void Update()
    {
        for (int index = 0; index < CollectedTexts.Length; index++)
        {
            CollectedTexts[index].text = "TODAYS'S COLLECTED SCRAP: " + Collected;
        }
    }
}
