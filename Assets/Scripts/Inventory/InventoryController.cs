using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public StoreTower towerHolder;

    public GameObject autoTower;
    public GameObject sniperTower;
    public GameObject freezeTower;
    public GameObject cannonTower;
    public GameObject fastTower;
    public GameObject bladeTower;
    public GameObject shotgunTower;
    public GameObject barrelTower;
    public GameObject supportTower;

    public List<GameObject> towerPickups;

    public List<GameObject> upgradePickups;

    private InputController ic;

    private UIPrefabHolder uiPrefabHolder;
    private void Start()
    {
        uiPrefabHolder = FindObjectOfType<UIPrefabHolder>();
        towerHolder = GameObject.FindGameObjectWithTag("TowerHolder").GetComponent<StoreTower>();
        ic = GameObject.FindGameObjectWithTag("InputController").GetComponent<InputController>();
    }

    private void Update()
    {
        if (ic.MousePosHit() && ic.MousePosHit().collider.gameObject.tag == "Ground")
        {
            onNumClick();
        }
    }

    public void onNumClick()
    {
        if (ic.Use(1))
        {
            towerHolder.userPlaceTower(1);
        }
        else if (ic.Use(2))
        {
            towerHolder.userPlaceTower(2);
        }
        else if (ic.Use(3))
        {
            towerHolder.userPlaceTower(3);
        }
        else if (ic.Use(4))
        {
            towerHolder.userPlaceTower(4);
        }
        else if (ic.Use(5))
        {
            towerHolder.userPlaceTower(5);
        }
    }

    public void StoreTower(string type)
    {
        if(type == "auto")
        {
            towerHolder.AddTower(autoTower, uiPrefabHolder.UITowerButton[1]);
        }

        if (type == "sniper")
        {
            towerHolder.AddTower(sniperTower, uiPrefabHolder.UITowerButton[8]);
        }

        if (type == "freeze")
        {
            towerHolder.AddTower(freezeTower, uiPrefabHolder.UITowerButton[6]);
        }

        if (type == "cannon")
        {
            towerHolder.AddTower(cannonTower, uiPrefabHolder.UITowerButton[4]);
        }

        if (type == "fast")
        {
            towerHolder.AddTower(fastTower, uiPrefabHolder.UITowerButton[5]);
        }

        if (type == "blade")
        {
            towerHolder.AddTower(bladeTower, uiPrefabHolder.UITowerButton[3]);
        }

        if (type == "shotgun")
        {
            towerHolder.AddTower(shotgunTower, uiPrefabHolder.UITowerButton[7]);
        }

        if (type == "barrel")
        {
            towerHolder.AddTower(barrelTower, uiPrefabHolder.UITowerButton[2]);
        }

        if (type == "support")
        {
            towerHolder.AddTower(supportTower, uiPrefabHolder.UITowerButton[0]);
        }
    }

}
