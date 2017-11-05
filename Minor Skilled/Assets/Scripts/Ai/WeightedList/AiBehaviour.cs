using UnityEngine;

public abstract class AiBehaviour : MonoBehaviour
{
    public abstract float GetWeight();
    public abstract void Execute();
    public float WeightMultiplier = 1;
    public float TimePassed = 0;
}
