using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TowerInteract : MonoBehaviour
{
    private PlayerController playerController;
    private Vector3 HolderR;
    private Vector3 HolderL;
    private Vector3 HolderT;
    private Vector3 HolderB;

    private CircleCollider2D holderRCollider;
    private CircleCollider2D holderLCollider;
    private CircleCollider2D holderTCollider;
    private CircleCollider2D holderBCollider;

    private Vector3 towerLocationFromDir;

    public int lastDir;

    public bool towerPickedUp = false;
    public bool canDrop = false;

    public Transform towerRig;

    public float dropDelay = .5f;

    private InputController ic;

    private bool towerCollisionOn = false;


    private void Start()
    {
        playerController = this.GetComponent<PlayerController>();
        ic = GameObject.FindGameObjectWithTag("InputController").GetComponent<InputController>();

        holderRCollider = this.transform.GetChild(0).gameObject.GetComponent<CircleCollider2D>();
        holderLCollider = this.transform.GetChild(1).gameObject.GetComponent<CircleCollider2D>();
        holderTCollider = this.transform.GetChild(2).gameObject.GetComponent<CircleCollider2D>();
        holderBCollider = this.transform.GetChild(3).gameObject.GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        pickUpTower();

        float playerH = playerController.H;
        float playerV = playerController.V;

        HolderR = this.transform.GetChild(0).gameObject.transform.position;
        HolderL = this.transform.GetChild(1).gameObject.transform.position;
        HolderT = this.transform.GetChild(2).gameObject.transform.position;
        HolderB = this.transform.GetChild(3).gameObject.transform.position;

        if (playerV > 0) //up
        {
            lastDir = 0;
            towerLocationFromDir = HolderT;
        }
        if (playerH > 0) //right
        {
            lastDir = 1;
            towerLocationFromDir = HolderR;
        }
        if (playerV < 0) //down
        {
            lastDir = 2;
            towerLocationFromDir = HolderB;
        }
        if (playerH < 0) //left
        {
            lastDir = 3;
            towerLocationFromDir = HolderL;
        }

        if (towerCollisionOn == true)
        {
            if (towerRig != null)
            {
                towerRig.GetComponent<CircleCollider2D>().enabled = false;
            }
            if (lastDir == 0) //up
            {
                holderTCollider.enabled = true;
                holderRCollider.enabled = false;
                holderLCollider.enabled = false;
                holderBCollider.enabled = false;
            }
            if (lastDir == 1) //right
            {
                holderTCollider.enabled = false;
                holderRCollider.enabled = true;
                holderLCollider.enabled = false;
                holderBCollider.enabled = false;
            }
            if (lastDir == 2) //down
            {
                holderTCollider.enabled = false;
                holderRCollider.enabled = false;
                holderLCollider.enabled = false;
                holderBCollider.enabled = true;
            }
            if (lastDir == 3) //left
            {
                holderTCollider.enabled = false;
                holderRCollider.enabled = false;
                holderLCollider.enabled = true;
                holderBCollider.enabled = false;
            }
        }
        else
        {
            holderRCollider.enabled = false;
            holderLCollider.enabled = false;
            holderTCollider.enabled = false;
            holderBCollider.enabled = false;
        }
    }
 


    public IEnumerator dropTimer()
    {
        yield return new WaitForSeconds(dropDelay);
        if (towerRig != null)
        {
            towerRig.GetComponent<Tower>().pickedUp = true;
            canDrop = true;
        }
    }
    private void pickUpTower()
    {
        if (towerRig != null)
        {
            towerPickedUp = true;
            towerCollisionOn = true;
            towerRig.transform.position = towerLocationFromDir;
            StopCoroutine(dropTimer());
            StartCoroutine(dropTimer());
        }

        if (canDrop == true && ic.PickupTower())
        {
            if (towerRig != null)
            {
                towerRig.gameObject.GetComponent<CircleCollider2D>().enabled = true;
                towerRig.gameObject.GetComponent<Tower>().pickedUp = false;
                towerCollisionOn = false;
                towerRig = null;
                canDrop = false;
                Invoke("dropCD", 0.1f);
            }
        }
    }

    private void dropCD()
    {
        towerPickedUp = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (ic.PickupTower())
        {
            if (towerPickedUp == false)
            {
                if (collision.gameObject.tag == "Tower")
                {
                    towerRig = collision.gameObject.transform;
                }
            }
        }
    }


    public int getLastDir()
    {
        return lastDir;
    }
}
