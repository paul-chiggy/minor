using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    public State CurrentState;
    public State RemainState;
    public float LookingRange = 30f;
    public float AttackRange = 10f;
    public float AttackRate = 1f;
    public float SearchDuration = 1f;
    public float ScanRotationSpeed = 10f;
    public Transform Eyes;
    public Transform[] Waypoints;
    [HideInInspector] public NavMeshAgent Agent;
    [HideInInspector] public int NextWayPoint;
    [HideInInspector] public Transform ChaseTarget;
    [HideInInspector] public float StateTimeElapsed = 0;
    private bool _isActive;

	void Start ()
    {
        Agent = GetComponent<NavMeshAgent>();
	}
	
	void Update ()
    {
        //if (!_isActive) return;
        CurrentState.UpdateState(this);
	}

    private void OnDrawGizmos()
    {
        if(CurrentState != null && Eyes != null)
        {
            Gizmos.color = CurrentState.GizmoColor;
            Gizmos.DrawWireSphere(Eyes.position, 2f);
        }
    }

    public void TransitionToState(State nextState)
    {
        if(nextState != RemainState)
        {
            CurrentState = nextState;
        }
    }

    public bool CheckIfCountdownElapsed(float duration)
    {
        StateTimeElapsed += Time.deltaTime;
        return (StateTimeElapsed >= duration);
    }

    private void _onExitState()
    {
        StateTimeElapsed = 0;
    }
}
