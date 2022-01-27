using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreUpgrades : MonoBehaviour
{
    
    private InventoryUpdate inventoryUpdate;
    private UIPrefabHolder uiPrefabHolder;
    private PlayerUpgradeController playerUpgradeController;

    private void Start()
    {
        playerUpgradeController = FindObjectOfType<PlayerUpgradeController>();
        uiPrefabHolder = FindObjectOfType<UIPrefabHolder>();
        inventoryUpdate = FindObjectOfType<InventoryUpdate>();
    }


    public void addUpgradeToInventory(GameObject upgrade)
    {
        if(upgrade.GetComponent<Upgrade>().upgradeType == Upgrade.UpgradeType.tower_speed)
        {
            inventoryUpdate.addUpgradeButton(uiPrefabHolder.UIUpgradeButton[0]);
        }
        if (upgrade.GetComponent<Upgrade>().upgradeType == Upgrade.UpgradeType.tower_damage)
        {
            inventoryUpdate.addUpgradeButton(uiPrefabHolder.UIUpgradeButton[1]);
        }
        if (upgrade.GetComponent<Upgrade>().upgradeType == Upgrade.UpgradeType.tower_cooldown)
        {
            inventoryUpdate.addUpgradeButton(uiPrefabHolder.UIUpgradeButton[2]);
        }
        if (upgrade.GetComponent<Upgrade>().upgradeType == Upgrade.UpgradeType.tower_bulletSpeed)
        {
            inventoryUpdate.addUpgradeButton(uiPrefabHolder.UIUpgradeButton[3]);
        }
        if (upgrade.GetComponent<Upgrade>().upgradeType == Upgrade.UpgradeType.tower_extraShot)
        {
            inventoryUpdate.addUpgradeButton(uiPrefabHolder.UIUpgradeButton[6]);
        }
        if (upgrade.GetComponent<Upgrade>().upgradeType == Upgrade.UpgradeType.tower_spread)
        {
            inventoryUpdate.addUpgradeButton(uiPrefabHolder.UIUpgradeButton[7]);
        }
        if (upgrade.GetComponent<Upgrade>().upgradeType == Upgrade.UpgradeType.tower_explosive)
        {
            inventoryUpdate.addUpgradeButton(uiPrefabHolder.UIUpgradeButton[8]);
        }
        if (upgrade.GetComponent<Upgrade>().upgradeType == Upgrade.UpgradeType.tower_piercing)
        {
            inventoryUpdate.addUpgradeButton(uiPrefabHolder.UIUpgradeButton[9]);
        }
        if (upgrade.GetComponent<Upgrade>().upgradeType == Upgrade.UpgradeType.player_speed)
        {
            playerUpgradeController.Upgrade("player_speed");
        }
        if (upgrade.GetComponent<Upgrade>().upgradeType == Upgrade.UpgradeType.player_hp)
        {
            playerUpgradeController.Upgrade("player_hp");
        }

    }

    //public GameObject useUpgrade()
    //{
        
    //}
}
