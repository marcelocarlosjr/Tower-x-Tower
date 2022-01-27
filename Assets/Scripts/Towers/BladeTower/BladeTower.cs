using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTower : Tower
{
    public GameObject bullet;
    public Transform pivot;
    public Transform barrel;
    public float towerSetupTime;

    private float tempSpeed1;


    private InputController ic;
    private void Start()
    {
        ic = GameObject.FindGameObjectWithTag("InputController").GetComponent<InputController>();
        towerType = TowerType.blade;
    }

    protected override void abilityOne()
    {
        StartCoroutine(abilityOneDurration());
        base.abilityOne();
    }

    private IEnumerator abilityOneDurration()
    {
        barrel.gameObject.GetComponent<Blade>().DOT = true;
        yield return new WaitForSeconds(10);
        barrel.gameObject.GetComponent<Blade>().DOT = false;
    }
    protected override void abilityTwo()
    {
        StartCoroutine(abilityTwoDurration());
        base.abilityTwo();
    }

    private IEnumerator abilityTwoDurration()
    {
        tempSpeed1 = shotDelay;
        shotDelay /= 3;
        yield return new WaitForSeconds(10);
        shotDelay = tempSpeed1;
    }
}
