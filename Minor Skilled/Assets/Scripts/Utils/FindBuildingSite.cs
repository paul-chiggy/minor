using UnityEngine;

public class FindBuildingSite : Find
{
    public string BuildingType { get; set; }

    protected override void Start()
    {
        base.Start();
        MaxDistance = 30;
    }

    private void Update()
    {
        if(Info.Gold < Cost) { 
            Debug.Log("Insufficient funds to build this unit with price " + Cost);
            Destroy(this.gameObject); 
        }
        var tempTarget = Util.ScreenPointToMapPosition(GameManager.Instance.MouseManager.MapCollider, Input.mousePosition);
        if (tempTarget.HasValue == false) return;
        transform.position = new Vector3(tempTarget.Value.x, Prefab.transform.lossyScale.y, tempTarget.Value.z);
        if (Vector3.Distance(transform.position, Source.position) > MaxDistance)
        {
            _rend.material.color = red;
            return;
        }
        if (Util.isGameObjectsSafeToPlace(gameObject)) {
            _rend.material.color = green;
            if(Input.GetMouseButtonDown(0)) {
                switch(BuildingType) {
                    case "castle":
                        CreateBuildingCommand castle = new CreateBuildingCommand(typeof(Castle), Info, transform);
                        castle.Create();
                        break;
                    case "tower":
                        CreateBuildingCommand tower = new CreateBuildingCommand(typeof(Tower), Info, transform);
                        tower.Create();
                        break;
                    case "knight":
                        Vector3 tmp = Source.transform.position;
                        Source.transform.position = Util.GetSpotInsideUnitSphere(Source.transform, 20);
                        CreateUnitCommand knight = new CreateUnitCommand(typeof(Knight), Info, Source.transform);
                        knight.Create();
                        Source.transform.position = tmp;
                        break;
                    default:
                        throw new System.Exception("Correct type (tag) of building must be provided");
                }
                GameManager.Instance.ResourcesManager.ChargeCost(Info, Cost);
                Destroy(this.gameObject);
            }
        }
        else _rend.material.color = red;
    }

    private void OnDestroy()
    {
        GameManager.Instance.MouseManager.enabled = true;
    }
}
