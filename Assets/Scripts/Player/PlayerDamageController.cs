using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageController : MonoBehaviour
{
    private PlayerHealthController PlayerHealth;

    float damage = 0;
    private void Start()
    {
        PlayerHealth = this.GetComponent<PlayerHealthController>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            damage = collision.gameObject.GetComponent<Enemy>().doMelee();
            PlayerHealth.takeDamage(damage);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Bat>())
        {
            damage = other.gameObject.GetComponent<Enemy>().doMelee();
            PlayerHealth.takeDamage(damage);
        }
    }

}
