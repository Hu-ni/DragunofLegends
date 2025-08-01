using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class PlayerTracking : MonoBehaviour
{
    [SerializeField] public Transform _target;
    public bool canMove = true;
    NavMeshAgent _agent;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false; // Agent 가 Target 을 향해 이동할 때 방향을 회전할지
        _agent.updateUpAxis = false; // 캐릭터의 이동을 평면으로 제한하기 위해
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            _agent.SetDestination(_target.position); // Agent에게 target의 현재 위치로 이동하도록 지시
        }
    }
}
