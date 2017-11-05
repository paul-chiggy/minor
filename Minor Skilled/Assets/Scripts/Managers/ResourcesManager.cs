using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public void ChargeCost(PlayerInfo player, float cost)
    {
        player.Gold -= cost;
    }

    public void EarnGold(PlayerInfo player, float amount)
    {
        player.Gold += amount * Time.deltaTime;
    }

    public void SpendGold(PlayerInfo player, float amoundPerSec)
    {
        player.Gold -= amoundPerSec * Time.deltaTime;
    }
}
