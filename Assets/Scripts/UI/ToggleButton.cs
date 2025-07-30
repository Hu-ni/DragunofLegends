using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    ToggleButtonList toggleButtonList;

    bool isSelected = false;

    private void Awake()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        if (toggleButtonList == null)
        {
            toggleButtonList = GetComponentInParent<ToggleButtonList>();
        }
    }


    public void OnClickToggleButton()
    {
        isSelected = !isSelected;
        animator.SetBool("IsSelected", isSelected);
        
    }


    public void TryClickToggleButton()
    {
        int index = transform.GetSiblingIndex();
        toggleButtonList.OnClickButtonChild(index);
    }

}
