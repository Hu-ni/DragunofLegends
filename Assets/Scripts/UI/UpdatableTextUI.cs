using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdatableTextUI : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Awake()
    {
        if (text == null)
        {
            text = GetComponent<TextMeshProUGUI>();
        }
    }


    public void UpdateText(string inText)
    {
        if (text == null) return;

        text.text = inText;
    }
}
