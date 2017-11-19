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
        Debug.DrawRay(controller.Vars.Eyes.position, controller.Vars.Eyes.forward.normalized * controller.Vars.AttackRange, Color.red);
        if (Physics.SphereCast(controller.Vars.Eyes.position, 3f, controller.Vars.Eyes.forward, out hit, controller.Vars.AttackRange))
        {
            if (hit.collider.gameObject.GetComponent<Vulnerable>() != null)
            {
                var eInfo = hit.collider.gameObject.GetComponent<Player>().Info;
                bool allValid = (eInfo != null && eInfo.Name != controller.Info.Name);

                if (allValid && controller.CheckIfCountdownElapsed(controller.Vars.AttackRate))
                {
                    _fire(controller);
                    controller.StateTimeElapsed = 0;
                }
            }
        }
    }

    private void _fire(StateController controller)
    {
        GameObject bullet = GameManager.Instance.ObjectPooler.GetPooledObject(ObjectPooler.UnitType.BULLET);
        if (bullet)
        {
            bullet.transform.position = controller.Vars.Eyes.position + new Vector3(0,2,0); //ideally clamp to a circle turned towards enemy target
            bullet.transform.LookAt(controller.ChaseTarget);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * controller.Vars.ShootingSpeed;
            bullet.SetActive(true);
        }
        else Debug.Log("out of cannon bullets!");
    }
}
