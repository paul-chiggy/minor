using UnityEngine;

public class AttackAi : AiBehaviour
{
    public int KnightsRequired = 5;
    public float TimeDelay = 5.0f;
    public float SquadSize = 0.5f; // half of all available Mobiles
    public int IncreasePerWave = 1;

    public override float GetWeight()
    {
        if (TimePassed < TimeDelay) return 0;
        TimePassed = 0;
        var ai = AiSupport.GetSupport(this.gameObject);
        if (ai.Knights.Count < KnightsRequired) return 0;
        return 1;
    }

    public override void Execute()
    {
        var ai = AiSupport.GetSupport(this.gameObject);
        Debug.Log(ai.Info.Name + " is attacking");
        int wave = (int)(ai.Knights.Count * SquadSize);
        KnightsRequired += IncreasePerWave;
        foreach (var p in GameManager.Instance.Players)
        {
            if (p.isAi) continue;
            for (int i = 0; i < wave; i++)
            {
                var knight = ai.Knights[i];
                var nav = knight.GetComponent<RightClickNav>();
                var enemy = p.ActiveUnits[Random.Range(min: 0, max: p.ActiveUnits.Count)];
                if (nav != null) nav.SendToTarget(enemy.transform.position);
            }
            return;
        }
    }
}
