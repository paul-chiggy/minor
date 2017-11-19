using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Scan")]
public class ScanDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool noEnemyInSight = _scan(controller);
        return noEnemyInSight;
    }

    private bool _scan(StateController controller)
    {
        if (controller.Agent) { controller.Agent.isStopped = true; }
        controller.transform.Rotate(0, controller.Vars.ScanRotationSpeed * Time.deltaTime, 0);
        return controller.CheckIfCountdownElapsed(controller.Vars.SearchDuration);
    }
}
