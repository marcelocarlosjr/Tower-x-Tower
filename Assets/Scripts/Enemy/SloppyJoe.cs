using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SloppyJoe : Enemy
{
    void Start()
    {
        myRig = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    protected override void shoot()
    {

    }

    protected override void moveCharactor()
    {
        if (agent.enabled == true)
        {
            this.agent.SetDestination(playerPos);
        }
    }

    protected override void Die()
    {
        FindObjectOfType<AudioManager>().Play("enemySloppyJoeDeath");
        base.Die();
    }

    public override void takeDamage(float amount)
    {

        if (Random.Range(0, 2) == 0)
        {
            FindObjectOfType<AudioManager>().Play("enemySloppyJoeHurt1");
        }
        if (Random.Range(0, 2) == 1)
        {
            FindObjectOfType<AudioManager>().Play("enemySloppyJoeHurt2");
        }
        base.takeDamage(amount);
    }
}
