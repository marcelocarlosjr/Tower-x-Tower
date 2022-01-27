using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public float damage;
    public bool DOT;

    private void Start()
    {
        if (transform.GetComponentInParent<Transform>().transform.GetComponentInParent<Tower>())
        {
            damage = transform.GetComponentInParent<Transform>().transform.GetComponentInParent<Tower>().damage;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.takeDamage(damage);
            if (DOT)
            {
                enemy.applyTickCannon(10, damage);
            }
        }
    }
}
