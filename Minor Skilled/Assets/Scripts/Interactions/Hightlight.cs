using UnityEngine;

public class Hightlight : Interaction
{
	public enum Selected { SELECTED, DESELECTED };
	public Selected status = Selected.DESELECTED;
    public GameObject DispleyItem;
	public bool swapSelection = false;

	public override void Select()
    {
        status = Selected.SELECTED;
        if(!GetComponent<Player>().Info.isAi) DispleyItem.SetActive(true);
    }

    public override void Deselect()
    {
        status = Selected.DESELECTED;
        DispleyItem.SetActive(false);
    }

    private void Start()
    {
        Deselect();
    }

    private void Update()
    {
        _updateSelection();
    }

    private void _updateSelection()
    {
        if (swapSelection)
        {
            swapSelection = false;
            _switchSelection();
        }
    }

    private void _switchSelection()
    {
        if (status == Selected.DESELECTED) { Select(); }
        else if (status == Selected.SELECTED) { Deselect(); }
    }
}
