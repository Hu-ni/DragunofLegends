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

    // 풀링 메소드
    public GameObject GetMonster(int id, Transform trans)
    {
        if(!_pool.ContainsKey(id)) _pool[id] = new Queue<GameObject>();


        GameObject monster;

        if(_pool[id].Count > 0)
            monster = _pool[id].Dequeue();
        else
            monster = _factory.CreateMonster(id, this);

        monster.GetComponent<EnemyBaseController>().Spawn(trans);
        return monster;
    }

    public void ReturnMonster(int id, GameObject monster)
    {
        _pool[id].Enqueue(monster);
    }
}
