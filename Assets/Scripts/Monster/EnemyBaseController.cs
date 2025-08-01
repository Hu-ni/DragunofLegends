using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.PostProcessing;

public class EnemyBaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;

    protected EnemyAnimationHandler enemyanimationHandler;
    protected EnemyStatHandler enemystatHandler;

    protected bool isAgentMoving;
    protected bool isAttacking;

    protected bool useAgentMovement = true;
    protected NavMeshAgent agent;

    private int _id;
    private SpawningPool _pool;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        enemyanimationHandler = GetComponent<EnemyAnimationHandler>();
        enemystatHandler = GetComponent<EnemyStatHandler>();

        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
        HandleAttackDelay();

        if (useAgentMovement && agent != null)
        {
            if (knockbackDuration <= 0.0f && isAgentMoving)
            {
                enemyanimationHandler.Move(agent.velocity);
            }
            else
            {
                enemyanimationHandler.Move(Vector2.zero);
            }
        }
    }

    protected virtual void FixedUpdate()
    {

        if (knockbackDuration > 0.0f)
        {
            // 넉백 중일 때는 NavMeshAgent를 멈춤
            if (agent != null && agent.enabled)
            {
                agent.isStopped = true;
            }
            _rigidbody.velocity = knockback;
            knockbackDuration -= Time.fixedDeltaTime;
        }
        else
        {
            // 넉백이 끝나면 NavMeshAgent를 다시 활성화하고 Rigidbody 속도를 초기화
            if (agent != null && agent.enabled)
            {
                agent.isStopped = false;
            }
            _rigidbody.velocity = Vector2.zero;
        }
    }

    protected virtual void HandleAction()
    {
    }

    private void Rotate(Vector2 direction)
    {
        if (direction.x < 0)
        {
            characterRenderer.flipX = true; // 왼쪽
        }
        else if (direction.x > 0)
        {
            characterRenderer.flipX = false; // 오른쪽
        }
    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
    }

    private void HandleAttackDelay()
    {
    }

    protected virtual void Attack()
    {
    }

    public void Spawn(Transform pos)
    {
        transform.parent = pos;
        transform.position = pos.position;
        gameObject.SetActive(true);
    }

    public virtual void Death()
    {
        _rigidbody.velocity = Vector3.zero;
        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }
        // 20250801 - PH: 코루틴 처리
        StartCoroutine(ReturnAfterDelay());

        // 풀링 재사용으로 주석처리

        //foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        //{
        //    component.enabled = false;
        //}


        //Destroy(gameObject, 2f);
    }

    private IEnumerator ReturnAfterDelay()
    {
        // 2초 대기
        yield return new WaitForSeconds(2.0f);

        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 1f;
            renderer.color = color;
        }

        gameObject.SetActive(false);
        _pool.ReturnMonster(_id, gameObject);
    }

    public void Initialize(int id, SpawningPool pool)
    {
        _id = id;
        _pool = pool;
    }
}