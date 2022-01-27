using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnButtonUpgradeClick : MonoBehaviour
{ 
    private StoreTower storeTower;
    public GameObject cancelButton;

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

    private void Awake()
    {
        storeTower = FindObjectOfType<StoreTower>();
        cancelButton = FindObjectOfType<InventoryUpdate>().transform.GetChild(2).gameObject;
    }

    public void sendUpgradeToHolder()
    {
        storeTower.replaceCurrentUpgrade(upgradeType.ToString(), this.gameObject);
        cancelButton.SetActive(true);
    }
}
