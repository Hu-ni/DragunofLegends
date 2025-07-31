using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Game/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public int[] level; //현재 레벨
    public WeaponType weaponType; //무기 종류
    public int[] weaponDamage; //무기공격력
    public float[] fireRate; //연사속도
    public float[] area;  //공격 범위
    public float[] range; //공격 사거리
    public int[] projectileNum; //투사체 수
    public int[] penetrateNum; //관통 횟수
    public float[] critRate; //치명타 확률
    public float[] critdamage; //치명타 데미지
}
