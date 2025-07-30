using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBar : MonoBehaviour
{
    [SerializeField]
    bool hasIcon;
    [SerializeField]
    Sprite icon;
    [SerializeField]
    Image fillImage;
    [SerializeField, Range(0f, 1f)]
    float value;

    Transform iconTransform;
    UpdatableTextUI updatableText;

    private void Awake()
    {
        if (hasIcon && icon == null)
        {
            Debug.LogWarning("IconSprite 연결 해주세요.");
            return;
        }
        
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (iconTransform && updatableText)
            {
                break;
            }

            if (t.name == "Icon")
            {
                iconTransform = t;
                continue;
            }
            if (t.name == "UpdatableText")
            {
                updatableText = t.GetComponent<UpdatableTextUI>(); 
                continue;
            }
        }

        if (hasIcon)
        {
            iconTransform.GetComponent<Image>().sprite = icon;
        }

        SetValue(value);
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public void SetValue(float value)
    {
        if (fillImage == null) return;

        value = Mathf.Clamp(value, 0f, 1f);
        fillImage.fillAmount = value;
    }


    
}
