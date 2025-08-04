using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// 생성된 몬스터 풀링 클래스
public class SpawningPool : MonoBehaviour
{
    [SerializeField] private MonsterFactory _factory;

    // 풀링 큐
    private Dictionary<int,Queue<GameObject>> _pool = new();
    private Stage _stage;

    public void Initialize(Stage stage)
    {
        _stage = stage;
    }

    // 풀링 메소드
    public GameObject GetMonster(int id, Transform trans, int stageLevel)
    {
        if(!_pool.ContainsKey(id)) _pool[id] = new Queue<GameObject>();


        GameObject monster;

        if(_pool[id].Count > 0)
            monster = _pool[id].Dequeue();
        else
            monster = _factory.CreateMonster(id, this);

        EnemyBaseController enemy = monster.GetComponent<EnemyBaseController>();
        enemy.SetStageLevel(stageLevel);
        enemy.Spawn(trans);

        return monster;
    }

    // 만약 Wave를 각 스포닝풀마다 저장할경우
    // 현재 순서를 체크할 인덱스 생성
    // GetMosnter에 id를 제거한 후 WaveInfo 안에 있는 id만 사용
    public void ReturnMonster(int id, GameObject monster)
    {
        _pool[id].Enqueue(monster);
        _stage.UpdateMonsterCount();
    }
}
