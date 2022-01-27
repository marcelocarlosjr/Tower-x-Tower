using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<PlayerController>().transform.position = this.transform.position;
    }
}
