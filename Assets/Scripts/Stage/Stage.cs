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

    public float spawnTiming = 0.2f;
    private float timecheck = 0f;

    //private void Update()
    //{
    //    timecheck += Time.deltaTime;
    //    if (timecheck >= spawnTiming)
    //    {
    //        foreach (SpawningPool pool in _spawningPools)
    //        {
    //            pool.GetMonster(0, pool.gameObject.transform);
    //        }
    //        timecheck = 0f;
    //    }
    //}

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
                }
                yield return new WaitForSeconds(spawn.delayAfter);
            }
            yield return new WaitForSeconds(1f);    // 웨이브 끝나면 1초 대기
        }
    }
}
