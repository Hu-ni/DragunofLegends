using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeBar : MonoBehaviour
{
    [SerializeField]
    Image fillImage;
    [SerializeField, Range(0f, 1f)]
    float value;

    private void Awake()
    {
        SetValue(value);
    }

    public void SetValue(float value)
    {
        if (fillImage == null) return;

        value = Mathf.Clamp(value, 0f, 1f);
        fillImage.fillAmount = value;
    }

}
