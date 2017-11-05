using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    private List<Interaction> _selections = new List<Interaction>();
    public TerrainCollider MapCollider;
    public Texture2D SelectionHighlight;
    public static Rect Selection = new Rect(0, 0, 0, 0);
    private Vector3 _startClick = -Vector3.one;
    public List<Interaction> Selections { get { return _selections; } }
    public GameObject RightClickPointerPrefab;
    private GameObject _rightClickPointer;
    public GameObject RightClickPointer { get { return _rightClickPointer; } }

    private void Start()
    {
        _rightClickPointer = Instantiate(RightClickPointerPrefab);
        RightClickPointerPrefab.SetActive(false);
    }

    private void Update()
    {
        _checkDragging();
        _checkClick();
	}

    private void _checkClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        var es = UnityEngine.EventSystems.EventSystem.current;
        if (es != null && es.IsPointerOverGameObject()) return;
        _deselectAllUnnecessary();
        _selectVulnerable();
    }

    private void _checkDragging()
    {
        if (Input.GetMouseButtonDown(0)) { _startClick = Input.mousePosition; }
        else if (Input.GetMouseButtonUp(0)) { _startClick = -Vector3.one; }
        if (Input.GetMouseButton(0)) {
            Selection = new Rect(_startClick.x, Util.InvertMouseY(_startClick.y), Input.mousePosition.x - _startClick.x,
                                 Util.InvertMouseY(Input.mousePosition.y) - Util.InvertMouseY(_startClick.y));
            if (Selection.width < 0)
            {
                Selection.x += Selection.width;
                Selection.width = -Selection.width;
            }
            if (Selection.height < 0)
            {
                Selection.y += Selection.height;
                Selection.height = -Selection.height;
            }
        }
    }

    private void OnGUI()
    {
        if (_startClick != -Vector3.one) {
            GUI.color = new Color(1, 1, 1, 0.5f);
            GUI.DrawTexture(Selection, SelectionHighlight);
        }
    }

    private void _deselectAllUnnecessary()
    {
		if (_selections.Count > 0)
		{
			if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
			{
				foreach (var sel in _selections) { if (sel != null) sel.Deselect(); }
				_selections.Clear();
			}
		}
    }

    private void _selectVulnerable()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit)) return;
        var interacts = hit.transform.GetComponents<Interaction>();
        if (interacts == null) { return; }
        foreach (var interact in interacts)
        {
            _selections.Add(interact);
            interact.Select();
        }
    }
}
