using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultPopupUI : PopupUI
{
    [SerializeField]
    UpdatableTextUI currentStageText;
    [SerializeField]
    UpdatableTextUI bestRecordText;
    [SerializeField]
    Button restartButton;
    [SerializeField]
    Button mainMenuButton;

    private void Awake()
    {

        if (currentStageText == null)
        {
            Transform panelTransform = transform.GetChild(1);
            currentStageText = panelTransform.Find("CurrentStageText").GetComponent<UpdatableTextUI>();

        }

        if (bestRecordText == null)
        {
            Transform panelTransform = transform.GetChild(1);
            bestRecordText = panelTransform.Find("BestRecordText").GetComponent<UpdatableTextUI>();

        }

        if (restartButton == null)
        {
            restartButton = transform.Find("RestartButton").GetComponent<Button>();
        }

        if (mainMenuButton == null)
        {
            mainMenuButton = transform.Find("MainMenuButton").GetComponent<Button>();
        }

    }
    protected override void Start()
    {
        base.Start();

        restartButton.onClick.AddListener(() =>
        {
            Debug.Log("게임 재시작 하도록 수정");
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("WeaponSelectScene");
        });
    }


    // 스테이지 끝날때 호출해야 함
    public void Initialize(int stage, int bestStage)
    {
        InitCurrentStage(stage);
        InitBestRecordText(stage, bestStage);

    }

    public void InitCurrentStage(int stage)
    {
        if (currentStageText == null)
            return;

        currentStageText.UpdateText(stage.ToString());
    }


    public void InitBestRecordText(int currentStage, int bestStage)
    {
        if (bestRecordText == null)
            return;

        string text;
        if (currentStage > bestStage)
        {
            text = "Best Record";
        }
        else
        {
            text = $"Try Harder";
        }
    }
}
