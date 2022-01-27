using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    public static List<GameObject> enemyList = new List<GameObject>();

    public List<GameObject> enemies;

    public List<GameObject> bosses;

    private void Awake()
    {
    }
}
