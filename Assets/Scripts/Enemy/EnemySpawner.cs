using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public List<Transform> enemyTransform;
    private EnemyList enemies;

    public List<GameObject> enemiesAlive;

    private float enemySpawnCD;

    private float roomGenCD;

    public int roomNumber;

    private int floorLevel;

    private RoomTemplates roomTemplates;

    public int lastRoom;

    public int spawnNumber;

    public List<int> randSpaces;

    public List<int> k;

    public bool allEnemysKilled;

    public float updatingSpawnTimer;

    private void Awake()
    {
        roomTemplates = FindObjectOfType<RoomTemplates>();
        roomGenCD = roomTemplates.waitTime + 1;

        enemySpawnCD = 10;

        for (int i = 0; i < 5; i++)
        {
            randSpaces.Add(i);
        }

        EnemySpawnerController.enemySpawnerList.Add(gameObject);

        enemies = FindObjectOfType<EnemyList>();

        for(int i = 0; i < this.transform.childCount; i++)
        {
            enemyTransform.Add(this.transform.GetChild(i).gameObject.GetComponent<Transform>());
        }

        roomNumber = (int)(GetComponentInParent<Transform>().position.x - 8) / 16;

        Invoke("getLastRoom", roomGenCD);
    }

    private void getLastRoom()
    {
        lastRoom = roomTemplates.lastRoom;
    }

    private void Start()
    {
        updatingSpawnTimer = enemySpawnCD;
        if (((int)lastRoom * .33) > roomNumber && roomNumber > 0)
        {
            spawnNumber = 3;
        }
        else if (((int)lastRoom * .66) > roomNumber && roomNumber > ((int)lastRoom * .33))
        {
            spawnNumber = 4;
        }
        else if (((int)lastRoom * .66) < roomNumber)
        {
            spawnNumber = 5;
        }

        foreach(GameObject door in DoorSpawner.doorSpawnerList)
        {
            if (door.transform.parent.transform.position.x == roomNumber * 16)
            {
                door.GetComponent<DoorController>().close();
            }
        }

        Invoke("spawnEnemy", enemySpawnCD);
    }

    private void Update()
    {
        floorLevel = FindObjectOfType<PlayerController>().currentFloorNumber;

        if (updatingSpawnTimer > 0)
        {
            updatingSpawnTimer -= Time.deltaTime;
        }
        else
        {
            updatingSpawnTimer = 0.00f;
        }

        if (enemiesAlive.Capacity > 0)
        {

            for(int i = 0; i < enemiesAlive.Count; i++)
            {
                enemiesAlive.RemoveAll(i => i == null);
            }

            if(enemiesAlive.Count == 0 && allEnemiesSpawned)
            {
                allEnemysKilled = true;
                return;
            }
        }

        if (allEnemysKilled)
        {
            this.enabled = false;
        }
    }

    private void spawnEnemy()
    {
        allEnemiesSpawned = false;
        StartCoroutine(spawnEnemyDelay());
    }
    private bool allEnemiesSpawned;
    IEnumerator spawnEnemyDelay()
    {
        for (int i = 0; i < floorLevel; i++)
        {
            randSpaces.Sort((a, b) => 1 - 2 * Random.Range(0, 2));
            for (int j = 0; j < spawnNumber; j++)
            {
                int l = Random.Range(0, enemies.enemies.Count);

                GameObject temp = Instantiate(enemies.enemies[l], enemyTransform[randSpaces[j]].transform.position, Quaternion.identity);
                enemiesAlive.Add(temp);
            }
            yield return new WaitUntil(() => (enemiesAlive.Count == 0));
        }
        allEnemiesSpawned = true;
    }
}
