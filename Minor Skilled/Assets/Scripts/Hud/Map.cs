using UnityEngine;

public class Map : MonoBehaviour
{
    public RectTransform ViewPort;
    public Transform Corner1, Corner2; //to define terrain corners
    public GameObject BlipPrefab;
    public static Map Current;
    private Vector2 _terrainSize;
    private RectTransform _mapRect;

    public Map() {
        Current = this;
    }

	private void Start ()
    {
        _terrainSize = new Vector2(Corner2.position.x - Corner1.position.x, Corner2.position.z - Corner1.position.z);
        _mapRect = GetComponent<RectTransform>();

	}
	
	private void Update ()
    {
        ViewPort.position = WorldPosToMap(Camera.main.transform.position);
	}

	public Vector2 WorldPosToMap(Vector3 point)
	{
		var pos = point - Corner1.position;
		var mapPos = new Vector2(
			pos.x / _terrainSize.x * _mapRect.rect.width,
            pos.z / _terrainSize.y * _mapRect.rect.height
		);
		return mapPos;
	}
}
