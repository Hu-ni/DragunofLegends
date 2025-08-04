using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWeaponController : MonoBehaviour
{
    // 유니티 에디터에서 할당할 모든 무기 프리팹들입니다.
    [SerializeField]
    private GameObject[] weaponPrefabs;

    // 현재 보유 중인(활성화된) 무기 목록
    private List<WeaponBase> activeWeapons = new List<WeaponBase>();
    private Dictionary<WeaponBase, Coroutine> attackCoroutines = new Dictionary<WeaponBase, Coroutine>();

    public float rotationRadius = 1.0f;
    private Transform weaponPivot;

    void Awake()
    {
        weaponPivot = transform.Find("weaponpivot");
        if (weaponPivot == null)
        {
            Debug.LogError("플레이어 오브젝트 하위에 'weaponpivot' 오브젝트를 찾을 수 없습니다.");
        }
    }

    /// <summary>
    /// 무기 프리팹을 인스턴스화하고 보유 무기 목록에 추가하는 메서드
    /// </summary>
    /// <param name="weaponPrefab">추가할 무기 프리팹</param>
    public WeaponBase AddWeapon(GameObject weaponPrefab)
    {
        if (weaponPivot != null && weaponPrefab != null)
        {
            GameObject weaponInstance = Instantiate(weaponPrefab, weaponPivot);
            WeaponBase weaponBase = weaponInstance.GetComponent<WeaponBase>();

            if (weaponBase != null)
            {
                activeWeapons.Add(weaponBase);
                // 추가된 무기에 대해 자동 공격 코루틴 시작
                Coroutine attackCoroutine = StartCoroutine(AutoAttack(weaponBase));
                attackCoroutines.Add(weaponBase, attackCoroutine);
            }
            return weaponBase;
        }
        else
        {
            Debug.LogError("weaponpivot 오브젝트를 찾을 수 없거나 프리팹이 null입니다.");
            return null;
        }
    }

    /// <summary>
    /// 특정 무기를 보유 무기 목록에서 제거하는 메서드
    /// </summary>
    /// <param name="weaponToRemove">제거할 무기</param>
    public void RemoveWeapon(WeaponBase weaponToRemove)
    {
        if (weaponToRemove != null && activeWeapons.Contains(weaponToRemove))
        {
            // 공격 코루틴 중지
            if (attackCoroutines.ContainsKey(weaponToRemove))
            {
                StopCoroutine(attackCoroutines[weaponToRemove]);
                attackCoroutines.Remove(weaponToRemove);
            }

            activeWeapons.Remove(weaponToRemove);
            Destroy(weaponToRemove.gameObject);
        }
    }

    /// <summary>
    /// 모든 보유 무기를 제거하는 메서드
    /// </summary>
    public void RemoveAllWeapons()
    {
        foreach (WeaponBase weapon in activeWeapons)
        {
            if (attackCoroutines.ContainsKey(weapon))
            {
                StopCoroutine(attackCoroutines[weapon]);
            }
            Destroy(weapon.gameObject);
        }
        activeWeapons.Clear();
        attackCoroutines.Clear();
    }




    // 이 메서드는 이제 게임 시작 시 선택된 무기를 추가하기 위한 예시입니다.
    public WeaponBase AddInitialWeapon(string weaponName)
    {
        GameObject prefabToAdd = null;
        foreach (var prefab in weaponPrefabs)
        {
            if (prefab.name == weaponName)
            {
                prefabToAdd = prefab;
                break;
            }
        }

        if (prefabToAdd != null)
        {
            return AddWeapon(prefabToAdd);
        }
        else
        {
            Debug.LogWarning($"'${weaponName}' 이름의 무기 프리팹을 찾을 수 없습니다.");
            return null;
        }
    }

    public WeaponBase FindWeaponInstanceByPrefab(GameObject prefab)
    {
        foreach (WeaponBase weapon in activeWeapons)
        {
            // 프리팹과 인스턴스의 이름이 같다고 가정하고 찾습니다.
            if (weapon.name.Replace("(Clone)", "") == prefab.name)
            {
                return weapon;
            }
        }
        return null;
    }


    private void Update()
    {
        RotateWeaponsTowardsMouse();
    }

    // 테스트용 함수
    private void RotateWeaponsTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        foreach (WeaponBase weapon in activeWeapons)
        {
            // ChasingWeapon은 마우스 방향에 따라 회전하지 않도록 제외
            if (weapon is MeleeWeapon || weapon is ProjectileWeapon)
            {
                if (weapon != null) // 혹시 모를 null 체크
                {
                    Transform weaponTransform = weapon.transform;
                    weaponTransform.rotation = Quaternion.Euler(0, 0, angle);
                    weaponTransform.position = (Vector2)transform.position + direction * rotationRadius;
                }
            }
        }
    }

    private IEnumerator AutoAttack(WeaponBase weapon)
    {
        while (true)
        {
            yield return new WaitForSeconds(weapon.cooldown);
            if (weapon != null)
            {
                weapon.Attack();
            }
        }
    }
}