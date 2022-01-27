using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAntiStuck : MonoBehaviour
{
    public float delta = .5f;
    public enum WallSelector
    {
        topWall,
        bottomWall,
        rightWall,
        leftWall
    }

    public WallSelector wallSelector;

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<CircleCollider2D>() != null)
        {
            CircleCollider2D colliderSize = other.gameObject.GetComponent<CircleCollider2D>();
            delta = colliderSize.radius;
        }
        else if (other.gameObject.GetComponent<BoxCollider2D>() != null)
        {
            BoxCollider2D colliderSize = other.gameObject.GetComponent<BoxCollider2D>();
            delta = colliderSize.size.x/2;
        }

        if(wallSelector == WallSelector.topWall)
        {
            other.transform.position += new Vector3(0, delta*-1, 0);
        }

        if(wallSelector == WallSelector.bottomWall)
        {
            other.transform.position += new Vector3(0, delta, 0);
        }

        if (wallSelector == WallSelector.rightWall)
        {
            other.transform.position += new Vector3(delta * -1, 0, 0);
        }

        if (wallSelector == WallSelector.leftWall)
        {
            other.transform.position += new Vector3(delta, 0, 0);
        }
    }
}
