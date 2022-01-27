using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTower : Tower
{
    public GameObject bullet;
    public Transform pivot;
    public Transform barrel;
    public float towerSetupTime;

    private bool Ability1Used;
    private bool Ability2Used;

    private InputController ic;
    private void Start()
    {
        ic = GameObject.FindGameObjectWithTag("InputController").GetComponent<InputController>();
        towerType = TowerType.cannon;
        piercingBullet = true;
    }

    protected override void abilityOne()
    {
        StartCoroutine(AbilityOneDurration());
        base.abilityOne();
    }
    public IEnumerator AbilityOneDurration()
    {
        Ability1Used = true;
        yield return new WaitForSeconds(shotDelay + .5f);
        Ability1Used = false;
    }
    protected override void abilityTwo()
    {
        StartCoroutine(bigCannonTimer());
    }

    public IEnumerator bigCannonTimer()
    {
        Ability2Used = true;
        yield return new WaitForSeconds(shotDelay);
        Ability2Used = false;
    }

    protected override void shoot()
    {
        StartCoroutine(multishotDelay());
    }

    public void instantiateBullet()
    {
        if (TowerSelected || crRunning)
        {
            if ((ic.MouseLClick() || crRunning) && !Ability2Used)
            {
                base.shoot();
                GameObject newBullet = Instantiate(bullet, barrel.position, pivot.rotation);
                newBullet.GetComponent<Bullet>().bulletDamage = damage;
                newBullet.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
                if (spread > 0)
                {
                    for (int k = 1; k <= spread; k++)
                    {
                        if (k % 2 != 0)
                        {
                            GameObject newBullet1 = Instantiate(bullet, barrel.position, pivot.rotation * Quaternion.Euler(new Vector3(0, 0, (Mathf.Ceil((float)k / 2f) * 10))));
                            newBullet1.GetComponent<Bullet>().bulletDamage = damage;
                            newBullet1.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
                            if (Ability1Used)
                            {
                                newBullet1.GetComponent<Bullet>().DOT = true;
                            }
                        }
                        else if (k % 2 == 0)
                        {
                            GameObject newBullet1 = Instantiate(bullet, barrel.position, pivot.rotation * Quaternion.Euler(new Vector3(0, 0, (Mathf.Ceil((float)k / 2f) * 10 * -1))));
                            newBullet1.GetComponent<Bullet>().bulletDamage = damage;
                            newBullet1.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
                            if (Ability1Used)
                            {
                                newBullet1.GetComponent<Bullet>().DOT = true;
                            }
                        }
                    }
                }
                if (Ability1Used)
                {
                    newBullet.GetComponent<Bullet>().DOT = true;
                }
            }
            else if((ic.MouseLClick() || crRunning) && Ability2Used)
            {
                base.shoot();
                GameObject newBullet = Instantiate(bullet, barrel.position, pivot.rotation);
                newBullet.GetComponent<Bullet>().bulletDamage = damage;
                newBullet.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
                if (Ability1Used)
                {
                    newBullet.GetComponent<Bullet>().DOT = true;
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
                            if (Ability1Used)
                            {
                                newBullet1.GetComponent<Bullet>().DOT = true;
                            }
                            newBullet1.transform.localScale = new Vector2(3, 3);
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
                            if (Ability1Used)
                            {
                                newBullet1.GetComponent<Bullet>().DOT = true;
                            }
                            newBullet1.transform.localScale = new Vector2(3, 3);
                            if (piercingBullet)
                            {
                                newBullet1.GetComponent<Bullet>().piercing = true;
                            }
                        }
                    }
                }
                newBullet.transform.localScale = new Vector2(3, 3);
                if (piercingBullet)
                {
                    newBullet.GetComponent<Bullet>().piercing = true;
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
            FindObjectOfType<AudioManager>().Play("towerCannonShoot");
            Invoke("instantiateBullet", towerSetupTime);
            yield return new WaitForSeconds((shotDelay - (shotDelay / 1.5f)) / extraShot);
        }
        crRunning = false;
    }
}
