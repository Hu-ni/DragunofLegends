using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ExpOrbPool : MonoBehaviour
{
    private static ExpOrbPool _instance;
    public static ExpOrbPool Instance => _instance;

    [SerializeField] private GameObject[] orbPrefabs; // Tiny ~ Huge
    private Dictionary<ExpOrbType, Queue<GameObject>> pool = new();

    private void Awake()
    {
        if (Instance && Instance != this)
        {
            Destroy(this);
            return;
        }

        _instance = this;

        foreach (ExpOrbType type in Enum.GetValues(typeof(ExpOrbType)))
        {
            pool[type] = new Queue<GameObject>();
        }
    }

    public GameObject GetOrb(ExpOrbType type)
    {
        if (pool[type].Count > 0)
        {
            var orb = pool[type].Dequeue();
            orb.SetActive(true);
            return orb;
        }
        else
        {
            return Instantiate(orbPrefabs[(int)type]);
        }
    }

    public void ReturnOrb(ExpOrbType type, GameObject orb)
    {
        orb.SetActive(false);
        pool[type].Enqueue(orb);
    }
}
