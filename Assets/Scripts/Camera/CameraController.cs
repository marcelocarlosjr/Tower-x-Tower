using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int CamPos = 0;
    private int RoomX;
    public GameObject Player;
    public float CamSpeed = 20f;
    private int tempCamPos;

    public bool camChanged;

    void Start()
    {
        CamPos = 0;
        tempCamPos = CamPos;
    }


    void Update()
    {
        CamChangeRoom();
        RoomChanged();
    }

    public void RoomChanged()
    {
        if(tempCamPos != CamPos)
        {
            camChanged = true;
            tempCamPos = CamPos;
            Invoke("setFalse", 0.5f);
        }
    }

    public void setFalse()
    {
        camChanged = false;
    }

    public void CamChangeRoom()
    {
        CamPos = ((int)Player.transform.position.x / 16);
        RoomX = ((16 * CamPos) + 8);

        transform.position = Vector3.Lerp(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z),
            new Vector3(RoomX, this.transform.position.y, this.transform.position.z), CamSpeed * Time.deltaTime);
    }
}
