using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/State")]
public class State : ScriptableObject
{
    public AiAction[] Actions;
    public Transition[] Transitions;
    public Color GizmoColor = Color.grey;

    public void UpdateState(StateController controller)
    {
        _doActions(controller);
        _checkTransitions(controller);
    }

    private void _doActions(StateController controller)
    {
        for (int i = 0; i < Actions.Length; i++)
        {
            Actions[i].Act(controller);
        }
    }

    private void _checkTransitions(StateController controller)
    {
        for (int i = 0; i < Transitions.Length; i++)
        {
            bool decisionSecceeded = Transitions[i].decision.Decide(controller);
            if(decisionSecceeded) {
                controller.TransitionToState(Transitions[i].TrueState);
            } else {
                controller.TransitionToState(Transitions[i].FalseState);
            }
        }
    }
}
