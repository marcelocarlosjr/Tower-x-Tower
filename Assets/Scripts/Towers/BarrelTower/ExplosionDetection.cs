using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDetection : MonoBehaviour
{
    private float damage;
    public BarrelTower barrelTower;
    private void Update()
    {
        damage = barrelTower.damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {     
        if(collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.takeDamage(damage);
        }
    }
}
