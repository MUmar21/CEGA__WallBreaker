using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Following,
        Attacking,
    }

    private EnemyState currentState;
    [SerializeField] private float attackRange = 2.0f;
    [SerializeField] private Transform target;
    private NavMeshAgent agent;
    private float distanceToTarget;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= attackRange)
        {
            currentState = EnemyState.Attacking;
        }
        else
        {
            currentState = EnemyState.Following;
        }

        HandleState();
    }

    private void HandleState()
    {
        switch (currentState)
        {
            case EnemyState.Following:
                agent.isStopped = false;
                agent.SetDestination(target.position);
                Debug.Log("Following the target.");
                break;

            case EnemyState.Attacking:
                agent.isStopped = true;
                Debug.Log("Attacking the target.");
                break;
        }
    }
}
