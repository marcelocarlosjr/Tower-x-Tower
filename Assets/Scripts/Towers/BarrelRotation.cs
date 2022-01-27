using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelRotation : MonoBehaviour
{
    public Transform pivot;
    public Transform barrel;

    public Tower tower;
    private void Update()
    {
        if (tower.towerType != Tower.TowerType.blade)
        {
            if (tower.autoShoot == true)
            {
                if (tower != null)
                {
                    if (tower.currentTarget != null)
                    {
                        Vector2 relative = tower.currentTarget.transform.position - pivot.position;

                        float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;

                        Vector3 newRotation = new Vector3(0, 0, angle);

                        pivot.localRotation = Quaternion.Euler(newRotation);
                    }
                }
            }

            if (tower.autoShoot == false && tower.TowerSelected)
            {
                if (tower != null)
                {
                    Vector3 mousePos = Input.mousePosition;
                    mousePos.z = Mathf.Infinity;

                    Ray rayPos = Camera.main.ScreenPointToRay(mousePos);

                    Vector2 relative = rayPos.origin - pivot.position;

                    float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;

                    Vector3 newRotation = new Vector3(0, 0, angle);

                    pivot.localRotation = Quaternion.Euler(newRotation);
                }
            }
        }
        else
        {
            if(tower.towerType == Tower.TowerType.blade && tower.towerPlaced)
            {
                pivot.Rotate(0, 0, 360 / (tower.shotDelay) * Time.deltaTime);
            }
        }
    }
}
