using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInRange : MonoBehaviour
{
    public float FindTargetDelay = 1.0f;
    public float AttackRange = 10f;
    public float AttackFrequency = 0.25f;
    public float AttackDamage = 10.0f;
    private ShowUnitInfo _target;
    public PlayerInfo player;
    private float _findTargetCounter = 0;
    private float _attackCounter = 0;

	void Start ()
    {
        player = GetComponent<Player>().Info;
	}

    private void _findTarget()
    {
        if (_target != null) return;
        foreach (var p in GameManager.Instance.Players)
        {
            if (p == player) continue;
            foreach(var unit in p.ActiveUnits)
            {
                if(Vector3.Distance(unit.transform.position, transform.position) < AttackRange)
                {
                    _target = unit.GetComponent<ShowUnitInfo>();
                    return;
                }
            }
        }
    }

    private void _attack()
    {
        if (_target == null) return;
        if (Vector3.Distance(_target.transform.position, transform.position) > AttackRange)
        {
            _target = null;
            return;
        }
        Vulnerable vul = GetComponent<Vulnerable>();
        vul.TakeDamage(AttackDamage);
    }

    private void _updateFindTarget()
    {
        _findTargetCounter += Time.deltaTime;
        if (_findTargetCounter > FindTargetDelay)
        {
            _findTarget();
            _findTargetCounter = 0;
        }
    }

    private void _updateAttack()
    {
        _attackCounter += Time.deltaTime;
        if (_attackCounter > AttackFrequency)
        {
            _attack();
            _attackCounter = 0;
        }
    }
	
	void Update ()
    {
        _updateFindTarget();
        _updateAttack();
	}
}
