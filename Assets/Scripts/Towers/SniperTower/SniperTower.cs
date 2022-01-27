using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTower : Tower
{
    public GameObject bullet;
    public Transform pivot;
    public Transform barrel;
    public float towerSetupTime;
    private bool piercing;

    private InputController ic;
    private void Start()
    {
        ic = GameObject.FindGameObjectWithTag("InputController").GetComponent<InputController>();
        towerType = TowerType.sniper;
    }

    public IEnumerator damageIncreaseTimer()
    {
        float tempDamage = damage;
        damage *= 2.5f;
        yield return new WaitForSeconds(shotDelay);
        damage = tempDamage;
    }
    protected override void abilityOne()
    {
        StartCoroutine(damageIncreaseTimer());
    }
    public IEnumerator piercingTimer()
    {
        piercing = true;
        yield return new WaitForSeconds(shotDelay);
        piercing = false;
        
    }
    protected override void abilityTwo()
    {
        StartCoroutine(piercingTimer());
    }

    protected override void shoot()
    {
        StartCoroutine(multishotDelay());
    }
    private bool crRunning;
    IEnumerator multishotDelay()
    {
        crRunning = true;
        for (int i = 0; i < extraShot + 1; i++)
        {
            FindObjectOfType<AudioManager>().Play("towerSniperShoot");
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
                base.shoot();
                GameObject newBullet = Instantiate(bullet, barrel.position, pivot.rotation);
                newBullet.GetComponent<Bullet>().bulletDamage = damage;
                newBullet.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
                if (piercingBullet)
                {
                    newBullet.GetComponent<Bullet>().piercing = true;
                }
                if (piercing)
                {
                    newBullet.GetComponent<Bullet>().piercing = true;
                }
                if(spread > 0)
                {
                    for (int k = 1; k <= spread; k++)
                    {
                        if (k % 2 != 0)
                        {
                            GameObject newBullet1 = Instantiate(bullet, barrel.position, pivot.rotation * Quaternion.Euler(new Vector3(0, 0, (Mathf.Ceil((float)k / 2f) * 10))));
                            newBullet1.GetComponent<Bullet>().bulletDamage = damage;
                            newBullet1.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
                            if (piercingBullet)
                            {
                                newBullet1.GetComponent<Bullet>().piercing = true;
                            }
                        }
                        else if (k % 2 == 0)
                        {
                            GameObject newBullet1 = Instantiate(bullet, barrel.position, pivot.rotation * Quaternion.Euler(new Vector3(0, 0, (Mathf.Ceil((float)k / 2f) * 10 * -1))));
                            newBullet1.GetComponent<Bullet>().bulletDamage = damage;
                            newBullet1.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
                            if (piercingBullet)
                            {
                                newBullet1.GetComponent<Bullet>().piercing = true;
                            }
                        }
                    }
                }
            }
        }
    }
}

