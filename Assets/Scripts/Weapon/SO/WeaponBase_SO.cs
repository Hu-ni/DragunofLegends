using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase_SO : ScriptableObject
{
    public float[] damages;
    public float[] cooldowns;
    public float[] criticalChances;

    public Sprite[] weaponSprites;
    public AnimationClip effectAnimation;

}
