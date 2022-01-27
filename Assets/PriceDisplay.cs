using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PriceDisplay : MonoBehaviour
{
    public int cost;
    
    void Start()
    {
        if (GetComponent<TowerType>())
        {
            cost = GetComponent<TowerType>().Cost;
            transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = cost.ToString() + "g";
        }
        else if (GetComponent<Upgrade>())
        {
            cost = GetComponent<Upgrade>().Cost;
            transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = cost.ToString() + "g";
        }
        

       

    }

    
}
