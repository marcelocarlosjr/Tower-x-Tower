using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammyShadowController : MonoBehaviour
{
    public Vector2 kingHammyLastPosition;

    public Vector2 kingHammyNewPosition;

    public Transform player;

    public bool followPlayer = true;

    public float followDelayCD;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject.transform;
    }

    private void Update()
    {
        if (followPlayer)
        {
            transform.position = Vector2.Lerp(transform.position, kingHammyNewPosition, followDelayCD * Time.deltaTime);
        }

        if (followPlayer)
        {
            follow();
        }
    }

    public void follow()
    {
        StartCoroutine(getLastPosDelay());
    }

    public IEnumerator getLastPosDelay()
    {
        kingHammyLastPosition = kingHammyNewPosition;
        kingHammyNewPosition = player.position;
        yield return new WaitForSeconds(followDelayCD);
    }
}
