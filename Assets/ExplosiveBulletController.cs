using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBulletController : MonoBehaviour
{
    private Bullet bulletInstance;

    private void Start()
    {
        bulletInstance = this.GetComponent<Bullet>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.takeDamage(bulletInstance.bulletDamage * bulletInstance.explodeMulti);
            if (bulletInstance.DOT)
            {
                enemy.applyTickCannon(10, bulletInstance.bulletDamage * bulletInstance.explodeMulti);
            }
        }
    }
}
