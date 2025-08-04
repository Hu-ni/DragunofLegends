using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;
    public static PlayerManager Instance => _instance;

    [SerializeField]
    private AutoWeaponController _autoWeapon;
    [SerializeField]
    private PlayerHealth _health;

    private WeaponBase _currWeapon;

    void Awake()
    {
        if(Instance && Instance != this)
        {
            Destroy(Instance);
            return;
        }

        _instance = this;
    }

    private void Start()
    {
        _currWeapon = _autoWeapon.AddInitialWeapon(GameAlwaysData.Instance.SelectedWeapon);
    }

    public void LevelUpWeapon(int level)
    {
        Debug.Log("LevelUP");
        _currWeapon.SetWeaponStat(level);
    }

    public void OnRestHealth(int amount)
    {
        _health.CurrentHealth = _health.CurrentHealth + amount;
    }
}
