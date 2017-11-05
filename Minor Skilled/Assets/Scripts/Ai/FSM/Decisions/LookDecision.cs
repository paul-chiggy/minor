using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetVisible = _look(controller);
        return targetVisible;
    }

    private bool _look(StateController controller)
    {
        RaycastHit hit;
        Debug.DrawRay(controller.Eyes.position, controller.Eyes.forward.normalized * controller.LookingRange, Color.red);
        if (Physics.SphereCast(controller.Eyes.position, 3f, controller.Eyes.forward, out hit, controller.LookingRange) 
            && hit.collider.CompareTag("knight"))
        {
            controller.ChaseTarget = hit.transform;
            return true;
        }
        else return false;
    }
}
