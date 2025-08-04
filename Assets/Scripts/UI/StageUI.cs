using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageUI : MonoBehaviour
{
    private const string LevelFormat = "Lv.{0}";
    public TextMeshProUGUI MonsterCountText;
    public TextMeshProUGUI StageRoundText;
    public TextMeshProUGUI LevelText;

    public GaugeBar_HUD ExpBar;

    public void Initialize()
    {
        MonsterCountText.text = "0";
        LevelText.text = string.Format(LevelFormat, 1);
        StageRoundText.text = "1";
        ExpBar.SetValue(0f);
    }

    public void UpdateExpBar(float value)
    {
        ExpBar.SetValue(value);
    }

    public void UpdateLevel(int level)
    {
        LevelText.text = string.Format(LevelFormat,level);
    }

    public void UpdateMonsterCount(int count)
    {
        MonsterCountText.text = count.ToString();
    }

    public void UpdateStageRound(int round)
    {
        StageRoundText.text = round.ToString();
    }

}
