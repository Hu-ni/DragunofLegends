using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName ="WaveInfo", menuName ="Stage/WaveInfo")]
public class WaveInfo : ScriptableObject
{
    // 웨이브 수
    // 몬스터 스폰 순서
    [System.Serializable]
    public struct SpawnInfo
    {
        public int Id;
        public float delayAfter;    // 다음 몬스터 딜레이 시간
    }

    [SerializeField]
    private List<SpawnInfo> _spawnSequance;
    [SerializeField]
    private bool _loop;

    public IReadOnlyList<SpawnInfo> SpawnSequance => _spawnSequance;
    public bool Loop => _loop;
}

