﻿using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Actions/Guard")]
public class GuardAction : AiAction
{
    public override void Act(StateController controller)
    {
        _guard(controller);
    }

    private void _guard(StateController controller)
    {
        
    }
}
