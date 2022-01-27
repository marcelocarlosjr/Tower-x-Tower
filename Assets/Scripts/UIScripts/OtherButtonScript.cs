using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherButtonScript : MonoBehaviour
{

    public GameObject DamageLabel;
    public GameObject AttackSpeedLabel;
    public GameObject CDRLabel;
    public GameObject BulletSpeedLabel;
    public GameObject ExtraShotLabel;
    public GameObject SpreadLabel;
    public GameObject ExplosiveLabel;
    public GameObject PiercingLabel;

    public bool OtherPress = false;

    

    private void Update()
    {
        
        if(OtherPress == false)
        {
            
            DamageLabel.SetActive(true);
            AttackSpeedLabel.SetActive(true);
            CDRLabel.SetActive(true);
            BulletSpeedLabel.SetActive(true);
            ExtraShotLabel.SetActive(false);
            SpreadLabel.SetActive(false);
            ExplosiveLabel.SetActive(false);
            PiercingLabel.SetActive(false);
        }
        if(OtherPress == true)
        {
            DamageLabel.SetActive(false);
            AttackSpeedLabel.SetActive(false);
            CDRLabel.SetActive(false);
            BulletSpeedLabel.SetActive(false);
            ExtraShotLabel.SetActive(true);
            SpreadLabel.SetActive(true);
            ExplosiveLabel.SetActive(true);
            PiercingLabel.SetActive(true);
        }
    }

    public void OtherClick()
    {

        
        if (OtherPress == false)
        {
            
            OtherPress = true;
        }
        else
        {
            OtherPress = false;
        }
        

    }




}
