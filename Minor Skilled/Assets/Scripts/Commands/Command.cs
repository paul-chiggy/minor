using System;
using UnityEngine;

public abstract class Command
{
    public abstract void Execute();
    public abstract GameObject Create();
    protected Type type;
    protected Vector3 spawnSpot;
    protected PlayerInfo info;
    protected float cost;

    protected void Setup(GameObject go)
    {
        var player = go.AddComponent<Player>();
        player.Info = info;
        if (!info.isAi)
        {
            if (Player.Default == null) Player.Default = info;
            go.AddComponent<ActionSelect>();
        } else {
            go.GetComponent<Hightlight>().enabled = false;
        }
        go.transform.position = spawnSpot;
        go.transform.rotation = Quaternion.identity;
        go.GetComponent<MapBlip>().enabled = true;
    }
}
