using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateKnightAi : AiBehaviour
{
    public int KnightsPerCastle = 5;
    public float RangeFromCastle = 15;
    public float Cost = 5;
    private AiSupport _support;

    public override float GetWeight()
    {
        if (_support == null) { _support = AiSupport.GetSupport(gameObject); }
        if (_support.Info.Gold < Cost) { return 0; }
        var knights = _support.Knights.Count;
        var castles = _support.Castles.Count;
        if (castles == 0) { return 0; }
        if (knights >= castles * KnightsPerCastle) { return 0; }
        return 1;
    }

    public override void Execute()
    {
        Debug.Log(_support.Info.Name + " is creating a knight");
        Vector3 tempPos = Util.GetSpotInsideUnitSphere(_support.Castles[Random.Range(0, _support.Castles.Count)].transform, RangeFromCastle);
        CreateUnitCommand knight = new CreateUnitCommand(typeof(Knight), _support.Info, tempPos);
        knight.Create();
    }
}
