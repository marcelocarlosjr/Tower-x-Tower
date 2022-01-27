using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTower : Tower
{
    public GameObject bullet;
    public Transform pivot;
    public Transform barrel;
    public float towerSetupTime;
    public int multishotAmmount;

    private void Start()
    {
        autoShoot = true;
        towerType = TowerType.auto;
    }

    protected override void shoot()
    {
         StartCoroutine(multishotDelay());
    }

    private float speedIncreaseLength = 5;
    public IEnumerator speedIncreaseTimer()
    {
        float tempShotDelay = shotDelay;
        shotDelay /= 1.5f;
        yield return new WaitForSeconds(speedIncreaseLength);
        shotDelay = tempShotDelay;
    }

    protected override void abilityOne()
    {
        StartCoroutine(speedIncreaseTimer());
    }
    protected override void abilityTwo()
    {
        StartCoroutine(abilityTwoDurration());
    }

    IEnumerator abilityTwoDurration()
    {
        float tempAttackDamage = damage;
        damage *= 1.5f;
        yield return new WaitForSeconds(5);
        damage = tempAttackDamage;
    }

    public void instantiateBullet()
    {
        if (base.autoShoot == true)
        {
            base.shoot();
            GameObject newBullet = Instantiate(bullet, barrel.position, pivot.rotation);
            newBullet.GetComponent<Bullet>().bulletDamage = damage;
            newBullet.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
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

        if (base.autoShoot == false)
        {
            if (Input.GetAxis("Fire1") > 0)
            {
                base.shoot();
                GameObject newBullet = Instantiate(bullet, barrel.position, pivot.rotation);
                newBullet.GetComponent<Bullet>().bulletDamage = damage;
                newBullet.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
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

    IEnumerator multishotDelay()
    {
        for (int i = 0; i < extraShot + 1; i++)
        {
            FindObjectOfType<AudioManager>().Play("towerAutoShoot");
            Invoke("instantiateBullet", towerSetupTime);
            yield return new WaitForSeconds((shotDelay - (shotDelay/2)) / extraShot);
        }
    }
}
