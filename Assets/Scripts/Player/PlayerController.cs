using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D MyRig;
    public float Speed = 5f;

    public float newSpeed;

    private RoomTemplates templates;
    private CameraController CamController;
    private PlayerUpgradeController upgrades;

    public float H;
    public float V;

    public bool playerIdle;

    public bool startWalkSound;

    public int currentFloorNumber;

    void Start()
    {
        upgrades = GetComponent<PlayerUpgradeController>();
        MyRig = this.gameObject.GetComponent<Rigidbody2D>();
        MyRig.gravityScale = 0;
        MyRig.drag = 0f;

        CamController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();

        currentFloorNumber = 1;
        Reload = false;
    }

    private bool Reload;

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Floor1" && Reload == false)
        {
            EnemySpawnerController.enemySpawnerList.Clear();
            DoorSpawner.doorSpawnerList.Clear();
            FindObjectOfType<InventoryUpdate>().updateNewFloor();
            templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
            GetComponent<PlayerHealthController>().currentHealth = GetComponent<PlayerHealthController>().maxHealth;
            Reload = true;
        }


        if(SceneManager.GetActiveScene().name == "Floor" + (currentFloorNumber + 1))
        {
            currentFloorNumber++;
            EnemySpawnerController.enemySpawnerList.Clear();
            DoorSpawner.doorSpawnerList.Clear();
            FindObjectOfType<InventoryUpdate>().updateNewFloor();
            templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
            GetComponent<PlayerHealthController>().currentHealth = GetComponent<PlayerHealthController>().maxHealth;
            FindObjectOfType<StairsController>().floorNumber++;
        }


        H = Input.GetAxis("Horizontal");
        V = Input.GetAxis("Vertical");

        if (H == 0f && V == 0f)
        {
            playerIdle = true;
        }
        else
        {
            playerIdle = false;
        }

        MyRig.velocity = new Vector2(H, V).normalized * (Speed + (Speed * (upgrades.player_speed*0.05f)));
        newSpeed = (Speed + (Speed * (upgrades.player_speed * 0.1f)));

        if (CamController.CamPos == templates.lastRoom && templates.spawnedBoss)
        {
            transform.position = new Vector2(1, transform.position.y);
        }

        if (transform.position.x < -.5 && templates.spawnedBoss)
        {
            transform.position = new Vector2((templates.lastRoom*16)-1, transform.position.y);
        }

        if (!playerIdle)
        {
            if (!startWalkSound)
            {
                startWalkSound = true;
                walkSound();
            }
        }
        if (playerIdle)
        {
            if (startWalkSound)
            {
                startWalkSound = false;
                FindObjectOfType<AudioManager>().Pause("B_Footstep");
            }
        }
    }

    public void walkSound()
    {
        FindObjectOfType<AudioManager>().Play("B_Footstep");
    }
}
