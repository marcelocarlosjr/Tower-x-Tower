using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    private int rand;
    private bool spawned = false;

    public float waitTime = 4f;

    private RoomTemplates templates;

    // 1 --> need a left door
    // 2 --> need a right door


    void Start()
    {
        Destroy(gameObject,waitTime);
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    void Spawn()
    {
        if(spawned == false)
        {
            if (openingDirection == 2)
            {
                if (templates.minHallways > 0) {
                    rand = Random.Range(1, templates.leftDoorRooms.Length);
                    Instantiate(templates.leftDoorRooms[rand], transform.position, templates.leftDoorRooms[rand].transform.rotation).transform.SetParent(templates.RoomsParent.transform);
                    spawned = true;
                    templates.minHallways--;
                }else if(templates.minHallways <= 0){
                    rand = Random.Range(0, 3);
                    Instantiate(templates.leftDoorRooms[rand], transform.position, templates.leftDoorRooms[rand].transform.rotation).transform.SetParent(templates.RoomsParent.transform);
                    spawned = true;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.CompareTag("SpawnPoint"))
        {
            Destroy(gameObject);
        }
    }
}
