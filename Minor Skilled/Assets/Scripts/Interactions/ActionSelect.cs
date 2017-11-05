public class ActionSelect : Interaction
{
    public override void Deselect()
    {
        GameManager.Instance.ActionsManager.ClearButtons();
    }

    public override void Select()
    {
		GameManager.Instance.ActionsManager.ClearButtons();
        foreach (var ab in GetComponents<ActionsBehaviour>()) {
            GameManager.Instance.ActionsManager.AddButton(ab.ButtonPic, ab.GetButtonClick());
        }
	}
}
