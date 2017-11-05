﻿using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/ActiveState")]
public class ActiveStateDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool chaseTarget = controller.ChaseTarget.gameObject.activeSelf;
        return chaseTarget;
    }
}
