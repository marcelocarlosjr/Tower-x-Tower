using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelTower : Tower
{
    public float towerSetupTime;
    public bool towerExploded;
    private bool freeze;
    public CircleCollider2D explosionCollider;
    private Animator animator;

    public bool abilityTwoBuff2;

    private float tempCD;

    private void Start()
    {
        animator = GetComponent<Animator>();
        towerType = TowerType.barrel;
        explosionCollider.enabled = false;
    }
    protected override void Update()
    {
        base.Update();

        explosionCollider.radius = bulletSpeed;
    }

    public IEnumerator ExplodeTimer(float range)
    {
        towerExploded = true;
        animator.SetTrigger("explode");
        explosionCollider.enabled = true;
        sizeUp(range);
        Invoke("collisionOff", .2f);
        yield return new WaitForSeconds(shotDelay);
        animator.SetTrigger("fix");
        towerExploded = false;
    }

    private void sizeUp(float r)
    {
        transform.localScale = new Vector2(r, r);
    }

    private void sizeDown()
    {
        transform.localScale = new Vector2(.9f, .9f);
    }

    private void explodeBarrel(float range)
    {
        if (!towerExploded)
        {
            FindObjectOfType<AudioManager>().Play("barrelExplode");
            StartCoroutine(ExplodeTimer(range));
        }
    }

    private void collisionOff()
    {
        explosionCollider.enabled = false;
        sizeDown();
    }

    protected override void abilityOne()
    {
        StartCoroutine(abilityOneDurration());
        base.abilityOne();
    }

    public IEnumerator abilityOneDurration()
    {
        tempCD = shotDelay;
        shotDelay = .5f;
        yield return new WaitForSeconds(10);
        shotDelay = tempCD;
    }

    protected override void abilityTwo()
    {
        StartCoroutine(abilityTwoDurration());
        base.abilityTwo();
    }

    public IEnumerator abilityTwoDurration()
    {
        abilityTwoBuff2 = true;
        yield return new WaitForSeconds(10);
        abilityTwoBuff2 = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "GoodProjectile" && !other.gameObject.GetComponent<AutoSupportBullet>())
        {
            if (abilityTwoBuff2)
            {
                explodeBarrel(4);
            }
            else
            {
                explodeBarrel(this.transform.localScale.x);
            }
            damage = other.GetComponent<Bullet>().bulletDamage;
            Object.Destroy(other.gameObject);
        }
    }
}
