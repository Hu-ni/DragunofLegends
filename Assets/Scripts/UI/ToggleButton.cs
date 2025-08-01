using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    [SerializeField]
    Image frame;

    ToggleButtonList toggleButtonList;

    bool isSelected = false;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        if (frame == null)
        {
            frame = animator.GetComponent<Image>();
        }

        if (toggleButtonList == null)
        {
            toggleButtonList = GetComponentInParent<ToggleButtonList>();
        }
    }


    private void Start()
    {
        Color color = frame.color;
        color.a = 0f;
        frame.color = color;
    }


    public void OnClickToggleButton()
    {
        isSelected = !isSelected;
        animator.SetBool("IsSelected", isSelected);

        if (isSelected)
        {
            Color color = frame.color;
            color.a = 1f;
            frame.color = color;
        }
        else
        {
            Color color = frame.color;
            color.a = 0f;
            frame.color = color;
        }
        
    }


    public void TryClickToggleButton()
    {
        int index = transform.GetSiblingIndex();
        toggleButtonList.OnClickButtonChild(index);
    }

}
