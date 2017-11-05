using UnityEngine;

public class Castle : Vulnerable 
{
    public Transform spawnSpot; //TODO: later on should be managable via UI
    public float GoldPerSecond = 1.0f;

    private void Awake()
    {
        if (!spawnSpot) spawnSpot = this.transform;
    }

    public override void Die(GameObject go)
    {
        info.ActiveUnits.Remove(go);
        GameManager.Instance.ObjectPooler.PooledMap[ObjectPooler.UnitType.CASTLE].Add(go);
        go.SetActive(false);
        if(go.GetComponent<Player>() != null) Destroy(go.GetComponent<Player>());
        if (go.GetComponent<ActionSelect>() != null) Destroy(go.GetComponent<ActionSelect>());
    }

    private void Update()
    {
        GameManager.Instance.ResourcesManager.EarnGold(info, GoldPerSecond);
    }
}
