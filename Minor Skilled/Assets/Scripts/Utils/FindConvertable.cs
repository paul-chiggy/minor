using UnityEngine;

public class FindConvertable : Find
{
    private GameObject _convertable;
    public GameObject Convertable { get { return _convertable; } }

	protected override void Start ()
    {
        base.Start();
        MaxDistance = 3;
	}
	
	private void Update ()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        var tempTarget = Util.ScreenPointToMapPosition(GameManager.Instance.MouseManager.MapCollider, Input.mousePosition);
        if (tempTarget.HasValue == false) return;
        transform.position = new Vector3(tempTarget.Value.x, Prefab.transform.lossyScale.y, tempTarget.Value.z);
        var es = UnityEngine.EventSystems.EventSystem.current;
        if (es != null && es.IsPointerOverGameObject()) return;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit)) return;
        var player = hit.transform.GetComponent<Player>();
        if (player == null) { return; }
        if (player.Info.isAi) {
            _setupUnit(hit);
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.MouseManager.enabled = true;
    }

    private void _setupUnit(RaycastHit hit)
    {
        _convertable = hit.transform.gameObject;
        _convertable.AddComponent<ActionSelect>();
        _convertable.GetComponent<Player>().Info = Info;
        _convertable.GetComponent<Hightlight>().enabled = true;
        foreach (MeshRenderer rend in _convertable.GetComponent<Knight>().myColor)
        {
            rend.material.color = Info.KingdomColor;
        }
        var blip = _convertable.GetComponent<MapBlip>();
        GameManager.Instance.VisibilityManager.PBlips.Add(blip);
        GameManager.Instance.VisibilityManager.OBlips.Remove(blip);
    }
}
