using UnityEngine;

public class IdleAi : AiBehaviour
{
    public float ReturnWeight = 0.1f;

    public override float GetWeight()
    {
        return ReturnWeight;
    }

    public override void Execute()
    {
        //Debug.Log("Standing still, completely idle");
    }
}
