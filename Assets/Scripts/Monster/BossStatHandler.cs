using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BossStatHandler : MonoBehaviour
{
    [SerializeField]
    private float bossMaxHealth = 500;
    [SerializeField]
    private float bossCurrentHealth;
    public float BossCurrentHealth
    {
        get => bossCurrentHealth;
        set => bossCurrentHealth = Mathf.Clamp(value, 0, 500);
    }

    [SerializeField]
    private float bossChargeDamage = 30;
    public float BossChargeDamage => bossChargeDamage;

    [SerializeField]
    private float bossRangedDamage = 15;
    public float BossRangedDamage => bossRangedDamage;

    [SerializeField]
    private float bossContactDamage = 10;
    public float BossContactDamage => bossContactDamage;

    [SerializeField]
    private float bossprojectileSpeed = 10f;
    public float BossProjectileSpeed => bossprojectileSpeed;

    protected EnemyAnimationHandler enemyAnimationHandler;
    private bool isDead = false;
    private void Start()
    {
        BossCurrentHealth = bossMaxHealth;
        enemyAnimationHandler = GetComponent<EnemyAnimationHandler>();
    }

    public void TakeDamage(float damage)
    {
        BossCurrentHealth -= damage;
        Debug.Log("Player took " + damage + " damage. Current health: " + BossCurrentHealth);

        if (BossCurrentHealth <= 0)
        {
            Die();
        }


    }

    private void Die()
    {
        isDead = true;

        if (enemyAnimationHandler != null)
        {
            enemyAnimationHandler.Die();
        }

        // 몬스터의 움직임과 공격 중지
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }

        // 충돌체 비활성화
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        // 몬스터 오브젝트 파괴
        StartCoroutine(DestroyAfterAnimation());
    }

    private IEnumerator DestroyAfterAnimation()
    {
        float animationLength = GetAnimationClipLength("BossDie");

        if (animationLength <= 0f)
            animationLength = 2f; // 기본 대기 시간

        yield return new WaitForSeconds(animationLength);
        Destroy(gameObject);
    }

    private float GetAnimationClipLength(string clipName)
    {
        if (enemyAnimationHandler == null) return 0f;

        RuntimeAnimatorController controller = enemyAnimationHandler.AnimatorController;
        if (controller == null) return 0f;

        foreach (AnimationClip clip in controller.animationClips)
        {
            if (clip.name == clipName)
            {
                return clip.length;
            }
        }

        return 0f;
    }


}

