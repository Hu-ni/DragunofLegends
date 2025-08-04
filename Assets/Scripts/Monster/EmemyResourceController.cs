using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f;

    private EnemyBaseController enemybasecontroller;
    private EnemyStatHandler enemystatHandler;
    private EnemyAnimationHandler enemyanimationHandler;

    private float timeSinceLastChange = float.MaxValue;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => enemystatHandler.Health;
    private bool isDead = false;
    private void Awake()
    {
        enemystatHandler = GetComponent<EnemyStatHandler>();
        enemyanimationHandler = GetComponent<EnemyAnimationHandler>();
        enemybasecontroller = GetComponent<EnemyBaseController>();
    }

    private void Start()
    {
        CurrentHealth = enemystatHandler.Health;
    }

    private void Update()
    {
        if (timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangeDelay)
            {
                enemyanimationHandler.InvincibilityEnd();
            }
        }
    }

    public bool ChangeHealth(float change) // 플레이어의 공격을 여기에 연결해서 몬스터한테 데미지
    {
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        timeSinceLastChange = 0f;
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        if (change < 0)
        {
            enemyanimationHandler.Damage();

        }

        if (CurrentHealth <= 0f)
        {
            Die();
        }

        return true;
    }

    private void Die()
    {
        isDead = true;

        if (enemyanimationHandler != null)
        {
            enemyanimationHandler.Die();
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
        float animationLength = GetAnimationClipLengthContains("die");

        if (animationLength <= 0f)
            animationLength = 2f; // 기본값

        yield return new WaitForSeconds(animationLength);
        Destroy(gameObject);
    }

    private float GetAnimationClipLengthContains(string keyword)
    {
        if (enemyanimationHandler == null) return 0f;

        RuntimeAnimatorController controller = enemyanimationHandler.AnimatorController;
        if (controller == null) return 0f;

        foreach (AnimationClip clip in controller.animationClips)
        {
            if (clip.name.ToLower().Contains(keyword.ToLower())) // 대소문자 무시하고 검색
            {
                return clip.length;
            }
        }

        return 0f;
    }

}