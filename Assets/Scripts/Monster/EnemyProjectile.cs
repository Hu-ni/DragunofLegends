using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage;
    public void SetDamage(int damageValue)
    {
        damage = damageValue;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어 확인
        if (other.CompareTag(Constants.PlayerTag))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            Destroy(gameObject);
            return;
        }

        // 벽인지 확인
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject);
            return;
        }
    }
}