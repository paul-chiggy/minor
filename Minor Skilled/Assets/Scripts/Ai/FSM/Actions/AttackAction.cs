using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : AiAction
{
    public override void Act(StateController controller)
    {
        _attack(controller);
    }

    private void _attack(StateController controller)
    {
        RaycastHit hit;
        Debug.DrawRay(controller.Eyes.position, controller.Eyes.forward.normalized * controller.AttackRange, Color.red);
        if (Physics.SphereCast(controller.Eyes.position, 3f, controller.Eyes.forward, out hit, controller.AttackRange)
            && hit.collider.CompareTag("knight"))
        {
            if (controller.CheckIfCountdownElapsed(controller.AttackRate))
                Debug.Log("cannon is firing!");
        }
    }
}
