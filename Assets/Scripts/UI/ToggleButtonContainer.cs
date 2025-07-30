using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButtonList : MonoBehaviour
{
    List<ToggleButton> buttons = new List<ToggleButton>();
    int selectedIndex = -1;

    private void Awake()
    {
        if (buttons.Count != transform.childCount)
        {
            buttons.Clear();
            ToggleButton[] toggleButtons = GetComponentsInChildren<ToggleButton>();

            foreach (ToggleButton button in toggleButtons)
            {
                buttons.Add(button);
            }
        }
    }


    public void OnClickButtonChild(int index)
    {
        if (index < 0 || index >= buttons.Count)
            return;

        if (index != selectedIndex)
        {
            if (selectedIndex == -1)
            {
                selectedIndex = index;
                buttons[index].OnClickToggleButton();

            }
            else
            {
                buttons[selectedIndex].OnClickToggleButton();
                buttons[index].OnClickToggleButton();
                selectedIndex = index;
            }
        }
        else
        {
            buttons[selectedIndex].OnClickToggleButton();
            selectedIndex = -1;
        }
    }

}
