using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    public float damage;
    public float shotDelay;
    public int extraShot;
    public float bulletSpeed;
    public int spread;
    public float CD;
    public bool explode;
    public float explosive;
    public bool piercingBullet;

    private float tempDamage;
    private float tempSpeed;

    public bool towerPlaced = false;
    public enum TowerType
    {
        auto,
        freeze,
        sniper,
        fast,
        shotgun,
        cannon,
        gate,
        support,
        barrel,
        blade
    }

    public TowerType towerType;

    public bool autoShoot = false;
    [SerializeField] private float range;

    private float nextTimeToShoot;

    public GameObject currentTarget;

    public bool pickedUp = false;

    public bool TowerSelected = false;

    public bool AbilityOneOnCD;

    public float AbilityOneCD;

    public bool AbilityTwoOnCD;

    public float AbilityTwoCD;

    public float abilityOneCDTimer;
    public float abilityTwoCDTimer;


    public GameObject ability1;

    public GameObject ability2;

    private void Awake()
    {
        TowerList.towerList.Add(gameObject);
    }
    private void Start()
    {
        nextTimeToShoot = Time.time;
        CD = 1;
        abilityOneTimer = false;
        abilityTwoTimer = false;
    }

    public void unselectTower()
    {
        TowerSelected = false;
    }

    private void updateNearestEnemy()
    {
        GameObject currentNearestEnemy = null;

        float distance = Mathf.Infinity;

        foreach(GameObject enemy in EnemyList.enemyList)
        {
            if(enemy != null)
            {
                float _distance = (transform.position - enemy.transform.position).magnitude;

                if (_distance < distance)
                {
                    distance = _distance;
                    currentNearestEnemy = enemy;
                }
            }
        }
        if(distance <= range)
        {
            currentTarget = currentNearestEnemy;
        }
        else
        {
            currentTarget = null;
        }
    }

    protected virtual void shoot()
    {
        
    }
    public IEnumerator AbilityOneTimer()
    {
        abilityOne();
        yield return new WaitForSeconds(AbilityOneCD * CD);
        AbilityOneOnCD = false;
    }

    protected virtual void abilityOne()
    {
        
    }

    public void addCDR(float cdr)
    {
        CD *= cdr;
    }

    public IEnumerator AbilityTwoTimer()
    {
        abilityTwo();
        yield return new WaitForSeconds(AbilityTwoCD * CD);
        AbilityTwoOnCD = false;
    }
    protected virtual void abilityTwo()
    {
        
    }
    private bool abilityOneTimer;
    private bool abilityTwoTimer;
    protected virtual void Update()
    {
        if (AbilityOneOnCD)
        {
            if (!abilityOneTimer)
            {
                abilityOneCDTimer = AbilityOneCD * CD;
                abilityOneTimer = true;
            }
            abilityOneCDTimer -= Time.deltaTime;
        }
        else
        {
            abilityOneCDTimer = 0;
            abilityOneTimer = false;
        }

        if (AbilityTwoOnCD)
        {
            if (!abilityTwoTimer)
            {
                abilityTwoTimer = true;
                abilityTwoCDTimer = AbilityTwoCD * CD;
            }
            abilityTwoCDTimer -= Time.deltaTime;
        }
        else
        {
            abilityTwoCDTimer = 0;
            abilityTwoTimer = false;
        }



        if (TowerSelected)
        {
            this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (!TowerSelected)
        {
            this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (towerType == TowerType.support && !pickedUp && towerPlaced)
        {
            if (Time.time >= nextTimeToShoot)
            {
                shoot();
                nextTimeToShoot = Time.time + shotDelay;
            }
        }
        if (!pickedUp && TowerSelected || !pickedUp && towerType == TowerType.auto && towerPlaced)
        {
            if (autoShoot == true)
            {
                updateNearestEnemy();

                if (Time.time >= nextTimeToShoot)
                {
                    if (currentTarget != null)
                    {
                        shoot();
                        nextTimeToShoot = Time.time + shotDelay;
                    }
                }
            }
            if (autoShoot == false)
            {
                if (Input.GetAxisRaw("Fire1") > 0)
                {
                    if (Time.time >= nextTimeToShoot)
                    {
                        shoot();
                        nextTimeToShoot = Time.time + shotDelay;
                    }
                }
            }

            if (Input.GetAxisRaw("UseAbility1") > 0)
            {
                if (!AbilityOneOnCD)
                {
                    AbilityOneOnCD = true;
                    StartCoroutine(AbilityOneTimer());
                }
            }

            if (Input.GetAxisRaw("UseAbility2") > 0)
            {
                if (!AbilityTwoOnCD)
                {
                    AbilityTwoOnCD = true;
                    StartCoroutine(AbilityTwoTimer());
                }
            }
        }
    }
    private bool hit;
    private bool speedHit;
    public IEnumerator DamageIncreaseTimer(float multiplier, float cd)
    {
        if (!hit) {
            hit = true;
            tempDamage = damage;
            damage *= multiplier;
            yield return new WaitForSeconds(cd);
            damage = tempDamage;
            hit = false;
        }
    }

    public IEnumerator SpeedIncreaseTimer(float multiplier, float cd)
    {
        if (!speedHit)
        {
            speedHit = true;
            tempSpeed = shotDelay;
            shotDelay /= multiplier;
            yield return new WaitForSeconds(cd);
            shotDelay = tempSpeed;
            speedHit = false;
        }
    }

    public void autoTurretBuff(float multiplier, float cd, GameObject bullet)
    {
        Object.Destroy(bullet);
        StartCoroutine(DamageIncreaseTimer(multiplier, cd));
    }
    public void autoTurretBuffSpeed(float multiplier, float cd)
    {
        StartCoroutine(SpeedIncreaseTimer(multiplier, cd));
    }

}
