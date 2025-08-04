using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    int sortOrder = 10;
    Stack<PopupUI> popupStack = new Stack<PopupUI>();

    GameObject popupRoot;

    [SerializeField]
    private StageUI _stageUI;

    private void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;

        popupRoot = GameObject.Find("PopupRoot");
    }

    #region 팝업UI 메소드

    public void InitCanvas(Canvas canvas)
    {
        if (canvas == null)
            return;

        canvas.overrideSorting = true;

        canvas.sortingOrder = sortOrder;
        ++sortOrder;
    }

    public T ShowPopupUI<T>(string prefabName = null) where T : PopupUI
    {
        if (string.IsNullOrEmpty(prefabName))
        {
            prefabName = typeof(T).Name;
        }

        GameObject go = Resources.Load<GameObject>($"Prefabs/UI/{prefabName}");
        if (go == null)
        {
            Debug.Log("프리팹 경로 확인해주세요.");
            return null;
        }

        go = Instantiate(go);
        T popup = go.GetComponent<T>();
        popupStack.Push(popup);

        if (popupRoot == null)
        {
            popupRoot = new GameObject { name = "PopupRoot" };
        }
        
        go.transform.SetParent(popupRoot.transform);

        return popup;
    }

    public void ClosePopupUI()
    {
        if (popupStack.Count == 0)
            return;

        PopupUI popup = popupStack.Pop();
        Destroy(popup.gameObject);
        popup = null;
        --sortOrder;
    }
    #endregion

    public void InitStageUI()
    {
        _stageUI.Initialize();
    }

    public void UpdateMonsterCount(int count)
    {
        _stageUI.UpdateMonsterCount(count);
    }

    public void UpdateStageRound(int currentStageIdx)
    {
        Debug.Log(currentStageIdx);
        _stageUI.UpdateStageRound(currentStageIdx);
    }

    public void UpdateLevel(int level)
    {
        _stageUI.UpdateLevel(level);
    }
}
