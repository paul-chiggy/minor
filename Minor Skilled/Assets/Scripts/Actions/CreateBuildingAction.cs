using UnityEngine;

public class CreateBuildingAction : ActionsBehaviour
{
    public override System.Action GetButtonClick()
    {
        return processUnit;
    }

    protected void Update()
    {
        if (active == null) return;
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)) Destroy(active);
    }

    protected void OnDestroy()
    {
        if (active == null) return;
        Destroy(active);
    }
}

