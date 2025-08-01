using Enums;
using System.Collections.Generic;
using UnityEngine;

// 몬스터 생성 팩토리
public class MonsterFactory : MonoBehaviour
{
    [SerializeField] private List<MonsterData> monsterDataList;
    private Dictionary<int, GameObject> prefabMap;

    private void Awake()
    {
        prefabMap = new Dictionary<int, GameObject>();
        foreach (var data in monsterDataList)
        {
            prefabMap[data.Id] = data.Prefabs;
        }
    }

    public GameObject CreateMonster(int id, SpawningPool pool)
    {
        if (!prefabMap.ContainsKey(id))
        {
            Debug.LogError($"Monster prefab for {id} not found!");
            return null;
        }

        GameObject obj = Instantiate(prefabMap[id]);
        var controller = obj.GetComponent<EnemyBaseController>();
        controller?.Initialize(id, pool);
        return obj;
    }
}