using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSupportBullet : Bullet
{
    public float cooldown;
    public float multiplier;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Wall"))
        {
            Object.Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "Tower" && !collision.gameObject.GetComponent<BarrelTower>())
        {
            collision.GetComponent<Tower>().autoTurretBuff(multiplier, cooldown, this.gameObject);
        }
    }
}
