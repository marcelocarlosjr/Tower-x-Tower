using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KingHammy : Boss
{
    private Transform HammySpawn1;
    private Transform HammySpawn2;
    private Transform HammySpawn3;
    private Transform HammySpawn4;

    public Transform shadow;

    private GameObject hammy;

    public bool reachedTop;

    public bool flying;

    public bool charging;

    public bool getChargeDir;

    public float chargeSpeed;

    public int chargeDir;

    public bool chargeDelay;

    public bool hitWall;
    private void Start()
    {
        myRig = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        HammySpawn1 = transform.parent.GetChild(1).transform;
        HammySpawn2 = transform.parent.GetChild(2).transform;
        HammySpawn3 = transform.parent.GetChild(3).transform;
        HammySpawn4 = transform.parent.GetChild(4).transform;

        hammy = FindObjectOfType<EnemyList>().enemies[2];

        StartCoroutine(spawn());
    }
    public bool spawning;
    IEnumerator spawn()
    {
        spawning = true;
        yield return new WaitForSeconds(10f);
        spawning = false;
    }

    protected override void moveCharactor()
    {
        if (agent.enabled == true)
        {
            this.agent.SetDestination(playerPos);
        }
    }

    protected override void abilityOne()
    {
        GameObject enemy1 = Instantiate(hammy, HammySpawn1.position, Quaternion.identity);
        GameObject enemy2 = Instantiate(hammy, HammySpawn2.position, Quaternion.identity);
        GameObject enemy3 = Instantiate(hammy, HammySpawn3.position, Quaternion.identity);
        GameObject enemy4 = Instantiate(hammy, HammySpawn4.position, Quaternion.identity);

        enemy1.transform.localScale = new Vector3(enemy1.transform.localScale.x * .8f, enemy1.transform.localScale.y * .8f, enemy1.transform.localScale.z * .8f);
        enemy2.transform.localScale = new Vector3(enemy2.transform.localScale.x * .8f, enemy2.transform.localScale.y * .8f, enemy2.transform.localScale.z * .8f);
        enemy3.transform.localScale = new Vector3(enemy3.transform.localScale.x * .8f, enemy3.transform.localScale.y * .8f, enemy3.transform.localScale.z * .8f);
        enemy4.transform.localScale = new Vector3(enemy4.transform.localScale.x * .8f, enemy4.transform.localScale.y * .8f, enemy4.transform.localScale.z * .8f);

        enemy1.GetComponent<Enemy>().meleeDamage *= .5f;
        enemy2.GetComponent<Enemy>().meleeDamage *= .5f;
        enemy3.GetComponent<Enemy>().meleeDamage *= .5f;
        enemy4.GetComponent<Enemy>().meleeDamage *= .5f;

        enemy1.GetComponent<Enemy>().enemyHealth *= .5f;
        enemy2.GetComponent<Enemy>().enemyHealth *= .5f;
        enemy3.GetComponent<Enemy>().enemyHealth *= .5f;
        enemy4.GetComponent<Enemy>().enemyHealth *= .5f;

        base.abilityOne();
    }

    protected override void abilityTwo()
    {
        FindObjectOfType<AudioManager>().Play("KingHJump");
        GetComponent<EnemyAnimationController>().animateSlam();
        base.abilityTwo();
    }

    protected override void abilityThree()
    {
        charging = true;
        if (charging)
        {
            GetComponent<EnemyAnimationController>().animateCharge();
        }
        base.abilityThree();
    }

    public void fly()
    {
        shadow.gameObject.SetActive(true);
        if (agent.enabled == true)
        {
            agent.enabled = false;
        }
        if (GetComponent<PolygonCollider2D>().enabled)
        {
            GetComponents<PolygonCollider2D>()[0].enabled = false;
            GetComponents<PolygonCollider2D>()[1].enabled = true;
        }
        if (transform.position.y < 15 && !reachedTop)
        {
            myRig.velocity = new Vector2(0, 10);
            StartCoroutine(waitTimer());
        }
        else if (reachedTop)
        {
            myRig.velocity = new Vector2(shadow.transform.position.x - transform.position.x, shadow.transform.position.y + 1 - transform.position.y) * 15;
        }
    }

    protected override void Die()
    {
        FindObjectOfType<AudioManager>().Play("KingHDeath");
        base.Die();
    }

    public override void takeDamage(float damage)
    {
        int random = Random.Range(0, 2);

        if (random == 0)
        {
            FindObjectOfType<AudioManager>().Play("KingHHurt");
        }
        if (random == 1)
        {
            FindObjectOfType<AudioManager>().Play("KingHHurt2");
        }
        base.takeDamage(damage);
    }


    public IEnumerator waitTimer()
    {
        yield return new WaitForSeconds(3.7f);
        reachedTop = true;
    }

    public override void Update()
    {
        if (!spawning)
        {
            base.Update();

            if (hitWall)
            {
                charging = false;
            }

            if (!flying)
            {
                shadow.transform.position = transform.position;
                shadow.gameObject.SetActive(false);
                reachedTop = false;
                if (agent.enabled == false)
                {
                    agent.enabled = true;
                }
                if (!GetComponent<PolygonCollider2D>().enabled)
                {
                    GetComponents<PolygonCollider2D>()[0].enabled = true;
                    GetComponents<PolygonCollider2D>()[1].enabled = false;
                }
            }

            if (charging)
            {
                hitWall = false;
                if ((this.transform.position.y <= (this.transform.parent.position.y - 4.5)) || (this.transform.position.y >= (this.transform.parent.position.y + 4.5)) || (this.transform.position.x >= (this.transform.parent.position.x + 8.5)) || (this.transform.position.x <= (this.transform.parent.position.x - 8.5)))
                {
                    hitWall = true;
                }

                if (agent.enabled == true)
                {
                    agent.enabled = false;
                }
                if (GetComponent<PolygonCollider2D>().enabled)
                {
                    GetComponents<PolygonCollider2D>()[0].enabled = false;
                    GetComponents<PolygonCollider2D>()[1].enabled = true;
                }
                if (chargeDir == -1)
                {
                    if (chargeDelay == false)
                    {
                        StartCoroutine(chargeTimer());
                    }
                }
                if (chargeDir == 0)
                {
                    if (chargeDelay == false)
                    {
                        StartCoroutine(chargeTimer());
                    }
                    else
                    {
                        myRig.velocity = new Vector2(0, chargeSpeed);
                    }
                }
                if (chargeDir == 1)
                {
                    if (chargeDelay == false)
                    {
                        StartCoroutine(chargeTimer());
                    }
                    else
                    {
                        myRig.velocity = new Vector2(chargeSpeed, 0);
                    }
                }
                if (chargeDir == 2)
                {
                    if (chargeDelay == false)
                    {
                        StartCoroutine(chargeTimer());
                    }
                    else
                    {
                        myRig.velocity = new Vector2(0, chargeSpeed * -1);
                    }
                }
                if (chargeDir == 3)
                {
                    if (chargeDelay == false)
                    {
                        StartCoroutine(chargeTimer());
                    }
                    else
                    {
                        myRig.velocity = new Vector2(chargeSpeed * -1, 0);
                    }
                }
            }
            else if (!charging && chargeDelay)
            {
                if (agent.enabled == false)
                {
                    agent.enabled = true;
                }
                if (!GetComponent<PolygonCollider2D>().enabled)
                {
                    GetComponents<PolygonCollider2D>()[0].enabled = true;
                    GetComponents<PolygonCollider2D>()[1].enabled = false;
                }
                GetComponent<EnemyAnimationController>().stopCharge();

                chargeDir = -1;
                getChargeDir = false;
                chargeDelay = false;
            }
            else if (!charging && !chargeDelay && hitWall)
            {
                hitWall = false;
            }

            if (flying)
            {
                fly();
            }

            if (!abilityOneOnCD)
            {
                abilityOne();
            }

            if (!abilityTwoOnCD && !charging)
            {
                abilityTwo();
            }

            if (!abilityThreeOnCD && !flying)
            {
                abilityThree();
            }
        }
    }

    public IEnumerator chargeTimer()
    {
        chargeDelay = false;
        yield return new WaitForSeconds(.7f);
        if (!getChargeDir)
        {
            chargeDir = lastDir;
            getChargeDir = true;
        }
        chargeDelay = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (charging)
        {
            if(collision.transform.tag == "Wall")
            {
                hitWall = true;
            }
        }
    }

}
