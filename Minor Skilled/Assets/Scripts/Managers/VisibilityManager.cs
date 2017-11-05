using System.Collections.Generic;
using UnityEngine;

public class VisibilityManager : MonoBehaviour
{
    public float TimeBetweenChecks = 1.0f;
    public float VisibleRange = 100.0f;
    private float _waited = 10000.0f;
    public List<MapBlip> PBlips;
    public List<MapBlip> OBlips;

    private void Start()
    {
        PBlips = new List<MapBlip>();
        OBlips = new List<MapBlip>();
    }

    private void Update ()
    {
        _waited += Time.deltaTime;
        if (_waited <= TimeBetweenChecks) return;
        _waited = 0;

        _populateBlipLists(PBlips, OBlips);
        _updateVisibility(PBlips, OBlips);
	}

    private void _populateBlipLists(List<MapBlip> pBlips, List<MapBlip> oBlips)
    {
		foreach (var p in GameManager.Instance.Players)
		{
			foreach (var u in p.ActiveUnits)
			{
				var blip = u.GetComponent<MapBlip>();
				if (p == Player.Default) pBlips.Add(blip);
				else oBlips.Add(blip);
			}
		}
    }

    private void _updateVisibility(List<MapBlip> pBlips, List<MapBlip> oBlips) {
		foreach (var o in oBlips)
		{
			bool active = false;
			foreach (var p in pBlips)
			{
				var dist = Vector3.Distance(o.transform.position, p.transform.position);
				if (dist <= VisibleRange)
				{
					active = true;
					break;
				}
			}
			o.Blip.SetActive(active);
			foreach (var r in o.GetComponentsInChildren<Renderer>())
			{
				r.enabled = active;
			}
		}
    }
}
