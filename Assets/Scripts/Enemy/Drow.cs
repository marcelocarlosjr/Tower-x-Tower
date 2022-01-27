using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Drow : Enemy
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
}
