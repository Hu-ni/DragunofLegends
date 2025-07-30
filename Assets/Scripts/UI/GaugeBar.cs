using System.Collections;
using System.Collections.Generic;
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
    Transform textTransform;

    private void Awake()
    {
        if (hasIcon && icon == null)
        {
            Debug.LogWarning("IconSprite 연결 해주세요.");
            return;
        }
        
        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (iconTransform && textTransform)
            {
                break;
            }

            if (t.name == "Icon")
            {
                iconTransform = t;
                continue;
            }
            if (t.name == "Text")
            {
                textTransform = t; 
                continue;
            }
        }

        if (hasIcon)
        {
            iconTransform.GetComponent<Image>().sprite = icon;
        }

        SetValue(value);
    }


    private void OnEnable()
    {
        if (hasIcon)
        {
            textTransform.gameObject.SetActive(false);
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
        value = Mathf.Clamp(value, 0f, 1f);
        fillImage.fillAmount = value;
    }
}
