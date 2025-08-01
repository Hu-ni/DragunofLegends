using UnityEngine;
using UnityEngine.AI;

public class EnemyBaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField]
    private SpriteRenderer characterRenderer;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;

    protected EnemyAnimationHandler enemyanimationHandler;
    protected EnemyStatHandler enemystatHandler;

    protected bool isAttacking;
    private float timeSinceLastAttack = float.MaxValue;

    protected bool useAgentMovement = false;
    protected NavMeshAgent agent;

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
            // NavMeshAgent의 속도로 애니메이션 갱신
            enemyanimationHandler.Move(agent.velocity);
        }
    }

    protected virtual void FixedUpdate()
    {

        if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }

    protected virtual void HandleAction()
    {
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        characterRenderer.flipX = isLeft;
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

    public virtual void Death()
    {
        _rigidbody.velocity = Vector3.zero;

        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false;
        }

        Destroy(gameObject, 2f);
    }
}