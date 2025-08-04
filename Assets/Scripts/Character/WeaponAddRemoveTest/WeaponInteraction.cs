using UnityEngine;

public class WeaponInteraction : MonoBehaviour
{
    [SerializeField]
    protected GameObject weaponPrefab;

    protected AutoWeaponController autoWeaponController;

    private void Awake()
    {
        // Player 오브젝트의 AutoWeaponController를 찾습니다.
        // 태그나 이름으로 플레이어 오브젝트를 찾은 후 컴포넌트를 가져옵니다.
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            autoWeaponController = player.GetComponent<AutoWeaponController>();
        }
    }
}