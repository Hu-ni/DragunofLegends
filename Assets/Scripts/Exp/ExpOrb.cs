using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum ExpOrbType { Tiny, Small, Medium, Large, Huge }

public class ExpOrb : MonoBehaviour
{
    // 경험치 구슬
    public ExpOrbType type;
    // 경험치양
    private int _expPoint
    {
        get
        {
            switch (type)
            {
                case ExpOrbType.Huge:
                    return 40;
                case ExpOrbType.Large:
                    return 15;
                case ExpOrbType.Medium:
                    return 7;
                case ExpOrbType.Small:
                    return 3;
                case ExpOrbType.Tiny:
                default:
                    return 1;
            }
        }
    }

    private Transform _target;         // 흡수 대상 (플레이어)
    private bool _isAttracting = false;
    private float _moveSpeed = 8f;     // 자석 이동 속도

    private void Update()
    {
        if (_isAttracting && _target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _moveSpeed * Time.deltaTime);

            float distance = Vector3.Distance(transform.position, _target.position);
            if (distance < 0.5f)
            {
                LevelManager.Instance.GainExp(_expPoint);
                // 풀링이 있다면 ReturnOrb 사용
                ExpOrbPool.Instance.ReturnOrb(type, gameObject);
            }
        }
    }

    public void StartAttractToPlayer(Transform player)
    {
        _target = player;
        _isAttracting = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Constants.PlayerTag))
        {
            LevelManager.Instance.GainExp(_expPoint);
            ExpOrbPool.Instance.ReturnOrb(type, gameObject);
        }
    }
}
