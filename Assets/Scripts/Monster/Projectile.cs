using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Player 태그와 충돌했을 때 반응
        if (other.CompareTag("Player"))
        {
            // 데미지 (Player 체력과 연결하기)
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            Destroy(gameObject);
        }

        // 벽과 부딪혀도 사라짐
        if (other.gameObject.layer == LayerMask.NameToLayer("Level"))
        {
            Destroy(gameObject);
        }
    }
}