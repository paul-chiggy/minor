using UnityEngine;

public abstract class Interaction : MonoBehaviour //decide whether or not it to be abstract
{
	public abstract void Select();
    public abstract void Deselect();
    private bool _isSelected { get; set; }

    protected void LateUpdate()
    {
        checkInSelection();
    }

    protected void checkInSelection()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 camPos = Camera.main.WorldToScreenPoint(transform.position);
            camPos.y = Util.InvertMouseY(camPos.y);
            _isSelected = MouseManager.Selection.Contains(camPos);

            if (_isSelected)
            {
                GameManager.Instance.MouseManager.Selections.Add(this);
                this.Select();
                _isSelected = false;
            }
        }
    }
}
