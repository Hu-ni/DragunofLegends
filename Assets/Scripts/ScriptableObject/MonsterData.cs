using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


[CreateAssetMenu(fileName ="Monster", menuName ="Stage/Monster")]
public class MonsterData : ScriptableObject
{
    [SerializeField]
    private int _id;
    [SerializeField]
    private GameObject _prefabs;

    public int Id => _id;
    public GameObject Prefabs => _prefabs;
}
