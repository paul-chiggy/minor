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
            var eInfo = hit.collider.gameObject.GetComponent<Player>().Info;
            bool allValid = (eInfo != null && eInfo.Name != controller.Info.Name);

            if (allValid && controller.CheckIfCountdownElapsed(controller.AttackRate))
            {
                _fire(controller);
            }
        }
    }

    private void _fire(StateController controller)
    {
        GameObject bullet = GameManager.Instance.ObjectPooler.GetPooledObject(ObjectPooler.UnitType.BULLET);
        if (bullet)
        {
            Debug.Log(controller.gameObject.tag + " | " + controller.gameObject.transform.position);
            bullet.transform.position = 
                Vector3.Lerp(controller.gameObject.transform.position, controller.ChaseTarget.position, 5 * Time.deltaTime);
            bullet.SetActive(true);
            Debug.Log("cannon is firing!");
        }
        else Debug.Log("out of cannon bullets!");
    }
}
