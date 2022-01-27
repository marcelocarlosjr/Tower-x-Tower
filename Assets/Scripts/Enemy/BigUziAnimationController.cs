using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigUziAnimationController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void startKatanaAnimation()
    {
        animator.SetTrigger("katana");
    }

    public void stopKatanaAnimation()
    {
        animator.SetTrigger("katanaStop");
    }

    public void setSpin(bool i)
    {
        animator.SetBool("spinning", i);
    }

    public void shootShotgun(int dir)
    {
        animator.SetInteger("dir", dir);
        animator.SetTrigger("shotgun");
    }

    public void shootUzi(int dir)
    {
        animator.SetInteger("dir", dir);
        animator.SetTrigger("shootUzi");
    }

    public void rotate(int r)
    {
        if (r == 0)
        {
            animator.SetBool("spinDir", false);
        }
        else if(r == 1)
        {
            animator.SetBool("spinDir", true);
        }
        animator.SetTrigger("spin");
    }

    public void stopRotate()
    {
        animator.SetTrigger("stopSpin");
    }
}
