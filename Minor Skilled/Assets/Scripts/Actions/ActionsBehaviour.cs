using System;
using UnityEngine;

public abstract class ActionsBehaviour : MonoBehaviour
{
    public abstract Action GetButtonClick();
    public Sprite ButtonPic;
    public GameObject UnitPrefab;
    public GameObject UnitGhostPrefab;
    protected GameObject active = null;
    public float MaxBuildingDistance = 30;
    public float UnitCost = 0;

    protected void processUnit()
    {
        var player = GetComponent<Player>().Info;
        var go = Instantiate(UnitGhostPrefab);
        var site = go.AddComponent<FindBuildingSite>();
        site.MaxDistance = MaxBuildingDistance;
        site.Prefab = UnitPrefab;
        site.Info = player;
        site.Source = transform;
        site.BuildingType = UnitPrefab.tag;
        site.Cost = UnitCost;
        active = go;
    }
}
