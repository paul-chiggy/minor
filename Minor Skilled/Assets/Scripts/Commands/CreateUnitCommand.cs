using System;
using UnityEngine;

public class CreateUnitCommand : Command
{
    public CreateUnitCommand(Type type, PlayerInfo info, Vector3 spawnSpot)
    {
        this.type = type;
        this.info = info;
        this.spawnSpot = spawnSpot;
    }

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }

    public override GameObject Create()
    {
        if (type == typeof(Knight)) { return _createUnit(ObjectPooler.UnitType.KNIGHT); }
        if (type == typeof(Peasant)) { return _createUnit(ObjectPooler.UnitType.PEASANT); }
        //insert else if condition with other options
        return null;
    }

    private GameObject _createUnit(ObjectPooler.UnitType type)
    {
        GameObject go = GameManager.Instance.ObjectPooler.GetPooledObject(type);
        if (go)
        {
            go.SetActive(true);
            Setup(go);
            go.AddComponent<RightClickNav>();
            info.ActiveUnits.Add(go);
        }
        return go;
    }
}
