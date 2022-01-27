using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSupportTower : Tower
{
    public GameObject bullet;
    public Transform pivot;
    public Transform barrel;
    public float towerSetupTime;

    private bool abilityOneBuff;
    private bool abilityTwoBuff;

    private void Start()
    {
        towerType = TowerType.support;
        abilityOneBuff = false;
    }

    protected override void shoot()
    {
        StartCoroutine(multishotDelay());
    }

    protected override void abilityOne()
    {
        StartCoroutine(abilityOneDurration());
        base.abilityOne();
    }

    public IEnumerator abilityOneDurration()
    {
        abilityOneBuff = true;
        yield return new WaitForSeconds(10);
        abilityOneBuff = false;
    }

    protected override void abilityTwo()
    {
        StartCoroutine(abilityTwoDurration());
        base.abilityOne();
    }
    public IEnumerator abilityTwoDurration()
    {
        abilityTwoBuff = true;
        yield return new WaitForSeconds(10);
        abilityTwoBuff = false;
    }

    public void instantiateBullet()
    {
        GameObject newBullet = Instantiate(bullet, barrel.position, pivot.rotation);
        newBullet.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
        newBullet.GetComponent<Bullet>().originTower = this.gameObject;
        if (piercingBullet)
        {
            newBullet.GetComponent<Bullet>().piercing = true;
        }
        if (spread > 0)
        {
            for (int k = 1; k <= spread; k++)
            {
                if (k % 2 != 0)
                {
                    GameObject newBullet1 = Instantiate(bullet, barrel.position, pivot.rotation * Quaternion.Euler(new Vector3(0, 0, (Mathf.Ceil((float)k / 2f) * 10))));
                    newBullet1.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
                    newBullet1.GetComponent<Bullet>().originTower = this.gameObject;
                    if (piercingBullet)
                    {
                        newBullet1.GetComponent<Bullet>().piercing = true;
                    }
                }
                else if (k % 2 == 0)
                {
                    GameObject newBullet1 = Instantiate(bullet, barrel.position, pivot.rotation * Quaternion.Euler(new Vector3(0, 0, (Mathf.Ceil((float)k / 2f) * 10 * -1))));
                    newBullet1.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
                    newBullet1.GetComponent<Bullet>().originTower = this.gameObject;
                    if (piercingBullet)
                    {
                        newBullet1.GetComponent<Bullet>().piercing = true;
                    }
                }
            }
        }
    }

    protected override void Update()
    {
        if (abilityOneBuff)
        {
            foreach (GameObject tower in TowerList.towerList)
            {
                tower.GetComponent<Tower>().autoTurretBuff(1.5f, 10, null);
            }
        }

        if (abilityTwoBuff)
        {
            foreach (GameObject tower in TowerList.towerList)
            {
                tower.GetComponent<Tower>().autoTurretBuffSpeed(1.33f, 10);
            }
        }


        base.Update();
    }

    IEnumerator multishotDelay()
    {
        for (int i = 0; i < extraShot + 1; i++)
        {
            Invoke("instantiateBullet", towerSetupTime);
            yield return new WaitForSeconds((shotDelay - (shotDelay / 2)) / extraShot);
        }

    }
}

