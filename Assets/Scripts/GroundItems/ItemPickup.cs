using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventoryController inventory;
    private StoreTower towerholder;
    private StoreUpgrades storeUpgrades;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryController>();
        towerholder = GameObject.FindGameObjectWithTag("TowerHolder").GetComponent<StoreTower>();
        storeUpgrades = inventory.gameObject.GetComponent<StoreUpgrades>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //upgrades
        if(other.GetComponent<Upgrade>())
        {
            if (other.GetComponent<Upgrade>().Cost <= transform.parent.GetComponent<PlayerUpgradeController>().money)
            {
                transform.parent.GetComponent<PlayerUpgradeController>().money -= other.GetComponent<Upgrade>().Cost;
                storeUpgrades.addUpgradeToInventory(other.gameObject);
                Object.Destroy(other.gameObject);
            }
        }

        //money
        if (other.GetComponent<Money>())
        {
            FindObjectOfType<AudioManager>().Play("collectMoney");
            transform.parent.GetComponent<PlayerUpgradeController>().money += other.GetComponent<Money>().Value;
            Object.Destroy(other.gameObject);
        }


        //towers
        if (other.gameObject.tag == "TowerPickup" && (towerholder.tower1 == null || towerholder.tower2 == null || towerholder.tower3 == null || towerholder.tower4 == null || towerholder.tower5 == null))
        {
            if (other.GetComponent<TowerType>().Cost <= transform.parent.GetComponent<PlayerUpgradeController>().money)
            {
                if (other.gameObject.GetComponent<TowerType>().towerType == TowerType.TowerTypes.auto)
                {
                    transform.parent.GetComponent<PlayerUpgradeController>().money -= other.GetComponent<TowerType>().Cost;
                    inventory.StoreTower("auto");
                    Object.Destroy(other.gameObject);
                }

                if (other.gameObject.GetComponent<TowerType>().towerType == TowerType.TowerTypes.freeze)
                {
                    transform.parent.GetComponent<PlayerUpgradeController>().money -= other.GetComponent<TowerType>().Cost;
                    inventory.StoreTower("freeze");
                    Object.Destroy(other.gameObject);
                }

                if (other.gameObject.GetComponent<TowerType>().towerType == TowerType.TowerTypes.sniper)
                {
                    transform.parent.GetComponent<PlayerUpgradeController>().money -= other.GetComponent<TowerType>().Cost;
                    inventory.StoreTower("sniper");
                    Object.Destroy(other.gameObject);
                }

                if (other.gameObject.GetComponent<TowerType>().towerType == TowerType.TowerTypes.fast)
                {
                    transform.parent.GetComponent<PlayerUpgradeController>().money -= other.GetComponent<TowerType>().Cost;
                    inventory.StoreTower("fast");
                    Object.Destroy(other.gameObject);
                }

                if (other.gameObject.GetComponent<TowerType>().towerType == TowerType.TowerTypes.shotgun)
                {
                    transform.parent.GetComponent<PlayerUpgradeController>().money -= other.GetComponent<TowerType>().Cost;
                    inventory.StoreTower("shotgun");
                    Object.Destroy(other.gameObject);
                }

                if (other.gameObject.GetComponent<TowerType>().towerType == TowerType.TowerTypes.cannon)
                {
                    transform.parent.GetComponent<PlayerUpgradeController>().money -= other.GetComponent<TowerType>().Cost;
                    inventory.StoreTower("cannon");
                    Object.Destroy(other.gameObject);
                }

                if (other.gameObject.GetComponent<TowerType>().towerType == TowerType.TowerTypes.gate)
                {
                    transform.parent.GetComponent<PlayerUpgradeController>().money -= other.GetComponent<TowerType>().Cost;
                    inventory.StoreTower("gate");
                    Object.Destroy(other.gameObject);
                }

                if (other.gameObject.GetComponent<TowerType>().towerType == TowerType.TowerTypes.blade)
                {
                    transform.parent.GetComponent<PlayerUpgradeController>().money -= other.GetComponent<TowerType>().Cost;
                    inventory.StoreTower("blade");
                    Object.Destroy(other.gameObject);
                }

                if (other.gameObject.GetComponent<TowerType>().towerType == TowerType.TowerTypes.support)
                {
                    transform.parent.GetComponent<PlayerUpgradeController>().money -= other.GetComponent<TowerType>().Cost;
                    inventory.StoreTower("support");
                    Object.Destroy(other.gameObject);
                }

                if (other.gameObject.GetComponent<TowerType>().towerType == TowerType.TowerTypes.barrel)
                {
                    transform.parent.GetComponent<PlayerUpgradeController>().money -= other.GetComponent<TowerType>().Cost;
                    inventory.StoreTower("barrel");
                    Object.Destroy(other.gameObject);
                }
            }
        }
    }
}
