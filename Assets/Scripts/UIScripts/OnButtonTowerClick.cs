using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnButtonTowerClick : MonoBehaviour
{
    private StoreTower storeTower;
    public GameObject Tower;
    public GameObject cancelButton;

    public enum TowerType
    {
        auto,
        freeze,
        sniper,
        fast,
        shotgun,
        cannon,
        gate,
        support,
        barrel,
        blade
    }

    public TowerType towerType;

    private void Awake()
    {
        storeTower = FindObjectOfType<StoreTower>();
        cancelButton = FindObjectOfType<InventoryUpdate>().transform.GetChild(2).gameObject;
    }

    public void towerUpgradeButton()
    {
        storeTower.triggerTowerCurrent(towerType.ToString());
        if (storeTower.upgradeSelected && storeTower.currentSelectedUpgrade != "player_speed" && storeTower.currentSelectedUpgrade != "player_hp")
        {
            Tower.GetComponent<TowerUpgradeController>().Upgrade(storeTower.currentSelectedUpgrade);
            storeTower.currentSelectedUpgrade = null;
            Object.Destroy(storeTower.currentSelectedUpgradeButton);
            cancelButton.SetActive(false);
        }
    }

    public void setTower(GameObject tower)
    {
        Tower = tower;
    }

    private void Update()
    {
        
    }


}
