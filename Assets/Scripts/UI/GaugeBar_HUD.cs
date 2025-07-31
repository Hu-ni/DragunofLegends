using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBar_HUD : MonoBehaviour
{
    [SerializeField]
    bool hasIcon;
    [SerializeField]
    Sprite icon;
    [SerializeField]
    GaugeBar gaugeBar;

    Transform iconTransform;
    UpdatableTextUI updatableText;


    private void Awake()
    {
        if (hasIcon && icon == null)
        {
            Debug.LogWarning("IconSprite 연결 해주세요.");
        }

        if (gaugeBar == null)
        {
            gaugeBar = GetComponentInChildren<GaugeBar>();
        }

        if (updatableText == null)
        {
            updatableText = GetComponentInChildren<UpdatableTextUI>();
        }

        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t.name == "Icon")
            {
                iconTransform = t;
                break;
            }
            
        }

        if (hasIcon)
        {
            iconTransform.GetComponent<Image>().sprite = icon;
        }

        updatableText.UpdateText("Lv.1");
    }


    private void OnEnable()
    {
        if (hasIcon)
        {
            updatableText.gameObject.SetActive(false);
        }
        else
        {
            iconTransform.gameObject.SetActive(false);
        }

    }
    
}
