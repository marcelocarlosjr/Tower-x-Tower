using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigUziVert : Boss
{
    public GameObject bullet;
    public GameObject bullet1;
    public GameObject bullet2;

    public bool meleeing;

    private float bullet1Rot;
    private float bullet2Rot;

    private int rotDir;

    public float fireSpeedAB1;

    public float rotSpeed;

    private float nextTimeToShoot1;

    public List<int> randSpaces;

    private BigUziAnimationController animator;
    private void Start()
    {
        nextTimeToShoot1 = Time.time;
        animator = GetComponent<BigUziAnimationController>();
        canShoot = false;
        StartCoroutine(spawn());
    }
    public bool spawning;
    IEnumerator spawn()
    {
        spawning = true;
        yield return new WaitForSeconds(10f);
        spawning = false;
    }

    protected override void shoot()
    {
        Invoke("instantiateBullet", fireSpeed);
    }

    private void abilityOneShoot()
    {
        Invoke("instantiateBullet1", fireSpeedAB1);
    }

    protected override void FixedUpdate()
    {
    }
    public override void Update()
    {
        if (!spawning)
        {
            if (!abilityOneOnCD && !meleeing && !abilityThreeBuff && !canShoot && !abilityTwoBuff)
            {
                abilityOne();
            }
            if (!abilityOneBuff && !abilityThreeOnCD && !meleeing && !abilityTwoBuff)
            {
                abilityThree();
            }
            if (!abilityOneBuff && !abilityTwoOnCD && !meleeing && !abilityThreeBuff)
            {
                abilityTwo();
            }

            if (!abilityOneBuff && !meleeing && !abilityThreeBuff && !abilityTwoBuff)
            {
                Invoke("shootAgain", 0.5f);
                if (canShoot)
                {
                    animator.shootUzi(lastDir);
                }
            }
            else
            {
                if (canShoot)
                {
                    canShoot = false;
                }
            }

            if (Time.time >= nextTimeToShoot1 && shootAbilityOne)
            {
                abilityOneShoot();
                nextTimeToShoot1 = Time.time + fireSpeedAB1;
            }
        }
        base.Update();
    }
    private void shootAgain()
    {
        canShoot = true;
    }

    public void instantiateBullet()
    {
        FindObjectOfType<AudioManager>().Play("towerFastShoot");
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.Euler(newRotation));
    }

    private int direction;
    public void instantiateBullet1()
    {
        if(rotDir == 0)
        {
            direction = 1;
        }
        else if(rotDir == 1)
        {
            direction = -1;
        }
        FindObjectOfType<AudioManager>().Play("towerFastShoot");
        GameObject newBullet1 = Instantiate(bullet1, transform.position, Quaternion.Euler(new Vector3(0, 0, 180 + Time.time * rotSpeed * direction)));
        GameObject newBullet2 = Instantiate(bullet1, transform.position, Quaternion.Euler(new Vector3(0, 0, 0 + Time.time * rotSpeed * direction)));
    }

    protected override void abilityOne()
    {
        abilityOneBuff = true;
        StartCoroutine(abilityOneDurration());
        base.abilityOne();
    }
    public bool abilityOneBuff;
    private bool shootAbilityOne;
    IEnumerator abilityOneDurration()
    {
        yield return new WaitForSeconds(1);
        shootAbilityOne = true;
        rotDir = Random.Range(0, 2);
        animator.rotate(rotDir);
        animator.setSpin(true);
        yield return new WaitForSeconds(15);
        abilityOneBuff = false;
        shootAbilityOne = false;
        animator.setSpin(false);
        animator.stopRotate();
    }
    private int shootDir;
    protected override void abilityTwo()
    {
        abilityTwoBuff = true;
        randSpaces.Sort((a, b) => 1 - 2 * Random.Range(0, 2));
        StartCoroutine(ability2ShootDelay());
        base.abilityTwo();
    }
    public bool abilityTwoBuff;
    IEnumerator ability2ShootDelay()
    {
        yield return new WaitForSeconds(1);
        shootDir = randSpaces[0];
        animator.shootShotgun(shootDir);
        yield return new WaitForSeconds(0.2f);
        shootShotgun(shootDir);
        yield return new WaitForSeconds(1f);
        shootDir = randSpaces[1];
        animator.shootShotgun(shootDir);
        yield return new WaitForSeconds(0.2f);
        shootShotgun(shootDir);
        yield return new WaitForSeconds(1f);
        shootDir = randSpaces[2];
        animator.shootShotgun(shootDir);
        yield return new WaitForSeconds(0.2f);
        shootShotgun(shootDir);
        yield return new WaitForSeconds(1f);
        shootDir = randSpaces[3];
        animator.shootShotgun(shootDir);
        yield return new WaitForSeconds(0.2f);
        shootShotgun(shootDir);
        yield return new WaitForSeconds(1f);
        abilityTwoBuff = false;
    }

    protected override void abilityThree()
    {
        abilityThreeBuff = true;
        StartCoroutine(abilityThreeDurration());
        base.abilityThree();
    }

    private void shootShotgun()
    {
        FindObjectOfType<AudioManager>().Play("towerShotgunShoot");
        GameObject newBullet1 = Instantiate(bullet2, transform.position, Quaternion.Euler(newRotation) * Quaternion.Euler(new Vector3(0, 0, 20)));
        GameObject newBullet2 = Instantiate(bullet2, transform.position, Quaternion.Euler(newRotation) * Quaternion.Euler(new Vector3(0, 0, 10)));
        GameObject newBullet3 = Instantiate(bullet2, transform.position, Quaternion.Euler(newRotation) * Quaternion.Euler(new Vector3(0, 0, 0)));
        GameObject newBullet4 = Instantiate(bullet2, transform.position, Quaternion.Euler(newRotation) * Quaternion.Euler(new Vector3(0, 0, -10)));
        GameObject newBullet5 = Instantiate(bullet2, transform.position, Quaternion.Euler(newRotation) * Quaternion.Euler(new Vector3(0, 0, -20)));
    }
    private int shootDirRot;
    private void shootShotgun(int dir)
    {
        if(dir == 0)
        {
            shootDirRot = 90;
        }
        else if(dir == 1)
        {
            shootDirRot = 0;
        }
        else if (dir == 2)
        {
            shootDirRot = 270;
        }
        else if (dir == 3)
        {
            shootDirRot = 180;
        }

        FindObjectOfType<AudioManager>().Play("towerShotgunShoot");
        GameObject newBullet1 = Instantiate(bullet2, transform.position, Quaternion.Euler(new Vector3(0,0,shootDirRot)) * Quaternion.Euler(new Vector3(0, 0, 20)));
        GameObject newBullet2 = Instantiate(bullet2, transform.position, Quaternion.Euler(new Vector3(0, 0, shootDirRot)) * Quaternion.Euler(new Vector3(0, 0, 10)));
        GameObject newBullet3 = Instantiate(bullet2, transform.position, Quaternion.Euler(new Vector3(0, 0, shootDirRot)) * Quaternion.Euler(new Vector3(0, 0, 0)));
        GameObject newBullet4 = Instantiate(bullet2, transform.position, Quaternion.Euler(new Vector3(0, 0, shootDirRot)) * Quaternion.Euler(new Vector3(0, 0, -10)));
        GameObject newBullet5 = Instantiate(bullet2, transform.position, Quaternion.Euler(new Vector3(0, 0, shootDirRot)) * Quaternion.Euler(new Vector3(0, 0, -20)));
    }

    public bool abilityThreeBuff;
    IEnumerator abilityThreeDurration()
    {
        yield return new WaitForSeconds(1f);
        animator.shootShotgun(lastDir);
        yield return new WaitForSeconds(0.2f);
        shootShotgun();
        yield return new WaitForSeconds(1.5f);
        abilityThreeBuff = false;
    }
}
