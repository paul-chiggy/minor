using UnityEngine;
using System.Linq;
using UnityEngine.AI;
using System.Collections.Generic;

public class Util
{
    public static Vector3? ScreenPointToMapPosition(Collider col, Vector2 point)
	{
		var ray = Camera.main.ScreenPointToRay(point);
		RaycastHit hit;
		if (!col.Raycast(ray, out hit, Mathf.Infinity))
			return null;
		return hit.point;
	}

    public static Transform GetRandomPosAround(Transform spot, int xShifted, int zShifted) {
        spot.position = new Vector3(
            spot.position.x + Random.Range(-xShifted, xShifted),
            spot.position.y,
            spot.position.z + Random.Range(-zShifted, zShifted)
        );
        return spot;
    }

    public static bool isGameObjectsSafeToPlace(GameObject go)
    {
        var verts = go.GetComponent<MeshFilter>().mesh.vertices;
        var obstacles = GameObject.FindObjectsOfType<NavMeshObstacle>();
        var cols = new List<Collider>();
        foreach (var o in obstacles) {
            if (o.gameObject != go) {
                cols.Add(o.gameObject.GetComponent<Collider>());
            }
        }
        foreach (var v in verts) {
            NavMeshHit hit;
            //now find world position (currently all vertices have local model position)
            var vreal = go.transform.TransformPoint(v);
            NavMesh.SamplePosition(vreal, out hit, 20, NavMesh.AllAreas);
            bool onXAxis = Mathf.Abs(hit.position.x - vreal.x) < 0.5f;
            bool onZAxis = Mathf.Abs(hit.position.z - vreal.z) < 0.5f;
            bool hitCollider = cols.Any(c => c.bounds.Contains(vreal));
            if (!onXAxis || !onZAxis || hitCollider) return false;
        }
        return true;
    }

    public static float InvertMouseY(float y)
    {
        return Screen.height - y;
    }

    public static Vector3 GetSpotInsideUnitSphere(Transform unit, float range)
    {
        Vector3 pos = unit.position + (Random.insideUnitSphere * range);
        pos.y = Terrain.activeTerrain.SampleHeight(pos);
        return pos;
    }
}
