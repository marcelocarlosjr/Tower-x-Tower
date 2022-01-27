using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenController : MonoBehaviour
{
    public int room;
    void Update()
    {

        room = FindObjectOfType<CameraController>().CamPos;

        if (this.transform.parent.transform.position.x == ((room * 16)-8))
        {
            this.GetComponent<DoorController>().close();
        }
    }
}
