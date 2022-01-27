using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgradeController : MonoBehaviour
{
    public Tower tower;

    public int tower_speed;
    public float tower_speedMulti;

    public int tower_damage;
    public float tower_damageMulti;

    public int tower_cooldown;
    public float tower_cooldownMulti;

    public int tower_bulletSpeed;
    public float tower_bulletSpeedMulti;

    public int tower_extraShot;

    public int tower_spread;

    public int tower_explosive;
    public float tower_explosiveMulti;

    public bool tower_piercing;
    private void Start()
    {
        tower_speedMulti = 0.85f;
        tower_damageMulti = 1.15f;
        tower_cooldownMulti = 0.85f;
        tower_bulletSpeedMulti = 1.2f;
        tower_explosiveMulti = 0.10f;

        tower = this.gameObject.GetComponent<Tower>();
    }
    public void Upgrade(string type)
    {
        if(type == "tower_damage")
        {
            tower.damage *= tower_damageMulti;
        }
        if (type == "tower_speed")
        {
            tower.shotDelay *= tower_speedMulti;
        }
        if (type == "tower_cooldown")
        {
            tower.addCDR(tower_cooldownMulti);
        }
        if (type == "tower_bulletSpeed")
        {
            tower.bulletSpeed *= tower_bulletSpeedMulti;
        }
        if (type == "tower_extraShot")
        {
            tower.extraShot++;
        }
        if (type == "tower_explosive")
        {
            if (!tower.explode)
            {
                tower.explode = true;
            }
            tower.explosive += tower_explosiveMulti;
        }
        if (type == "tower_piercing")
        {
            tower.piercingBullet = true;
        }
        if (type == "tower_spread")
        {
            tower.spread++;
        }
    }
}
