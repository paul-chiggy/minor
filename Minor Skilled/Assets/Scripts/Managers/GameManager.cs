﻿using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
    public List<PlayerInfo> Players = new List<PlayerInfo>();
    public ObjectPoolSetup ObjectPoolSetup;
    private ActionsManager _actionsManager;
    public ActionsManager ActionsManager { get { return _actionsManager; } }
    private MouseManager _mouseManager;
    public MouseManager MouseManager { get { return _mouseManager; } }
    private ObjectPooler _objectPooler;
    public ObjectPooler ObjectPooler { get { return _objectPooler; } }
    private UnitInfoManager _unitInfoManager;
    public UnitInfoManager UnitInfoManager { get { return _unitInfoManager; } }
    private VisibilityManager _visibilityManager;
    public VisibilityManager VisibilityManager { get { return _visibilityManager; } }
    private ResourcesManager _resourcesManager;
    public ResourcesManager ResourcesManager { get { return _resourcesManager; } }
    private AiController _aiController;
    public AiController AiController { get { return _aiController; } }
    private AiSupport _aiSupport;
    public AiSupport AiSupport { get { return _aiSupport; } }

    protected override void Awake()
    {
        base.Awake();
        _actionsManager = FindObjectOfType<ActionsManager>();
        _mouseManager = FindObjectOfType<MouseManager>();
        _objectPooler = new ObjectPooler();
        _unitInfoManager = FindObjectOfType<UnitInfoManager>();
        _visibilityManager = FindObjectOfType<VisibilityManager>();
        _resourcesManager = FindObjectOfType<ResourcesManager>();
        _aiController = FindObjectOfType<AiController>();
        _aiSupport = FindObjectOfType<AiSupport>();
    }

    private void Start()
    {
        _setupStartUnits();
    }

    private void _setupStartUnits()
    {
		foreach (var p in Players)
		{
			foreach (var u in p.StartingUnits)
			{
                GameObject obj = null;
                Vector3 spawnSpot = Util.GetSpotInsideUnitSphere(p.Location, 10);
                switch(u.gameObject.tag) {
					case "knight":
                        CreateUnitCommand knight = new CreateUnitCommand(typeof(Knight), p, spawnSpot);
                        obj = knight.Create();
                        break;
                    case "castle":
                        CreateBuildingCommand castle = new CreateBuildingCommand(typeof(Castle), p, spawnSpot);
                        obj = castle.Create();
                        break;
                    case "tower":
                        CreateBuildingCommand tower = new CreateBuildingCommand(typeof(Tower), p, spawnSpot);
                        obj = tower.Create();
                        break;
                    case "peasant":
                        CreateUnitCommand peasant = new CreateUnitCommand(typeof(Peasant), p, spawnSpot);
                        obj = peasant.Create();
                        break;
                    default:
                        throw new System.Exception("Object with valid tag must be proveded!");
                }
				if (obj == null) throw new System.Exception("Objectpooler returned null, something went wrong!");
            }
		}
    }
}
