using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float enemyMaxHealth;

    public float enemyHealth;

    public float movementSpeed;

    public float fireSpeed;
    private float nextTimeToShoot;

    protected NavMeshAgent agent;

    public float meleeDamage;

    protected Transform Player;

    public float range;

    protected Vector3 newRotation;

    public float meleeDelay;
    public bool canMelee = true;

    public int lastDir;

    protected Rigidbody2D myRig;

    public Vector2 playerPos;

    public bool touchingPlayer;

    public bool touchingEnemy;

    private Transform parentHolder;

    public int coinValue;

    public GameObject moneyBag;

    private bool blinking = false;

    public bool isSlowed;
    public float tempMovementSpeed;

    private float slowTime;

    public bool canShoot;

    public List<int> burnTickTimerCannon = new List<int>();
    public List<int> burnTickTimersBlade = new List<int>();

    private void Awake()
    {
        if (!GetComponent<Boss>())
        {
            parentHolder = GameObject.FindGameObjectWithTag("EnemyHolder").transform;
            this.transform.parent = parentHolder;
        }
        EnemyList.enemyList.Add(gameObject);
        Player = GameObject.Find("Player").transform;
        enemyMaxHealth = enemyHealth;
        tempMovementSpeed = movementSpeed;
    }

    private void Start()
    {
        nextTimeToShoot = Time.time;
        Player = FindObjectOfType<PlayerController>().gameObject.transform;
    }

    public virtual void takeDamage(float amount)
    {
        enemyHealth -= amount;

        if (enemyHealth <= 0)
        {
            Die();
        }
        if (!blinking && amount != 0)
        {
            StartCoroutine("damageBlink");
        }
    }
    public void slow(float slowAmount, float slowT)
    {
        slowTime = slowT;
        StartCoroutine("slowTimer", slowAmount);
    }

    private IEnumerator slowTimer(float slowAmount)
    {
        movementSpeed *= slowAmount;
        isSlowed = true;
        canShoot = false;
        yield return new WaitForSeconds(slowTime);
        canShoot = true;
        movementSpeed = tempMovementSpeed;
        isSlowed = false;
    }


    private IEnumerator damageBlink()
    {
        blinking = true;
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
        blinking = false;
    }

    public void applyTickCannon(int ticks, float dmg)
    {
        if(burnTickTimerCannon.Count <= 0)
        {
            burnTickTimerCannon.Add(ticks);
            StartCoroutine(BurnCannon(dmg));
        }
        else
        {
            burnTickTimerCannon.Add(ticks);
        }
    }

    IEnumerator BurnCannon(float dmg)
    {
        while(burnTickTimerCannon.Count > 0)
        {
            for(int i = 0; i < burnTickTimerCannon.Count; i++)
            {
                burnTickTimerCannon[i]--;
            }
            takeDamage((dmg * .2f) * burnTickTimerCannon.Count);
            burnTickTimerCannon.RemoveAll(i => i == 0);
            yield return new WaitForSeconds(1);
        }
    }

    public void applyTickBlade(int ticks, float dmg)
    {
        if (burnTickTimersBlade.Count <= 0)
        {
            burnTickTimersBlade.Add(ticks);
            StartCoroutine(BurnBlade(dmg));
        }
        else
        {
            burnTickTimersBlade.Add(ticks);
        }
    }

    IEnumerator BurnBlade(float dmg)
    {
        while (burnTickTimersBlade.Count > 0)
        {
            for (int i = 0; i < burnTickTimersBlade.Count; i++)
            {
                burnTickTimersBlade[i]--;
            }
            takeDamage((dmg*.2f) * burnTickTimersBlade.Count);
            burnTickTimersBlade.RemoveAll(i => i == 0);
            yield return new WaitForSeconds(1);
        }
    }

    protected virtual void Die()
    {
        GameObject money = Instantiate(moneyBag, transform.position, Quaternion.identity);
        money.GetComponent<Money>().setValue(coinValue);

        EnemyList.enemyList.Remove(gameObject);
        Destroy(transform.gameObject);
    }

 

    public virtual void Update()
    {
        if (isSlowed)
        {
            GetComponent<SpriteRenderer>().color = new Color(0,211,255,255);
        }
        else if(!blinking && !isSlowed)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }

        if(agent != null)
        {
            agent.speed = movementSpeed;

            if (touchingPlayer)
            {
                agent.enabled = false;
                myRig.bodyType = RigidbodyType2D.Dynamic;
            }
            else if (!touchingPlayer)
            {
                myRig.velocity = Vector2.zero;
                myRig.bodyType = RigidbodyType2D.Kinematic;
                agent.enabled = true;
            }

            if (touchingEnemy)
            {
                agent.enabled = false;
                myRig.bodyType = RigidbodyType2D.Dynamic;
            }
            else if (touchingEnemy)
            {
                myRig.velocity = Vector2.zero;
                myRig.bodyType = RigidbodyType2D.Kinematic;
                agent.enabled = true;
            }
        }

        moveCharactor();

        if (!this.GetComponent<NavMeshAgent>())
        {
            moveCharactor((playerPos - (Vector2) this.transform.position));
        }

        playerPos = Player.position;

        Vector2 relative = Player.position - transform.position;

        float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;

        newRotation = new Vector3(0, 0, angle);
    }

    protected virtual void shoot()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        if ((transform.position - Player.position).magnitude <= range)
        {
            if (Time.time >= nextTimeToShoot && canShoot)
            {
                shoot();
                nextTimeToShoot = Time.time + fireSpeed;
            }
        }
        checkForward();
    }

    protected virtual void moveCharactor(Vector2 direction)
    {
        
    }

    protected virtual void moveCharactor()
    {

    }

    public IEnumerator meleeTimer()
    {
        yield return new WaitForSeconds(meleeDelay);
        canMelee = true;
    }

    public float doMelee()
    {
        if (canMelee)
        {
            canMelee = false;
            StartCoroutine(meleeTimer());
            this.gameObject.GetComponent<EnemyAnimationController>().animateAttack();
            return meleeDamage;
        }
        else
        {
            return 0;
        }
    }

    public void checkForward()
    {
        Vector2 tempVec = (Player.transform.position - transform.position);

        if((tempVec.x > 0 && tempVec.y > 0 && tempVec.x > tempVec.y) || (tempVec.x > 0 && tempVec.y < 0 && tempVec.x > Mathf.Abs(tempVec.y)))
        {
            lastDir = 1;
        }
        else if (tempVec.y > 0 && tempVec.x > 0 && tempVec.y > tempVec.x || (tempVec.y > 0 && tempVec.x < 0 && tempVec.y > Mathf.Abs(tempVec.x)))
        {
            lastDir = 0;
        }
        else if (tempVec.y < 0 && tempVec.x < 0 && tempVec.y < tempVec.x || (tempVec.y < 0 && tempVec.x > 0 && tempVec.x < Mathf.Abs(tempVec.y)))
        {
            lastDir = 2;
        }
        else if (tempVec.x < 0 && tempVec.y < 0 && tempVec.x < tempVec.y || (tempVec.x < 0 && tempVec.y > 0 && tempVec.y < Mathf.Abs(tempVec.x)))
        {
            lastDir = 3;
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            touchingPlayer = true;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            foreach (GameObject enemy in EnemyList.enemyList)
            {
                if (enemy.GetComponent<Enemy>().touchingPlayer)
                {
                    touchingEnemy = true;
                }

            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            foreach (GameObject enemy in EnemyList.enemyList)
            {
                enemy.gameObject.GetComponent<Enemy>().touchingEnemy = false;
                enemy.gameObject.GetComponent<Enemy>().touchingPlayer = false;
            }
        }
    }
}
