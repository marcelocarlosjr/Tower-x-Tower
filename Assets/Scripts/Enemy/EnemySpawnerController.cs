using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    public static List<GameObject> enemySpawnerList = new List<GameObject>();
    private CameraController cameraController;

    private void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
    }
    private void Update()
    {
        foreach (GameObject enemySpawner in EnemySpawnerController.enemySpawnerList)
        {
            if(enemySpawner.GetComponent<EnemySpawner>() && enemySpawner.GetComponent<EnemySpawner>().roomNumber == cameraController.CamPos)
            {
                enemySpawner.GetComponent<EnemySpawner>().enabled = true;
            }
            else if(enemySpawner.GetComponent<EnemySpawner>())
            {
                enemySpawner.GetComponent<EnemySpawner>().enabled = false;
            }
        }
    }
}
