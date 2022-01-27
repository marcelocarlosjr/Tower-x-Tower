using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hammy : Enemy
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

    public override void takeDamage(float amount)
    {
        int random = Random.Range(0, 2);

        if (random == 0)
        {
            FindObjectOfType<AudioManager>().Play("hammyHurt1");
        }
        if (random == 1)
        {
            FindObjectOfType<AudioManager>().Play("hammyHurt2");
        }
        base.takeDamage(amount);
    }

    protected override void Die()
    {
        FindObjectOfType<AudioManager>().Play("hammyDeath");
        base.Die();
    }
}
