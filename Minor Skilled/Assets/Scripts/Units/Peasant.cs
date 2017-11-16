using UnityEngine;

public class Peasant : Mobile
{
    public override void Die(GameObject go)
    {
        info.ActiveUnits.Remove(go);
        this.gameObject.GetComponent<MapBlip>().enabled = false;
        GameManager.Instance.ObjectPooler.PooledMap[ObjectPooler.UnitType.KNIGHT].Add(go);
        go.SetActive(false);
        if (go.GetComponent<RightClickNav>() != null) Destroy(go.GetComponent<RightClickNav>());
        if (go.GetComponent<Player>() != null) Destroy(go.GetComponent<Player>());
        if (go.GetComponent<ActionSelect>() != null) Destroy(go.GetComponent<ActionSelect>());
        if (!go.GetComponent<Hightlight>().enabled) go.GetComponent<Hightlight>().enabled = true;
    }
}
