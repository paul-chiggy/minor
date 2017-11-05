using System.Collections.Generic;
using UnityEngine;

public class AiSupport : MonoBehaviour
{
    public List<GameObject> Knights = new List<GameObject>();
    public List<GameObject> Castles = new List<GameObject>();
    public List<GameObject> Towers = new List<GameObject>();
    public PlayerInfo Info;

    public static AiSupport GetSupport(GameObject go)
    {
        return go.GetComponent<AiSupport>();
    }

    public void Refresh()
    {
        Knights.Clear();
        Castles.Clear();
        Towers.Clear();
        foreach(var u in Info.ActiveUnits)
        {
            if (u.gameObject.tag == "knight") { Knights.Add(u); }
            if (u.gameObject.tag == "castle") { Castles.Add(u); }
            if (u.gameObject.tag == "tower") { Towers.Add(u); }
        }
    }
}
