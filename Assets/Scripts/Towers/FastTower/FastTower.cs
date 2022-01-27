using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastTower : Tower
{
    public GameObject bullet;
    public Transform pivot;
    public Transform barrel;
    public float towerSetupTime;
    public int successiveHits;
    public float damageBuff;
    public float addedDamage;

    public bool ability2Piercing;

    private InputController ic;
    private void Start()
    {
        ic = GameObject.FindGameObjectWithTag("InputController").GetComponent<InputController>();
        towerType = TowerType.fast;
    }

    protected override void abilityOne()
    {
            
    }
    protected override void abilityTwo()
    {
        StartCoroutine(piercingTimer());
        base.abilityTwo();
    }
    public IEnumerator piercingTimer()
    {
        ability2Piercing = true;
        yield return new WaitForSeconds(6);
        ability2Piercing = false;
    }

    protected override void Update()
    {
        damageBuff = successiveHits * 0.03f;
        addedDamage = damage + (damageBuff * damage);

        abilityOne();
        base.Update();
    }

    protected override void shoot()
    {
        StartCoroutine(multishotDelay());
    }

    public void instantiateBullet()
    {
        if (TowerSelected || crRunning)
        {
            if (ic.MouseLClick() || crRunning)
            {
                base.shoot();
                GameObject newBullet = Instantiate(bullet, barrel.position, pivot.rotation);
                newBullet.GetComponent<Bullet>().bulletDamage = damage + (damageBuff * damage);
                newBullet.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
                newBullet.GetComponent<Bullet>().originTower = this.gameObject;
                newBullet.GetComponent<Bullet>().piercing = ability2Piercing;
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
                            newBullet1.GetComponent<Bullet>().bulletDamage = damage + (damageBuff * damage);
                            newBullet1.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
                            newBullet1.GetComponent<Bullet>().originTower = this.gameObject;
                            newBullet1.GetComponent<Bullet>().piercing = ability2Piercing;
                            if (piercingBullet)
                            {
                                newBullet1.GetComponent<Bullet>().piercing = true;
                            }
                        }
                        else if (k % 2 == 0)
                        {
                            GameObject newBullet1 = Instantiate(bullet, barrel.position, pivot.rotation * Quaternion.Euler(new Vector3(0, 0, (Mathf.Ceil((float)k / 2f) * 10 * -1))));
                            newBullet1.GetComponent<Bullet>().bulletDamage = damage + (damageBuff * damage);
                            newBullet1.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
                            newBullet1.GetComponent<Bullet>().originTower = this.gameObject;
                            newBullet1.GetComponent<Bullet>().piercing = ability2Piercing;
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
    private bool crRunning;
    IEnumerator multishotDelay()
    {
        crRunning = true;
        for (int j = 0; j < extraShot + 1; j++)
        {
            FindObjectOfType<AudioManager>().Play("towerFastShoot");
            Invoke("instantiateBullet", towerSetupTime);
            yield return new WaitForSeconds((shotDelay - (shotDelay / 1.5f)) / extraShot);
        }
        crRunning = false;
    }
}
