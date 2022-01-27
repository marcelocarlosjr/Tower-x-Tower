using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed;
    public int damage;

    private Transform parentHolder;

    void Start()
    {
        parentHolder = GameObject.FindGameObjectWithTag("ProjectileHolder").transform;
        this.transform.parent = parentHolder;
        Destroy(gameObject, 3f);
    }

    void FixedUpdate()
    {
        transform.position += transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if (collision.transform.tag == "Player")
        {
            if (collision.gameObject.GetComponent<BoxCollider2D>().isTrigger)
            {
                collision.GetComponent<PlayerHealthController>().takeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
