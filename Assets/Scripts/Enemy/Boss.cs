using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : Enemy
{
    public float abilityOneCD;
    public float abilityTwoCD;
    public float abilityThreeCD;

    public bool abilityOneOnCD;
    public bool abilityTwoOnCD;
    public bool abilityThreeOnCD;

    private int lastRoom;

    public IEnumerator abilityOneTimer()
    {
        abilityOneOnCD = true;
        yield return new WaitForSeconds(abilityOneCD);
        abilityOneOnCD = false;
    }
    protected virtual void abilityOne()
    {
        StartCoroutine(abilityOneTimer());
    }




    public IEnumerator abilityTwoTimer()
    {
        abilityTwoOnCD = true;
        yield return new WaitForSeconds(abilityTwoCD);
        abilityTwoOnCD = false;
    }
    protected virtual void abilityTwo()
    {
        StartCoroutine(abilityTwoTimer());
    }




    public IEnumerator abilityThreeTimer()
    {
        abilityThreeOnCD = true;
        yield return new WaitForSeconds(abilityThreeCD);
        abilityThreeOnCD = false;
    }
    protected virtual void abilityThree()
    {
        StartCoroutine(abilityThreeTimer());
    }

    protected override void Die()
    {
        lastRoom = FindObjectOfType<RoomTemplates>().lastRoom;
        EnemySpawnerController.enemySpawnerList[lastRoom - 2].GetComponent<EnemySpawner>().allEnemysKilled = true;
        FindObjectOfType<StairsController>().stairsOn();
        enemyHealth = 0;
        base.Die();
    }
}
