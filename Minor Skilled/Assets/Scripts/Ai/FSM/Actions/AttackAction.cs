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
        if (Physics.SphereCast(controller.Eyes.position, 3f, controller.Eyes.forward, out hit, controller.AttackRange))
        {
            if (hit.collider.gameObject.GetComponent<Vulnerable>() != null)
            {
                var eInfo = hit.collider.gameObject.GetComponent<Player>().Info;
                bool allValid = (eInfo != null && eInfo.Name != controller.Info.Name);

                if (allValid && controller.CheckIfCountdownElapsed(controller.AttackRate))
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
            bullet.transform.position = controller.Eyes.position + new Vector3(0,2,0); //ideally clamp to a circle turned towards enemy target
            bullet.transform.LookAt(controller.ChaseTarget);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * controller.ShootingSpeed;
            bullet.SetActive(true);
        }
        else Debug.Log("out of cannon bullets!");
    }
}
