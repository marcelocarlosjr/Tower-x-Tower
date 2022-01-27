using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private int lastDir = 2;
    private bool playerIdle;
    private bool towerPickedUp;

    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    private void Update()
    {
        lastDir = this.GetComponent<TowerInteract>().getLastDir();
        playerIdle = this.GetComponent<PlayerController>().playerIdle;
        towerPickedUp = this.GetComponent<TowerInteract>().towerPickedUp;
        animate();

    }

    private void animate()
    {
        animator.SetInteger("walkDirection", lastDir);
        animator.SetBool("idle", playerIdle);
        animator.SetBool("towerPickedUp", towerPickedUp);
    }
}
