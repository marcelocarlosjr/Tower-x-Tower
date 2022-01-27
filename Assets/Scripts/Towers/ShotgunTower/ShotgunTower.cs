using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunTower : Tower
{
    public GameObject bullet;
    public Transform pivot;
    public Transform barrel;
    public float towerSetupTime;

    private bool abilityTwoBuff;

    public bool reloaded;

    private InputController ic;
    private void Start()
    {
        ic = GameObject.FindGameObjectWithTag("InputController").GetComponent<InputController>();
        towerType = TowerType.cannon;
        reloaded = true;
    }

    protected override void abilityOne()
    {
        FindObjectOfType<AudioManager>().Play("towerShotgunReload");
        reloaded = true;
        base.abilityOne();
    }
    protected override void abilityTwo()
    {
        StartCoroutine(abilityTwoDurration());
        base.abilityTwo();
    }

    IEnumerator abilityTwoDurration()
    {
        abilityTwoBuff = true;
        yield return new WaitForSeconds(shotDelay + .05f);
        abilityTwoBuff = false;
    }

    protected override void shoot()
    {
        if (reloaded)
        {
            StartCoroutine(multishotDelay());
        }
    }
    private bool crRunning;
    IEnumerator multishotDelay()
    {
        crRunning = true;
        for (int i = 0; i < extraShot + 1; i++)
        {
            FindObjectOfType<AudioManager>().Play("towerShotgunShoot");
            Invoke("instantiateBullet", towerSetupTime);
            yield return new WaitForSeconds((shotDelay - (shotDelay / 1.5f)) / extraShot);
        }
        crRunning = false;
    }

    public void instantiateBullet()
    {
        if (TowerSelected || crRunning)
        {
            if (ic.MouseLClick() || crRunning)
            {
                //up
                GameObject newBullet1 = Instantiate(bullet, barrel.position, pivot.rotation * Quaternion.Euler(new Vector3(0, 0, 10)));
                newBullet1.GetComponent<Bullet>().bulletDamage = damage;
                newBullet1.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
                if (abilityTwoBuff)
                {
                    newBullet1.GetComponent<Bullet>().AOE = true;
                }
                if (piercingBullet)
                {
                    newBullet1.GetComponent<Bullet>().piercing = true;
                }

                //straight
                GameObject newBullet2 = Instantiate(bullet, barrel.position, pivot.rotation);
                newBullet2.GetComponent<Bullet>().bulletDamage = damage;
                newBullet2.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
                if (abilityTwoBuff)
                {
                    newBullet2.GetComponent<Bullet>().AOE = true;
                }
                if (piercingBullet)
                {
                    newBullet2.GetComponent<Bullet>().piercing = true;
                }

                //down
                GameObject newBullet3 = Instantiate(bullet, barrel.position, pivot.rotation * Quaternion.Euler(new Vector3(0, 0, -10)));
                newBullet3.GetComponent<Bullet>().bulletDamage = damage;
                newBullet3.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
                if (abilityTwoBuff)
                {
                    newBullet3.GetComponent<Bullet>().AOE = true;
                }
                if (piercingBullet)
                {
                    newBullet3.GetComponent<Bullet>().piercing = true;
                }
                if (spread > 0)
                {
                    for (int k = 4; k <= spread + 3; k++)
                    {
                        if (k % 2 != 0)
                        {
                            GameObject newBulletSpread = Instantiate(bullet, barrel.position, pivot.rotation * Quaternion.Euler(new Vector3(0, 0, (Mathf.Ceil((float)k / 2f) * 10))));
                            newBulletSpread.GetComponent<Bullet>().bulletDamage = damage;
                            newBulletSpread.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
                            if (piercingBullet)
                            {
                                newBulletSpread.GetComponent<Bullet>().piercing = true;
                            }
                        }
                        else if (k % 2 == 0)
                        {
                            GameObject newBulletSpread = Instantiate(bullet, barrel.position, pivot.rotation * Quaternion.Euler(new Vector3(0, 0, (Mathf.Ceil((float)k / 2f) * 10 * -1))));
                            newBulletSpread.GetComponent<Bullet>().bulletDamage = damage;
                            newBulletSpread.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
                            if (piercingBullet)
                            {
                                newBulletSpread.GetComponent<Bullet>().piercing = true;
                            }
                        }
                    }
                }

                reloaded = false;
            }
        }
    }
}

