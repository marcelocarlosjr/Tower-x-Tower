using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerType : MonoBehaviour
{
    public int Cost;
    public enum TowerTypes
    {
        auto,
        freeze,
        sniper,
        fast,
        shotgun,
        cannon,
        gate,
        barrel,
        blade,
        support
    }

    public TowerTypes towerType;
}
