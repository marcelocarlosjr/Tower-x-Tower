using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public int Cost;
    public enum UpgradeType
    {
        tower_speed,
        tower_damage,
        tower_cooldown,
        tower_bulletSpeed,
        tower_extraShot,
        tower_spread,
        tower_explosive,
        tower_piercing,
        player_speed,
        player_hp

    }

    public UpgradeType upgradeType;
}
