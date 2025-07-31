using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpPopupUI : PopupUI
{
    [SerializeField]
    Button[] buttons;


    private void Awake()
    {
        if (buttons.Length < 1)
        {
            buttons = new Button[3];
            buttons = GetComponentsInChildren<Button>();
        }
    }


    protected override void Start()
    {
        base.Start();

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            Button button = buttons[i];
            button.onClick.AddListener(() =>
            {
                Debug.Log($"{index + 1}번 째 버튼 클릭");
            });
        }
    }
}
