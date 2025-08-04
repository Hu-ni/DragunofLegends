using UnityEngine;

public class AddWeaponObject : WeaponInteraction
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (autoWeaponController != null)
            {
                if (weaponPrefab != null)
                {
                    autoWeaponController.AddWeapon(weaponPrefab);
                    Destroy(gameObject); // 무기 획득 후 오브젝트 파괴
                }
                else
                {
                    Debug.LogError("AddWeaponObject에 Weapon Prefab이 할당되지 않았습니다!");
                    // 프리팹이 없으므로 오브젝트만 파괴
                    Destroy(gameObject);
                }
            }
            else
            {
                Debug.LogError("Player 오브젝트의 AutoWeaponController를 찾을 수 없습니다! Player에 'Player' 태그가 할당되었는지 확인하세요.");
                Destroy(gameObject);
            }
        }
    }
}