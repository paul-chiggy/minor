using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Chase")]
public class ChaseAction : AiAction
{
    public override void Act(StateController controller)
    {
        _chase(controller);
    }

    private void _chase(StateController controller)
    {
        controller.Agent.destination = controller.ChaseTarget.position;
        controller.Agent.isStopped = false;
    }
}
