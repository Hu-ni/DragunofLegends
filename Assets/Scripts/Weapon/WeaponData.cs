using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Game/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public int[] Level; //레벨
    public int[] weaponDamage; //공격력
    public float[] fireRate; //공격속도
    public float[] area;  //공격범위
    public float[] range; //공격 사거리
    public int[] projectileNum; //투사체 수
    public int[] penetrateNum; //관통 횟수
    public float[] critRate; //치명타 확률
    public float[] critdamage; //치명타 데미지
}
