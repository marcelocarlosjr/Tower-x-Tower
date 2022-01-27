using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTowerAbility1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().slow(0, 5);
        }
    }
}