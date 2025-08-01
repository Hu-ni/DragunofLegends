using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage", menuName = "Stage/Stage")]
public class StageData : ScriptableObject
{
    [SerializeField]
    private StageType _type;
    [SerializeField]
    private GameObject _prefabs;
    [SerializeField]
    private List<GameObject> _monsters = new();
    [SerializeField]
    private int _monstersCount = 0;

    public StageType Type => _type;
    public GameObject Prefabs => _prefabs;
    public List<GameObject> Monsters => _monsters;
    public int MonsterCount => MonsterCount;
}
// 1. 게임 메니저를 통해 스테이지 시작 알림
// 2. 스테이지 활성화  --> StageManager
// 3. 몬스터 생성 --> Stage
//   - 생성 패턴은? 어떻게?
// 4. 웨이브 반복 --> Stage
// 5. 웨이브 종료 시 클리어 알림 게임 매니저에 전달
