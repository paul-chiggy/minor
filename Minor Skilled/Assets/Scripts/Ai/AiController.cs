using UnityEngine;
using System.Collections.Generic;

public class AiController : MonoBehaviour
{
    private List<AiBehaviour> _ais = new List<AiBehaviour>();
    private float _waited = 0;
    public float Confusion = 0.3f;
    public float Frequency = 1;
    public string PlayerName;
    private PlayerInfo _info;
    public PlayerInfo Info { get { return _info; } }
    public enum AiType { WEIGHTED_LIST, FSM, BEHAVIOURAL_TREE }
    public AiType AiKind = AiType.WEIGHTED_LIST;
    public float Waited { get { return _waited; } set { _waited = value; } }

	private void Start ()
    {
        foreach(var ai in GetComponents<AiBehaviour>()) { _ais.Add(ai); }
        foreach(var p in GameManager.Instance.Players) { if (p.Name == PlayerName) { _info = p; } }
        gameObject.AddComponent<AiSupport>().Info = _info;
	}
	
    private void Update ()
    {
        _waited += Time.deltaTime;
        if(_waited < Frequency) return;
        AiSupport.GetSupport(gameObject).Refresh();
        _processAiController();
        _waited = 0;
	}

    private void _processAiController()
    {
        switch (AiKind)
        {
            case AiType.WEIGHTED_LIST:
                _processWeightedList();
                break;
            case AiType.FSM:
                //fsm.UpdateState();
                break;
            case AiType.BEHAVIOURAL_TREE:
                //process behavioural tree here
                break;
            default:
                throw new System.Exception("a valid AI type must be provided!");
        }
    }

    private void _processWeightedList()
    {
        float bestValue = float.MinValue;
        AiBehaviour bestAi = null;
        foreach (var ai in _ais)
        {
            ai.TimePassed += _waited;
            var aiValue = ai.GetWeight() * ai.WeightMultiplier + Random.Range(0, Confusion);
            //Debug.Log("ai " + ai.GetType().Name + " with weight " + aiValue);
            if (aiValue > bestValue)
            {
                bestValue = aiValue;
                bestAi = ai;
            }
        }
        bestAi.Execute();
    }
}
