﻿using System;
using UnityEngine;

public class CreateUnitCommand : Command
{
    public CreateUnitCommand(Type type, PlayerInfo info, Transform spot)
    {
        this.MyType = type;
        this.info = info;
        this.spawnSpot = spot;
    }

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }

    public override GameObject Create()
    {
        if (MyType == typeof(Knight)) { return _createKnight(); }
        //insert else if condition with other options
        return null;
    }

    private GameObject _createKnight()
    {
        GameObject go = GameManager.Instance.ObjectPooler.GetPooledObject(ObjectPooler.UnitType.KNIGHT);
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
