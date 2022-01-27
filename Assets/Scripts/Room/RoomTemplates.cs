using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public int minHallways = 5;

    public GameObject[] leftDoorRooms;

    public List<GameObject> rooms;

    public GameObject RoomsParent;

    public float waitTime = 4f;
    public bool spawnedBoss = false;
    public GameObject boss;

    public int lastRoom;

    private void Update()
    {
        if (waitTime <= 0 && spawnedBoss == false)
        {
            for(int i = 0; i< rooms.Count; i++)
            {
                if (i == rooms.Count - 1)
                {
                    lastRoom = rooms.Count;
                    if (FindObjectOfType<CameraController>().CamPos + 1 == lastRoom)
                    {
                        Instantiate(boss,rooms[i].transform.position, Quaternion.identity);
                        spawnedBoss = true;
                    }
                }
            }
        }
        else
        {
            if (waitTime >= 0)
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    //RIGHT DOOR ROOMS ARE REMOVED BC NOT NEEDED
    //public GameObject[] rightDoorRooms;
}
