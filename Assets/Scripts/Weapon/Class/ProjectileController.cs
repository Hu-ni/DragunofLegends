using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private float speed;
    private int penetration;
    private float damage;
    private float critChance;
    private float critDamageMultiplier;

    private int hitCount = 0;
    private Vector3 direction;

    [SerializeField] private LayerMask targetLayer; // Enemy
    [SerializeField] private LayerMask wallLayer;   // Wall

    public void Init(Vector3 dir, float spd, int pen, float dmg, float critChance, float critMultiplier)
    {
        direction = dir.normalized;
        speed = spd;
        penetration = pen;
        damage = dmg;
        this.critChance = critChance;
        this.critDamageMultiplier = critMultiplier;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;

        // 벽과 적이 아닌 경우 무시
        if ((wallLayer.value & (1 << layer)) == 0 &&
            (targetLayer.value & (1 << layer)) == 0)
        {
            return;
        }

        // 벽에 닿으면 삭제
        if ((wallLayer.value & (1 << layer)) != 0)
        {
            Destroy(gameObject);
            return;
        }

        // 적에 닿았을 때
        float finalDamage = damage; // 기본 데미지
        bool isCrit = Random.value < critChance;

        if (isCrit)
        {
            finalDamage *= critDamageMultiplier; // 치명타일 경우 데미지 증가
            Debug.Log($"치명타 발생! 데미지: {finalDamage}");
            // 치명타 이펙트나 사운드도 여기서 가능
        }
        else
        {
            Debug.Log($"일반 공격. 데미지: {finalDamage}");
        }

        // 데미지 적용 (예: 적의 체력 감소 함수 호출)
        // collision.GetComponent<Enemy>()?.TakeDamage(finalDamage);

        hitCount++;
        if (hitCount > penetration)
        {
            Destroy(gameObject);
        }
    }
}
