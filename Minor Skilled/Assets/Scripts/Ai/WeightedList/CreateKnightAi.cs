using UnityEngine;

public class CreateKnightAi : AiBehaviour
{
    public int KnightsPerCastle = 5;
    public int TriesPerCastle = 3;
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
        GameObject go = null;
        foreach (var u in _support.Castles)
        {
            for (int i = 0; i < TriesPerCastle; i++)
            {
                Vector3 tempPos = Util.GetSpotInsideUnitSphere(u.transform, RangeFromCastle);
                Debug.Log(_support.Info.Name + " is creating a knight at " + tempPos);
                CreateUnitCommand knight = new CreateUnitCommand(typeof(Knight), _support.Info, tempPos);
                go = knight.Create();
                if (Util.isGameObjectsSafeToPlace(go)) return;
            }
        }
        if (go) go.GetComponent<Knight>().Die(go);
    }
}
