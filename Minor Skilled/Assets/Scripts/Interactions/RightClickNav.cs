using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class RightClickNav : Interaction
{
    public float RelaxDistance = 5.0f;
    private PlayerInfo _info;
    private NavMeshAgent _agent;
    private Vector3 _target;
    private bool _selected = false;
    private bool _isActive = false;
    private List<Mobile> mobiles = new List<Mobile>();

    public override void Select()
    {
        _selected = true;
    }

    public override void Deselect()
    {
        _selected = false;
    }

    public void SendToTarget(Vector3 pos)
    {
        _target = pos;
        _sendToTarget();
    }

    private void _sendToTarget()
    {
        _agent.SetDestination(_target);
        _agent.isStopped = false;
        foreach (var m in GetComponents<Mobile>()) { mobiles.Add(m); }
        if (mobiles.Count == 1) { _agent.speed = mobiles[0].MovingSpeed; }
        _isActive = true;
        mobiles.Clear();
    }

	private void Start ()
    {
        _agent = GetComponent<NavMeshAgent>();
        _info = GetComponent<Player>().Info;
	}
	
	private void Update ()
    {
        if (_selected && Input.GetMouseButtonDown(1) && !_info.isAi) {
            var tempTarget = Util.ScreenPointToMapPosition(GameManager.Instance.MouseManager.MapCollider, Input.mousePosition);
            if(tempTarget.HasValue) {
                _target = tempTarget.Value;
                GameManager.Instance.MouseManager.RightClickPointer.transform.position = _target;
                GameManager.Instance.MouseManager.RightClickPointer.SetActive(true);
                _sendToTarget();
            }
        }

        if (_isActive && Vector3.Distance(_target, transform.position) < RelaxDistance) {
            _agent.isStopped = true;
            _isActive = false;
        }
	}
}
