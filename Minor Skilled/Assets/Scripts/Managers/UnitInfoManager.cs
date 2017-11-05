using UnityEngine;
using UnityEngine.UI;

public class UnitInfoManager : MonoBehaviour
{
    public Image UnitPic;
    public Image BackgroundPic;
    public Text Text1, Text2, Text3;
    public Text GoldCredits;

	private void Start ()
    {
        ClearText();
        ClearPic();
	}
	
	private void Update ()
    {
        _displayGoldAmount();
	}

    public void SetText(string t1, string t2, string t3)
    {
        Text1.text = t1;
        Text2.text = t2;
        Text3.text = t3;
    }

    public void ClearText()
    {
        SetText("", "", "");
    }

    public void SetPic(Sprite pic)
    {
        UnitPic.sprite = pic;
        UnitPic.color = Color.white;
        BackgroundPic.enabled = true;
    }

    public void ClearPic()
    {
        UnitPic.color = Color.clear;
        BackgroundPic.enabled = false;
    }

    private void _displayGoldAmount()
    {
        GoldCredits.text = "Gold: \n" + (int)Player.Default.Gold;
    }
}
