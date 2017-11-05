using UnityEngine;

public class Tower : Vulnerable
{
    public float GoldPerSecond = 0.5f;

    public override void Die(GameObject go)
    {
        info.ActiveUnits.Remove(go);
        GameManager.Instance.ObjectPooler.PooledMap[ObjectPooler.UnitType.TOWER].Add(go);
        go.SetActive(false);
        if (go.GetComponent<Player>() != null) Destroy(go.GetComponent<Player>());
        if (go.GetComponent<ActionSelect>() != null) Destroy(go.GetComponent<ActionSelect>());
    }

    private void Update()
    {
        GameManager.Instance.ResourcesManager.SpendGold(info, GoldPerSecond);
    }
}
