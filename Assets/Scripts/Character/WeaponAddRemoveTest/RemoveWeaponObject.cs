using UnityEngine;

public class RemoveWeaponObject : WeaponInteraction
{
    // OnTriggerEnter2D 메서드에서 제거할 무기의 프리팹이 필요합니다.
    [SerializeField]
    private GameObject weaponPrefabToRemove;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (autoWeaponController != null && weaponPrefabToRemove != null)
            {
                // activeWeapons 목록에서 제거할 무기를 찾습니다.
                WeaponBase weaponToRemove = autoWeaponController.FindWeaponInstanceByPrefab(weaponPrefabToRemove);

                if (weaponToRemove != null)
                {
                    // 찾은 무기를 제거합니다.
                    autoWeaponController.RemoveWeapon(weaponToRemove);
                    Debug.Log($"{weaponToRemove.gameObject.name} 무기가 제거되었습니다.");
                    Destroy(gameObject);
                }
                else
                {
                    Debug.LogWarning($"제거하려는 무기({weaponPrefabToRemove.name})가 현재 플레이어에게 없습니다.");
                    Destroy(gameObject);
                }
            }
            else
            {
                Debug.LogError("AutoWeaponController 또는 weaponPrefabToRemove가 할당되지 않았습니다.");
                Destroy(gameObject);
            }
        }
    }
}