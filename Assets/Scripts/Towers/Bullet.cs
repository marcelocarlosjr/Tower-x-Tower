using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletDamage;
    public float bulletLife;
    public bool piercing = false;
    public bool freezing = false;
    public bool DOT = false;
    public bool AOE = false;
    public int spreadAmount;

    public GameObject originTower;

    public float explodeMulti;

    private Transform parentHolder;

    public int numEnemiesHit;

    private void Start()
    {
        parentHolder = GameObject.FindGameObjectWithTag("ProjectileHolder").transform;
        this.transform.parent = parentHolder;
        Invoke("Die", bulletLife);
        if (originTower != null && originTower.GetComponent<Tower>() && originTower.GetComponent<Tower>().explode)
        {
            AOE = true;
            explodeMulti = originTower.GetComponent<Tower>().explosive;
        }
    }
    private void FixedUpdate()
    {
        transform.position += transform.right * bulletSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Wall"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == ("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.takeDamage(bulletDamage);
            if (freezing)
            {
                enemy.slow(0.33f, 2);
            }
            if (!piercing)
            {
                if (originTower != null && originTower.GetComponent<FastTower>())
                {
                    originTower.GetComponent<FastTower>().successiveHits += 1;
                }
                if (DOT)
                {
                    enemy.applyTickCannon(10, bulletDamage);
                }
                if (AOE)
                {
                    explode();
                }
                Destroy(gameObject);
            }
            if (piercing)
            {
                numEnemiesHit += 1;
                if (DOT)
                {
                    enemy.applyTickCannon(10, bulletDamage);
                }
                if (AOE)
                {
                    explode();
                }
            }
        }
    }

    private void Die()
    {
        if (originTower != null && originTower.GetComponent<FastTower>() && numEnemiesHit == 0)
        {
            originTower.GetComponent<FastTower>().successiveHits = 0;
        }
        else if(originTower != null && originTower.GetComponent<FastTower>() && numEnemiesHit > 0)
        {
            originTower.GetComponent<FastTower>().successiveHits += numEnemiesHit;
        }
        Object.Destroy(this.gameObject);
    }

    private void explode()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        Debug.Log("Exploded");
    }
}
