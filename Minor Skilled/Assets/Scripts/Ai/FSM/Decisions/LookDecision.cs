using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return _look(controller);
    }

    private bool _look(StateController controller)
    {
        RaycastHit hit;
        Debug.DrawRay(controller.Vars.Eyes.position, controller.Vars.Eyes.forward.normalized * controller.Vars.LookingRange, Color.red);
        if (Physics.SphereCast(controller.Vars.Eyes.position, 3f, controller.Vars.Eyes.forward, out hit, controller.Vars.LookingRange) 
            && hit.collider.CompareTag("knight"))
        {
            controller.ChaseTarget = hit.transform;
            return true;
        }
        else return false;
    }
}
