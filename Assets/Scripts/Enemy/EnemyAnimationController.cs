using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator animator;
    private int lastDir = 2;
    private bool canMelee;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        lastDir = this.GetComponent<Enemy>().lastDir;
        canMelee = this.GetComponent<Enemy>().canMelee;
        animate();
    }

    private void animate()
    {
        animator.SetInteger("dir", lastDir);
    }

    public void animateAttack()
    {
        animator.SetTrigger("attack");
    }

    public void animateRanged()
    {
        animator.SetTrigger("ranged");
    }

    public void animateSlam()
    {
        GetComponent<KingHammy>().flying = true;
        animator.SetBool("slaming", true);
        Invoke("slaming", 4f);
    }

    private void slaming()
    {
        animator.SetBool("slaming", false);
        GetComponent<KingHammy>().flying = false;
    }

    public void animateCharge()
    {
        animator.SetBool("charging", true);
    }

    public void stopCharge()
    {
        animator.SetBool("charging", false);
    }

}
