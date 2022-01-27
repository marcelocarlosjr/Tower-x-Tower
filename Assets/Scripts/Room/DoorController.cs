using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorController : MonoBehaviour
{
    private BoxCollider2D[] ClosedCollider;

    public int lastRoom;

    private Animator animator;

    public int room;

    public bool openDoor;

    public bool bossKilled;

    //private TilemapCollider2D TilemapCollider;

    private void Awake()
    {
        DoorSpawner.doorSpawnerList.Add(transform.parent.gameObject);
    }

    private void Start()
    {
        ClosedCollider = GetComponents<BoxCollider2D>();

        ClosedCollider[0].enabled = true;
        ClosedCollider[1].enabled = true;
        ClosedCollider[2].enabled = true;

        animator = GetComponent<Animator>();

        //opens first door
        if (transform.parent.transform.position.x == 16)
        {
            open();
        }

        Invoke("getLastRoom", FindObjectOfType<RoomTemplates>().waitTime + 0.5f);
    }

    private void getLastRoom()
    {
        lastRoom = FindObjectOfType<RoomTemplates>().lastRoom;
    }

    private void Update()
    {
        room = FindObjectOfType<CameraController>().CamPos;
        //closes door behind the player

        if (room > 0 && room < lastRoom)
        {
            if (transform.parent.transform.position.x == ((room * 16)))
            {
                if ((openDoor && !EnemySpawnerController.enemySpawnerList[room - 1].GetComponent<EnemySpawner>().allEnemysKilled))
                {
                    close();
                }
            }


            if (EnemySpawnerController.enemySpawnerList[room - 1].GetComponent<EnemySpawner>() && EnemySpawnerController.enemySpawnerList[room - 1].GetComponent<EnemySpawner>().allEnemysKilled)
            {
                if (transform.parent.transform.position.x == EnemySpawnerController.enemySpawnerList[room - 1].transform.position.x + 8)
                {
                    if (!openDoor)
                    {
                        open();
                    }
                }
            }

            if (EnemySpawnerController.enemySpawnerList[room - 1].GetComponent<EnemySpawner>().allEnemysKilled)
            {
                if (transform.parent.transform.position.x == EnemySpawnerController.enemySpawnerList[room - 1].transform.position.x - 8)
                {
                    if (!openDoor)
                    {
                        open();
                    }
                }
            }

            if(EnemySpawnerController.enemySpawnerList[lastRoom - 2].GetComponent<EnemySpawner>().allEnemysKilled && bossKilled)
            {
                if(transform.parent.transform.position.x == 0){
                    open();
                }
            }
        }
    }

    public void open()
    {
        FindObjectOfType<AudioManager>().Play("roomDoorOpen");
        openDoor = true;
        animator.SetTrigger("open");
        ClosedCollider[2].enabled = false;
    }

    public void close()
    {
        FindObjectOfType<AudioManager>().Play("doorClose");
        openDoor = false;
        animator.SetTrigger("close");
        ClosedCollider[2].enabled = true;
    }
}
