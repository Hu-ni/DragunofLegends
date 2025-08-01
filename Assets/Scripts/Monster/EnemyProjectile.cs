using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            // PlayerHealth 스크립트가 있어야 함
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            Destroy(gameObject); // 총알 제거
        }

        // 예: 벽과 부딪혀도 사라지게 하고 싶을 때
        if (other.gameObject.layer == LayerMask.NameToLayer("Level"))
        {
            Destroy(gameObject);
        }
    }
}