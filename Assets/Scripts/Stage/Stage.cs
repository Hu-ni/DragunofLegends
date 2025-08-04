using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static WaveInfo;

public class Stage : MonoBehaviour
{
    // 스테이지 이벤트 구현
    // 현재 일반 스테이지로 구현. 추후 추상 클래스 변경 후 확장
    
    // 몬스터 생성 및 웨이브
    [SerializeField]
    private List<SpawningPool> _spawningPools = new();
    [SerializeField]
    private List<WaveInfo> _waves = new();

    private Portal _portal;

    private int _aliveMonsters = 0;

    private void Start()
    {
        foreach(var pool in _spawningPools)
        {
            pool.Initialize(this);
        }
    }

    public void Execute()
    {
        StartCoroutine(ActiveWave());
    }

    public IEnumerator ActiveWave()
    {
        foreach (WaveInfo wave in _waves)
        {
            //몬스터 생성 
            foreach(SpawnInfo spawn in wave.SpawnSequance)
            {
                foreach (SpawningPool pool in _spawningPools)
                {
                    pool.GetMonster(spawn.Id, pool.gameObject.transform);
                    _aliveMonsters++;
                }
                GameManager.instance.UpdateMonsterCount(_aliveMonsters);
                yield return new WaitForSeconds(spawn.delayAfter);
            }
            yield return new WaitForSeconds(1f);    // 웨이브 끝나면 1초 대기
        }
    }

    public void UpdateMonsterCount()
    {
        _aliveMonsters -= 1;
        GameManager.instance.UpdateMonsterCount(_aliveMonsters);
    }

    public void ClearStage()
    {
        _portal.OnClear();
    }
}
