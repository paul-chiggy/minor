using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    public State CurrentState;
    public State RemainState;
    public StateVars Vars;
    [HideInInspector] public NavMeshAgent Agent;
    [HideInInspector] public int NextWayPoint;
    [HideInInspector] public Transform ChaseTarget;
    [HideInInspector] public float StateTimeElapsed = 0;
    [HideInInspector] public PlayerInfo Info;
    private bool _isActive;

	void Start ()
    {
        Agent = GetComponent<NavMeshAgent>();
	}
	
	void Update ()
    {
        //if (!_isActive) return;
        if (Info == null) { Info = GetComponent<Player>().Info; }
        CurrentState.UpdateState(this);
	}

    private void OnDrawGizmos()
    {
        if(CurrentState != null && Vars.Eyes != null)
        {
            Gizmos.color = CurrentState.GizmoColor;
            Gizmos.DrawWireSphere(Vars.Eyes.position, 2f);
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
