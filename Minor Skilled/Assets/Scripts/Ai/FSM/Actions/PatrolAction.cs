using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : AiAction
{
    public override void Act(StateController controller)
    {
        _patrol(controller);
    }

    private void _patrol(StateController controller)
    {
        controller.Agent.destination = controller.Waypoints[controller.NextWayPoint].position;
        controller.Agent.isStopped = false;

        if(controller.Agent.remainingDistance <= controller.Agent.stoppingDistance && !controller.Agent.pathPending)
        {
            controller.NextWayPoint = (controller.NextWayPoint + 1) % controller.Waypoints.Length;
        }
    }
}
