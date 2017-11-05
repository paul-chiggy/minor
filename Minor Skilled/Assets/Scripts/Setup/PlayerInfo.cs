using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo 
{
    public string Name;
    public Transform Location;
    public Color KingdomColor;
    public List<GameObject> StartingUnits = new List<GameObject>();
    public bool isAi;
    public float Gold;
    private List<GameObject> _activeUnits = new List<GameObject>();
    public List<GameObject> ActiveUnits {
        get { return _activeUnits; }
    }
}
