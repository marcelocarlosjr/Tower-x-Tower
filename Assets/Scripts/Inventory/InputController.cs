using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Ray ray;
    private RaycastHit2D hit;

    public bool Use(int num)
    {
        return Input.GetButtonDown("Use" + num);
    }
    
    public RaycastHit2D MousePosHit()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit = Physics2D.Raycast(ray.origin, ray.direction, 10);
        return hit;
    }

    public bool MouseLClick()
    {
        return (Input.GetAxisRaw("Fire1") > 0);
    }
    
    public bool PickupTower()
    {
        return (Input.GetAxisRaw("TowerPickup") > 0);
    }

    public bool UseAbility(int num)
    {
        return Input.GetButtonDown("UseAbility" + num);
    }
}
