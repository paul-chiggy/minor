using UnityEngine;

public class CreateCastleAi : AiBehaviour
{
    private AiSupport _support;
    public float Cost = 200;
    public float KnightsPerCastle = 5;
    public float RangeFromPeasant = 30;
    public int TriesPerPeasant = 3;

    public override float GetWeight()
    {
        if (_support == null) _support = AiSupport.GetSupport(gameObject);
        if (_support.Info.Gold < Cost || _support.Knights.Count == 0) return 0;
        if (_support.Castles.Count * KnightsPerCastle <= _support.Knights.Count) return 1;
        return 0;
    }

    public override void Execute()
    {
        Debug.Log("creating castle");
        GameObject go = null;
        foreach(var u in _support.Peasants)
        {
            for (int i = 0; i < TriesPerPeasant; i++)
            {
                Vector3 tempPos = Util.GetSpotInsideUnitSphere(u.transform, RangeFromPeasant);
                CreateBuildingCommand castle = new CreateBuildingCommand(typeof(Castle), _support.Info, tempPos);
                go = castle.Create();
                if(Util.isGameObjectsSafeToPlace(go)) return;
            }
        }
        if(go) go.GetComponent<Castle>().Die(go);
    }
}
