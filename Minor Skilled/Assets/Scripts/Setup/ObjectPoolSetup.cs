using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolSetup
{
    public List<GameObject> PooledPrefabs;
    public int castlesAmount = 5;
    public int knightsAmount = 25;
    public int towersAmount = 5;

    public GameObject GetPooledPrefab(string myTag)
    {
        foreach (GameObject go in PooledPrefabs) { if (go.tag == myTag) return go; }
        return null;
    }
}
