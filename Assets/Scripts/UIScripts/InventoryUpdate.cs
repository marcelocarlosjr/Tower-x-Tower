using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUpdate : MonoBehaviour
{
    public Transform playerTransform;

    public GameObject inventoryPanel;
    public GameObject openTowerSlot;

    public Text FloorRoom;

    public Text MoneyAmount;
    public int currentMoney;

    public Text SpawnTimer;
    public float SpawnTime;
    public int room;
    public int round;

    public Transform playerHealthBar;
    private float playerCurrentHealth;
    private float playerMaxHealth;

    public Transform bossHealthBar;
    private float bossCurrentHealth;
    private float bossMaxHealth;

    public int floorLevel;

    public GameObject towerSlot1;
    public GameObject towerSlot2;
    public GameObject towerSlot3;
    public GameObject towerSlot4;
    public GameObject towerSlot5;

    public bool towerSlot1Filled;
    public bool towerSlot2Filled;
    public bool towerSlot3Filled;
    public bool towerSlot4Filled;
    public bool towerSlot5Filled;

    private int lastRoom;

    private bool lastRoomRecieved;

    private void Start()
    {
        round = 0;
        lastRoomRecieved = false;

        inventoryPanel = transform.GetChild(0).gameObject;

        towerSlot1Filled = false;
        towerSlot2Filled = false;
        towerSlot3Filled = false;
        towerSlot4Filled = false;
        towerSlot5Filled = false;

        Invoke("getLastRoom", FindObjectOfType<RoomTemplates>().waitTime + 0.5f);
    }
    private void getLastRoom()
    {
        lastRoom = FindObjectOfType<RoomTemplates>().lastRoom;
        floorLevel = FindObjectOfType<PlayerController>().currentFloorNumber;
        lastRoomRecieved = true;
    }

    public void updateNewFloor()
    {
        lastRoomRecieved = false;
        Invoke("getLastRoom", FindObjectOfType<RoomTemplates>().waitTime + 0.5f);
    }


    private void Update()
    {
        playerCurrentHealth = FindObjectOfType<PlayerHealthController>().currentHealth;
        playerMaxHealth = FindObjectOfType<PlayerHealthController>().maxHealth;

        if (FindObjectOfType<Boss>())
        {
            bossCurrentHealth = FindObjectOfType<Boss>().enemyHealth;
            bossMaxHealth = FindObjectOfType<Boss>().enemyMaxHealth;

            if (bossCurrentHealth / bossMaxHealth > 0)
            {
                bossHealthBar.gameObject.transform.parent.gameObject.SetActive(true);
                bossHealthBar.transform.localScale = new Vector2(bossCurrentHealth / bossMaxHealth, 1);
            }
        }
        if (lastRoomRecieved && EnemySpawnerController.enemySpawnerList[lastRoom - 2].GetComponent<EnemySpawner>().allEnemysKilled)
        {
            bossHealthBar.transform.localScale = new Vector2(0, 1);
            Invoke("removeBossBar", 5);
        }

        if (playerCurrentHealth/playerMaxHealth > 0)
        {
            playerHealthBar.transform.localScale = new Vector2(playerCurrentHealth / playerMaxHealth, 1);
        }
        else if(playerCurrentHealth / playerMaxHealth <= 0)
        {
            playerHealthBar.transform.localScale = new Vector2(0, 1);
        }

        room = FindObjectOfType<CameraController>().CamPos;

        if(lastRoomRecieved && room > 0)
        {
            SpawnTime = EnemySpawnerController.enemySpawnerList[room - 1].GetComponent<EnemySpawner>().updatingSpawnTimer;
        }

        currentMoney = FindObjectOfType<PlayerUpgradeController>().money;
        FloorRoom.text = floorLevel + " - " + ((((int)playerTransform.position.x) / 16)+1).ToString();

        MoneyAmount.text = ": " + currentMoney;

        SpawnTimer.text = Math.Round((double)SpawnTime, round).ToString();

        if(SpawnTime == 0)
        {
            SpawnTimer.enabled = false;
        }
        else if (SpawnTime > 0)
        {
            SpawnTimer.enabled = true;
        }
    }

    public void removeBossBar()
    {
        bossHealthBar.transform.parent.gameObject.SetActive(false);
    }
    public void addUpgradeButton(GameObject button)
    {
        GameObject buttonInst = Instantiate(button);
        buttonInst.transform.SetParent(inventoryPanel.transform);
    }

    public void addTowerButton(GameObject button, GameObject tower)
    {
        if (towerSlot1Filled == false)
        {
            towerSlot1.SetActive(true);
            openTowerSlot = towerSlot1.transform.GetChild(0).transform.GetChild(0).gameObject;
            towerSlot1Filled = true;
        }
        else if (towerSlot2Filled == false)
        {
            towerSlot2.SetActive(true);
            openTowerSlot = towerSlot2.transform.GetChild(0).transform.GetChild(0).gameObject;
            towerSlot2Filled = true;
        }
        else if (towerSlot3Filled == false)
        {
            towerSlot3.SetActive(true);
            openTowerSlot = towerSlot3.transform.GetChild(0).transform.GetChild(0).gameObject;
            towerSlot3Filled = true;
        }
        else if (towerSlot4Filled == false)
        {
            towerSlot4.SetActive(true);
            openTowerSlot = towerSlot4.transform.GetChild(0).transform.GetChild(0).gameObject;
            towerSlot4Filled = true;
        }
        else if (towerSlot5Filled == false)
        {
            towerSlot5.SetActive(true);
            openTowerSlot = towerSlot5.transform.GetChild(0).transform.GetChild(0).gameObject;
            towerSlot5Filled = true;
        }
        else
        {
            return;
        }
        GameObject buttonInst = Instantiate(button);
        buttonInst.transform.SetParent(openTowerSlot.transform);
        buttonInst.GetComponent<OnButtonTowerClick>().setTower(tower);
        openTowerSlot.transform.gameObject.transform.parent.gameObject.SetActive(true);
    }
}
