using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigUziVertKatanaController : MonoBehaviour
{
    private BigUziAnimationController animator;
    private BigUziVert controller;

    private void Start()
    {
        animator = transform.parent.GetComponent<BigUziAnimationController>();
        controller = transform.parent.GetComponent<BigUziVert>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (!controller.abilityOneBuff && !controller.abilityTwoBuff && !controller.abilityThreeBuff && !controller.spawning)
            {
                collision.GetComponent<PlayerHealthController>().takeDamage(transform.parent.GetComponent<Enemy>().doMelee());
                animator.startKatanaAnimation();
                controller.meleeing = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!controller.abilityOneBuff)
            {
                animator.stopKatanaAnimation();
                Invoke("stopMelee", 0.5f);
            }
        }
    }

    public void stopMelee()
    {
        controller.meleeing = false;
    }
}
