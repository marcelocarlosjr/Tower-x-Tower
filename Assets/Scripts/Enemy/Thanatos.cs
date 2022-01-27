using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Thanatos : Enemy
{
    public GameObject bullet;
    void Start()
    {
        myRig = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    protected override void shoot()
    {
        Invoke("instantiateBullet", fireSpeed);
    }

    public void instantiateBullet()
    {
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.Euler(newRotation));
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
            FindObjectOfType<AudioManager>().Play("thanatosHurt1");
        }
        if (random == 1)
        {
            FindObjectOfType<AudioManager>().Play("thanatosHurt2");
        }

        base.takeDamage(amount);
    }

    protected override void Die()
    {
        FindObjectOfType<AudioManager>().Play("thanatosDeath");
        base.Die();
    }
}
