using System;
using UnityEngine;

public class CreateBuildingCommand : Command
{
    public CreateBuildingCommand(Type type, PlayerInfo info, Vector3 spot)
    {
        MyType = type;
        this.info = info;
        spawnSpot = spot;
    }

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }

    public override GameObject Create()
    {
        if (MyType == typeof(Castle)) { return _createCastle(); }
        if (MyType == typeof(Tower)) { return _createTower(); }
        return null;
    }

    private GameObject _createCastle()
    {
        GameObject go = GameManager.Instance.ObjectPooler.GetPooledObject(ObjectPooler.UnitType.CASTLE);
        if(go)
        {
            go.SetActive(true);
            Setup(go);
            go.transform.position = new Vector3(go.transform.position.x, go.transform.lossyScale.y/2, go.transform.position.z);
            info.ActiveUnits.Add(go);
        }
        return go;
    }

    private GameObject _createTower()
    {
        GameObject go = GameManager.Instance.ObjectPooler.GetPooledObject(ObjectPooler.UnitType.TOWER);
        if (go)
        {
            go.SetActive(true);
            Setup(go);
            go.transform.position = new Vector3(go.transform.position.x, go.transform.lossyScale.y, go.transform.position.z);
            info.ActiveUnits.Add(go);
        }
        return go;
    }
}
