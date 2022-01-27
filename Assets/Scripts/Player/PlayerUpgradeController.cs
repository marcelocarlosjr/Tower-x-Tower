using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeController : MonoBehaviour
{
    public int player_speed;
    public int player_hp;

    public int money = 100;

    public void Upgrade(string type)
    {
        if(type == "player_hp")
        {
            player_hp++;
            GetComponent<PlayerHealthController>().addMaxHp(10);
        }
        if (type == "player_speed")
        {
            player_speed++;
        }
    }
}
