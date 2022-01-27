using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StairsController : MonoBehaviour
{
    public int floorNumber;

    private void Start()
    {
        floorNumber = FindObjectOfType<PlayerController>().currentFloorNumber;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(floorNumber == 3)
            {
                FindObjectOfType<PauseController>().winGame();
            }
            SceneManager.LoadScene("Floor" + (floorNumber + 1));
        }
    }

    public void stairsOn()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
    }
}
