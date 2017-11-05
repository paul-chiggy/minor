using UnityEngine;
using System.Collections.Generic;

public class ObjectPooler
{
    private ObjectPoolSetup _setup;
    private Dictionary<UnitType, List<GameObject>> _pooledMap;
    private List<GameObject> _inactiveKnights;
    private List<GameObject> _inactiveCastles;
    private List<GameObject> _inactiveTowers;
    public Dictionary<UnitType, List<GameObject>> PooledMap { get { return _pooledMap; } }
    public enum UnitType { CASTLE, KNIGHT, TOWER } //add types here when expansion is needed

    public ObjectPooler()
    {
        _setupLists();
        _populatePooledLists();
    }

    private void _setupLists()
    {
        _setup = GameManager.Instance.ObjectPoolSetup;
        _inactiveKnights = new List<GameObject>();
        _inactiveCastles = new List<GameObject>();
        _inactiveTowers = new List<GameObject>();
        _pooledMap = new Dictionary<UnitType, List<GameObject>>();
        _pooledMap.Add(UnitType.KNIGHT, _inactiveKnights);
        _pooledMap.Add(UnitType.CASTLE, _inactiveCastles);
        _pooledMap.Add(UnitType.TOWER, _inactiveTowers);
    }

    private void _populatePooledLists()
    {
        for (int i = 0; i < _setup.castlesAmount; i++)
        {
            GameObject obj = MonoBehaviour.Instantiate(_setup.GetPooledPrefab("castle"));
            obj.SetActive(false);
            PooledMap[UnitType.CASTLE].Add(obj);
        }
        for (int j = 0; j < _setup.knightsAmount; j++)
        {
            GameObject obj = MonoBehaviour.Instantiate(_setup.GetPooledPrefab("knight"));
            obj.SetActive(false);
            PooledMap[UnitType.KNIGHT].Add(obj);
        }
        for (int j = 0; j < _setup.towersAmount; j++)
        {
            GameObject obj = MonoBehaviour.Instantiate(_setup.GetPooledPrefab("tower"));
            obj.SetActive(false);
            PooledMap[UnitType.TOWER].Add(obj);
        }
    }

    public GameObject GetPooledObject(UnitType type)
    {
        GameObject go = null;
        switch (type)
        {
            default: //default case is KNIGHT, so we do not mention KNIGHT case explicitly
                if (PooledMap[UnitType.KNIGHT].Count > 0)
                {
                    go = PooledMap[UnitType.KNIGHT][PooledMap[UnitType.KNIGHT].Count - 1];
                    PooledMap[UnitType.KNIGHT].Remove(go);
                    return go;
                }
                return null;
            case UnitType.CASTLE:
                if (PooledMap[UnitType.CASTLE].Count > 0)
                {
                    go = PooledMap[UnitType.CASTLE][PooledMap[UnitType.CASTLE].Count - 1];
                    PooledMap[UnitType.CASTLE].Remove(go);
                    return go;
                }
                return null;
            case UnitType.TOWER:                
                if (PooledMap[UnitType.TOWER].Count > 0)
                {
                    go = PooledMap[UnitType.TOWER][PooledMap[UnitType.TOWER].Count - 1];
                    PooledMap[UnitType.TOWER].Remove(go);
                    return go;
                }
                return null;
            // add cases here when new units pop up
        }
    }
}
