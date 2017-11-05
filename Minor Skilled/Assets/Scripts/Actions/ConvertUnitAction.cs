using UnityEngine;

public class ConvertUnitAction : ActionsBehaviour
{
    public override System.Action GetButtonClick()
    {
        return _processConvertation;
    }

    private void _processConvertation()
    {
        var player = GetComponent<Player>().Info;
        GameObject obj = new GameObject
        {
            name = typeof(FindConvertable).Name
        };
        var site = obj.AddComponent<FindConvertable>();
        site.Info = player;
        site.Prefab = UnitPrefab;
        site.Source = transform;
        active = obj;
    }

    protected void Update()
    {
        if (active == null) return;
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)) Destroy(active);
    }

    protected void OnDestroy()
    {
        if (active == null) return;
        Destroy(active);
    }
}
