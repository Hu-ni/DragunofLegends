using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupUI : MonoBehaviour
{
    protected virtual void Start()
    {
        Canvas canvas = GetComponent<Canvas>();
        UIManager.Instance.InitCanvas(canvas);
    }


}
