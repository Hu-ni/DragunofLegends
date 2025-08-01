using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

[CreateAssetMenu(fileName ="Stage", menuName ="Stage/Stage")]
public class Stage : ScriptableObject
{
    [SerializeField]
    private GameObject _stage;
    [SerializeField]
    private List<GameObject> _monsters = new();

    public GameObject Stg { get { return _stage; } }
    public List<GameObject> Monsters { get { return _monsters; } }


}
