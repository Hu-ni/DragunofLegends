using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    ProjectileWeapon Instigator { get; set; }

    float moveSpeed = 1f;
    Vector3 moveDir;
    int hitCount = 0;

    public void Initialize(ProjectileWeapon instigator, Vector3 dir)
    {
        Instigator = instigator;
        moveSpeed = Instigator.ProjectileSpeed;
        moveDir = dir.normalized;

    }


    private void FixedUpdate()
    {
        transform.position += moveDir * moveSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter");

        string layerName = LayerMask.LayerToName(collision.gameObject.layer);
        LayerMask layerMask = LayerMask.GetMask(layerName);

        if (layerMask != LayerMask.GetMask("Wall") && layerMask != LayerMask.GetMask("Enemy"))
        {
            return;
        }

        if (layerMask == LayerMask.GetMask("Wall"))
        {
            Debug.Log("Wall에 트리거");
            Destroy(gameObject);
            return;
        }

        Debug.Log("Enemy에 트리거");

        EnemyBaseController enemyBaseController = collision.GetComponent<EnemyBaseController>();
        if (enemyBaseController != null)
        {
            enemyBaseController.Death();
        }
        else
        {
            collision.GetComponent<BossStatHandler>().TakeDamage(Instigator.Damage);
        }

        // enemy character에서 데미지 계산

        hitCount++;
        if (hitCount > Instigator.PenetrationCount)
        {
            Destroy(gameObject);
        }
    }
}
