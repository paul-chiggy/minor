using UnityEngine;

public class ShowUnitInfo : Interaction
{
    public Sprite UnitImage;
    private bool _show = false;
    private Vulnerable vul;
    private float _maxHealth = 0f;

    public override void Select()
    {
        _show = true;
    }

    private void Start()
    {
        vul = GetComponent<Vulnerable>();
        _maxHealth = vul.Health;
    }

    private void Update()
    {
        if (!_show) return;
        GameManager.Instance.UnitInfoManager.SetPic(UnitImage);
        GameManager.Instance.UnitInfoManager.SetText(
            this.gameObject.tag, 
            vul.Health + " | " + _maxHealth, 
            "Owner: " + GetComponent<Player>().Info.Name
        );
    }
	
    public override void Deselect()
    {
        _show = false;
        GameManager.Instance.UnitInfoManager.ClearText();
        GameManager.Instance.UnitInfoManager.ClearPic();
    }
}
