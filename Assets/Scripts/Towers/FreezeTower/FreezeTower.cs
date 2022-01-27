using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTower : Tower
{
    public GameObject bullet;
    public Transform pivot;
    public Transform barrel;

    public float towerSetupTime;

    private void Start()
    {
        towerType = TowerType.freeze;
    }

    protected override void shoot()
    {
        StartCoroutine(multishotDelay());
    }

    protected override void abilityOne()
    {
        StartCoroutine("abilityOneTimer");
    }

    private IEnumerator abilityOneTimer()
    {
        this.transform.GetChild(2).gameObject.GetComponent<CircleCollider2D>().enabled = true;
        this.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        this.transform.GetChild(2).gameObject.GetComponent<CircleCollider2D>().enabled = false;
        this.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        base.abilityOne();
    }

    protected override void abilityTwo()
    {
        foreach (GameObject enemy in EnemyList.enemyList)
        {
            enemy.GetComponent<Enemy>().slow(.50f, 6);
        }
        base.abilityTwo();
    }

    public void instantiateBullet()
    {
        if ((Input.GetAxis("Fire1") > 0 && TowerSelected) || crRunning)
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

    private bool crRunning;
    IEnumerator multishotDelay()
    {
        crRunning = true;
        for (int j = 0; j < extraShot + 1; j++)
        {
            FindObjectOfType<AudioManager>().Play("towerFreezeShoot");
            Invoke("instantiateBullet", towerSetupTime);
            yield return new WaitForSeconds((shotDelay - (shotDelay / 1.5f)) / extraShot);
        }
        crRunning = false;
    }
}
