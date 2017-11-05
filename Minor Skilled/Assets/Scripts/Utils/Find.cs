using UnityEngine;
using UnityEngine.AI;

public class Find : MonoBehaviour
{
    protected Renderer _rend;
    protected Color red = Color.red;
    protected Color green = Color.green;
    public float MaxDistance = 0;
    public GameObject Prefab;
    public PlayerInfo Info;
    public Transform Source;
    protected bool _isActive = false;
    public float Cost = 0;

    protected virtual void Start()
    {
        _rend = GetComponent<Renderer>();
        GameManager.Instance.MouseManager.enabled = false;
    }
}
