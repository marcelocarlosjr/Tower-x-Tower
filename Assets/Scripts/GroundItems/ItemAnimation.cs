using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimation : MonoBehaviour
{
    private Transform item;
    Vector2 posStart;
    Vector3 tempPos;
    public float amplitude = 0.5f;
    public float frequency = 1f;
    void Start()
    {
        item = this.transform;
        posStart = this.transform.position;
    }

    void Update()
    {
        tempPos = posStart;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        item.transform.position = tempPos;
    }
}
