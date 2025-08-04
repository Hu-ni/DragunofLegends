using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GameAlwaysData : MonoBehaviour
{
    private static GameAlwaysData _instance;
    public static GameAlwaysData Instance;

    public string SelectedWeapon { get; set; }


    private void Awake()
    {
        if(Instance && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(this);
    }
}
