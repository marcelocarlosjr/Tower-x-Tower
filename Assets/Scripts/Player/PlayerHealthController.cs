using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    private SpriteRenderer spriteRenderer;
    private bool blinking = false;
    public GameObject hpBar;
    public RectTransform hpTran;
    public GameObject deathMenu;
    private void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();

        hpTran = hpBar.GetComponent<RectTransform>();
    }

    public void Update()
    {
        hpTran.localScale = new Vector2(currentHealth / (float)maxHealth, 1);

        if(currentHealth <= 0)
        {
            die();
        }
    }

    private void die()
    {
        Time.timeScale = 0;
        deathMenu.SetActive(true);
        FindObjectOfType<AudioManager>().Pause("B_Footstep");

    }

    public void takeDamage(float damage)
    {
        if(damage > 0)
        {
            FindObjectOfType<AudioManager>().Play("B_Hurt");
        }
        
        currentHealth -= damage;
        if (!blinking && damage != 0)
        {
            StartCoroutine("damageBlink");
        }
    }

    private IEnumerator damageBlink()
    {
        blinking = true;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
        blinking = false;


    }

    public void addMaxHp(float amount)
    {
        maxHealth += amount;
        if(currentHealth + amount >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }
    }
}
