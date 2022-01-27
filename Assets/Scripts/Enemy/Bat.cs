using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    void Start()
    {
        myRig = GetComponent<Rigidbody2D>();
    }
    protected override void moveCharactor(Vector2 direction)
    {
        myRig.velocity = direction.normalized * movementSpeed;
    }

    protected override void Die()
    {
        FindObjectOfType<AudioManager>().Play("enemyBatDeath");
        base.Die();
    }

    public override void takeDamage(float amount)
    {
        if(Random.Range(0, 2) == 0)
        {
            FindObjectOfType<AudioManager>().Play("enemyBatHurt1");
        }
        if (Random.Range(0, 2) == 1)
        {
            FindObjectOfType<AudioManager>().Play("enemyBatHurt2");
        }
        base.takeDamage(amount);
    }
}
