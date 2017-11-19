using UnityEngine;
using System;

[Serializable]
public class StateVars
{
    public float LookingRange = 30f;
    public float AttackRange = 10f;
    public float AttackRate = 1f;
    public float SearchDuration = 1f;
    public float ScanRotationSpeed = 10f;
    public float ShootingSpeed = 30f;
    public Transform Eyes;
    public Transform[] Waypoints;
}
