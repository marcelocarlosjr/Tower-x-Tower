using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TowerUIStatsUpdater : MonoBehaviour
{
    public GameObject tower;

    public Text damageText;
    public Text attackSpeed;
    public Text extraShot;
    public Text bulletSpeed;
    public Text spread;
    public Text CDR;
    public Text explosive;
    public Text piercing;
    public GameObject CDBOXPrefab;

    public GameObject ability1;
    public GameObject ability2;

    public GameObject abilityOneCD;
    public GameObject abilityTwoCD;

    public Text abilityOneCDText;

    private int round;

    private void Start()
    {
        round = 1;

        damageText = transform.GetChild(0).transform.GetChild(1).GetComponent<Text>();
        attackSpeed = transform.GetChild(1).transform.GetChild(1).GetComponent<Text>();
        extraShot = transform.GetChild(4).transform.GetChild(1).GetComponent<Text>();
        bulletSpeed = transform.GetChild(3).transform.GetChild(1).GetComponent<Text>();
        spread = transform.GetChild(5).transform.GetChild(1).GetComponent<Text>();
        CDR = transform.GetChild(2).transform.GetChild(1).GetComponent<Text>();
        explosive = transform.GetChild(6).transform.GetChild(1).GetComponent<Text>();
        piercing = transform.GetChild(7).transform.GetChild(1).GetComponent<Text>();

        
    }

    private void Update()
    {
        if (transform.parent.transform.parent.transform.GetChild(0).transform.GetChild(0).transform.childCount == 1)
        {
            tower = transform.parent.transform.transform.parent.GetChild(0).transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<OnButtonTowerClick>().Tower;
            if (transform.parent.transform.parent.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.childCount == 0)
            {
                ability1 = tower.GetComponent<Tower>().ability1;
                ability2 = tower.GetComponent<Tower>().ability2;
                Instantiate(ability1, transform.parent.transform.parent.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0));
                Instantiate(ability2, transform.parent.transform.parent.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1));

                abilityOneCD = Instantiate(CDBOXPrefab, transform.parent.transform.parent.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0));
                abilityOneCD.transform.parent.gameObject.AddComponent<FlexibleGridLayout>();

                abilityTwoCD = Instantiate(CDBOXPrefab, transform.parent.transform.parent.transform.GetChild(0).transform.GetChild(1).transform.GetChild(1).transform.GetChild(0));
                abilityTwoCD.transform.parent.gameObject.AddComponent<FlexibleGridLayout>();
            }

            if (transform.parent.transform.parent.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).transform.GetChild(0).childCount == 1)
            {
                if (tower.GetComponent<Tower>().AbilityOneCD > 0)
                {
                    abilityOneCD.transform.localScale = new Vector2(abilityOneCD.transform.localScale.x, tower.GetComponent<Tower>().abilityOneCDTimer / (tower.GetComponent<Tower>().AbilityOneCD * tower.GetComponent<Tower>().CD));
                }
                if (tower.GetComponent<Tower>().AbilityTwoCD > 0)
                {
                    abilityTwoCD.transform.localScale = new Vector2(abilityTwoCD.transform.localScale.x, tower.GetComponent<Tower>().abilityTwoCDTimer / (tower.GetComponent<Tower>().AbilityTwoCD * tower.GetComponent<Tower>().CD));
                }
                if (tower.GetComponent<Tower>().AbilityOneCD == 0)
                {
                    abilityOneCD.transform.localScale = new Vector2(abilityOneCD.transform.localScale.x, abilityOneCD.transform.localScale.y);
                }

                if(tower.GetComponent<Tower>().AbilityTwoCD == 0)
                {
                    abilityTwoCD.transform.localScale = new Vector2(abilityTwoCD.transform.localScale.x, abilityTwoCD.transform.localScale.y);
                }
            }

                if (tower.GetComponent<FastTower>())
            {
                damageText.text = Math.Round((double)tower.GetComponent<FastTower>().addedDamage, round).ToString();
            }
            else
            {
                damageText.text = Math.Round((double)tower.GetComponent<Tower>().damage, round).ToString();
            }
            attackSpeed.text = Math.Round((double)tower.GetComponent<Tower>().shotDelay, round).ToString();
            extraShot.text = Math.Round((double)tower.GetComponent<Tower>().extraShot, round).ToString();
            bulletSpeed.text = Math.Round((double)tower.GetComponent<Tower>().bulletSpeed, round).ToString();
            spread.text = Math.Round((double)tower.GetComponent<Tower>().spread, round).ToString();
            CDR.text = Math.Round((double)tower.GetComponent<Tower>().CD, round).ToString();
            explosive.text = Math.Round((double)tower.GetComponent<Tower>().explosive, round).ToString();

            if(tower.GetComponent<Tower>().piercingBullet == false)
            {
                piercing.text = "NO";
            }
            if(tower.GetComponent<Tower>().piercingBullet == true)
            {
                piercing.text = "YES";
            }
            
        }
    }
}
