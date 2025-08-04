using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestPopupUI : PopupUI
{
    [SerializeField]
    Button yesButton;
    [SerializeField]
    Button noButton;

    private void Awake()
    {

        if (yesButton == null)
        {
            yesButton = transform.Find("YesButton").GetComponent<Button>();
        }

        if (noButton == null)
        {
            noButton = transform.Find("NoButton").GetComponent<Button>();
        }
    }


    protected override void Start()
    {
        base.Start();

        yesButton.onClick.AddListener(() =>
        {
            PlayerManager.Instance.OnRestHealth(20);
            UIManager.Instance.ClosePopupUI();
        });

        noButton.onClick.AddListener(() =>
        {
            UIManager.Instance.ClosePopupUI();
        });

    }
}
