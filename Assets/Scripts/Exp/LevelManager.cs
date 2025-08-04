using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

public class LevelManager : MonoBehaviour
{
    // 몬스터가 드랍하는 경험치를 통해 레벨업
    // 최대 경험치 필요
    // 추가되는 경험치는 스테이지 난이도에 따라 정하기.
    // 경험치 오르는 양을 경험치 스크립트 없이 그냥?
    // 몬스터 당 1~4의 경험치 구슬 드랍
    // 
    private static LevelManager _instance;
    public static LevelManager Instance => _instance;

    private int _level;
    public int Level => _level;

    private int exp;

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this;
    }

    public void GainExp(int point)
    {
        int maxExp = GetExpToNextLevel();
        exp += point;

        if(maxExp <= exp)
        {
            LevelUP();
        }

        float percent = exp > 0 ? exp / maxExp : 0;
        UIManager.Instance.SetExp(percent);
    }

    public int GetExpToNextLevel()
    {
        double raw = Math.Pow(_level, 1.5) * 10; // 곱하기 10은 난이도 조절
        return Mathf.CeilToInt((float)raw);     // 올림 처리 후 int로 변환
    }

    public void LevelUP()
    {
        _level++;
        exp -= GetExpToNextLevel();
        UIManager.Instance.UpdateLevel(Level);
        UIManager.Instance.ShowPopupUI<LevelUpPopupUI>();
    }
}
