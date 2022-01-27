using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelector : MonoBehaviour
{
    private InputController ic;
    public GameObject currentSelectedTower;
    private StoreTower towerHolder;


    private void Start()
    {
        ic = GameObject.FindGameObjectWithTag("InputController").GetComponent<InputController>();
        towerHolder = FindObjectOfType<StoreTower>();
    }
    private void Update()
    {
        if (ic.MouseLClick() && ic.MousePosHit())
        {
            if (ic.MousePosHit().collider.tag == "Tower")
            {
                if (currentSelectedTower != null)
                {
                    currentSelectedTower.GetComponent<Tower>().TowerSelected = false;
                }

                currentSelectedTower = ic.MousePosHit().collider.gameObject;
                currentSelectedTower.GetComponent<Tower>().TowerSelected = true;

            }
        }

        if (ic.Use(1))
        {
            if (currentSelectedTower != null)
            {
                currentSelectedTower.GetComponent<Tower>().TowerSelected = false;
            }
            currentSelectedTower = towerHolder.tower1;
            currentSelectedTower.GetComponent<Tower>().TowerSelected = true;
        }

        if (ic.Use(2))
        {
            if (currentSelectedTower != null)
            {
                currentSelectedTower.GetComponent<Tower>().TowerSelected = false;
            }
            currentSelectedTower = towerHolder.tower2;
            currentSelectedTower.GetComponent<Tower>().TowerSelected = true;
        }

        if (ic.Use(3))
        {
            if (currentSelectedTower != null)
            {
                currentSelectedTower.GetComponent<Tower>().TowerSelected = false;
            }
            currentSelectedTower = towerHolder.tower3;
            currentSelectedTower.GetComponent<Tower>().TowerSelected = true;
        }

        if (ic.Use(4))
        {
            if (currentSelectedTower != null)
            {
                currentSelectedTower.GetComponent<Tower>().TowerSelected = false;
            }
            currentSelectedTower = towerHolder.tower4;
            currentSelectedTower.GetComponent<Tower>().TowerSelected = true;
        }

        if (ic.Use(5))
        {
            if (currentSelectedTower != null)
            {
                currentSelectedTower.GetComponent<Tower>().TowerSelected = false;
            }
            currentSelectedTower = towerHolder.tower5;
            currentSelectedTower.GetComponent<Tower>().TowerSelected = true;
        }

    }

    private void removeTower()
    {

    }


}
