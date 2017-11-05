using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ActionsManager : MonoBehaviour
{
    public Image BackgroundPic;
    public Button[] Buttons;
    private List<Action> actions = new List<Action>();

    private void Start()
    {
        for (int i = 0; i < Buttons.Length; i++) {
            var index = i;
            Buttons[index].onClick.AddListener(delegate () { OnButtonClick(index); });
        }
        ClearButtons();
    }

    public void ClearButtons()
    {
        if (Buttons != null && Buttons.Length > 0)
        {
            foreach (var b in Buttons) { b.gameObject.SetActive(false); }
			actions.Clear();
        }
        BackgroundPic.enabled = false;
    }

    public void AddButton(Sprite pic, Action onClick)
	{
		int index = actions.Count;
        Buttons[index].gameObject.SetActive(true);
        Buttons[index].GetComponent<Image>().sprite = pic;
        actions.Add(onClick);
        if (!BackgroundPic.enabled) BackgroundPic.enabled = true;
    }

    public void OnButtonClick(int index)
    {
        actions[index]();
    }
}
