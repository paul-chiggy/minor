using UnityEngine;
using UnityEngine.UI;

public class MapBlip : MonoBehaviour
{
    private GameObject _blip;
    public GameObject Blip { get { return _blip; } }

	private void Start ()
    {
        _blip = (GameObject)Instantiate(Map.Current.BlipPrefab);
        _blip.transform.SetParent(Map.Current.transform);
        var color = GetComponent<Player>().Info.KingdomColor;
        _blip.GetComponent<Image>().color = color;
	}
	
	private void Update ()
    {
        _blip.transform.position = Map.Current.WorldPosToMap(transform.position);	
	}

    private void OnDestroy()
    {
        Destroy(_blip);
    }
}
