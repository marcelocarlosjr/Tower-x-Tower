using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreTower : MonoBehaviour
{
    private InventoryUpdate inventoryUpdate;

    public GameObject tower1;
    public GameObject tower2;
    public GameObject tower3;
    public GameObject tower4;
    public GameObject tower5;

    public Vector2 towerLocation1Holder;
    public Vector2 towerLocation2Holder;
    public Vector2 towerLocation3Holder;
    public Vector2 towerLocation4Holder;
    public Vector2 towerLocation5Holder;

    public string currentSelectedUpgrade;

    public GameObject currentSelectedUpgradeButton;

    public string currentSelectedTowerTrigger;

    public bool upgradeSelected;

    public void replaceCurrentUpgrade(string newUpgrade, GameObject button)
    {
        currentSelectedUpgrade = newUpgrade;
        currentSelectedUpgradeButton = button;
    }

    public void triggerTowerCurrent(string newUpgrade)
    {
        currentSelectedTowerTrigger = newUpgrade;
        Invoke("towerCurrentNull", .5f);

    }

    public void towerCurrentNull()
    {
        currentSelectedTowerTrigger = null;
    }

    public void removeCurrentUpgrade()
    {
        currentSelectedUpgrade = null;
    }

    private Vector2 rayPos;

    private bool cameraChanged;

    void Start()
    {
        currentSelectedUpgrade = null;

        inventoryUpdate = FindObjectOfType<InventoryUpdate>();

        towerLocation1Holder = this.transform.GetChild(0).gameObject.transform.position;
        towerLocation2Holder = this.transform.GetChild(1).gameObject.transform.position;
        towerLocation3Holder = this.transform.GetChild(2).gameObject.transform.position;
        towerLocation4Holder = this.transform.GetChild(3).gameObject.transform.position;
        towerLocation5Holder = this.transform.GetChild(4).gameObject.transform.position;
    }

    public void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        rayPos = Camera.main.ScreenPointToRay(mousePos).origin;

        cameraChanged = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().camChanged;

        returnTowerOnCamera();

        if(currentSelectedUpgrade != null)
        {
            upgradeSelected = true;
        }
        else
        {
            upgradeSelected = false;
        }

    }

    public void returnAll()
    {
        if (tower1 != null)
        {
            TowerStore(1);
            tower1.GetComponent<Tower>().towerPlaced = false;
            tower1.GetComponent<Tower>().TowerSelected = false;
        }

        if (tower2 != null)
        {
            TowerStore(2);
            tower2.GetComponent<Tower>().towerPlaced = false;
            tower2.GetComponent<Tower>().TowerSelected = false;
        }

        if (tower3 != null)
        {
            TowerStore(3);
            tower3.GetComponent<Tower>().towerPlaced = false;
            tower3.GetComponent<Tower>().TowerSelected = false;
        }

        if (tower4 != null)
        {
            TowerStore(4);
            tower4.GetComponent<Tower>().towerPlaced = false;
            tower4.GetComponent<Tower>().TowerSelected = false;
        }

        if (tower5 != null)
        {
            TowerStore(5);
            tower5.GetComponent<Tower>().towerPlaced = false;
            tower5.GetComponent<Tower>().TowerSelected = false;
        }
    }

    public void returnTowerOnCamera()
    {
        if (cameraChanged)
        {

            returnAll();
        }
    }

    public void AddTower(GameObject tower, GameObject ui)
    {
        if (tower1 == null)
        {
            tower1 = Instantiate(tower, towerLocation1Holder, Quaternion.identity);
            tower1.transform.SetParent(transform);
            inventoryUpdate.addTowerButton(ui, tower1);
        }
        else if (tower2 == null)
        {
            tower2 = Instantiate(tower, towerLocation2Holder, Quaternion.identity);
            tower2.transform.SetParent(transform);
            inventoryUpdate.addTowerButton(ui, tower2);
        }
        else if (tower3 == null)
        {
            tower3 = Instantiate(tower, towerLocation3Holder, Quaternion.identity);
            tower3.transform.SetParent(transform);
            inventoryUpdate.addTowerButton(ui, tower3);
        }
        else if (tower4 == null)
        {
            tower4 = Instantiate(tower, towerLocation4Holder, Quaternion.identity);
            tower4.transform.SetParent(transform);
            inventoryUpdate.addTowerButton(ui, tower4);
        }
        else if (tower5 == null)
        {
            tower5 = Instantiate(tower, towerLocation5Holder, Quaternion.identity);
            tower5.transform.SetParent(transform);
            inventoryUpdate.addTowerButton(ui, tower5);
        }
        else
        {
            return;
        }
    }

    public void RemoveTower(int towerNum)
    {
        if (towerNum == 1)
        {
            tower1 = null;
        }
        if (towerNum == 2)
        {
            tower2 = null;
        }
        if (towerNum == 3)
        {
            tower3 = null;
        }
        if (towerNum == 4)
        {
            tower4 = null;
        }
        if (towerNum == 5)
        {
            tower5 = null;
        }
    }

    public void SwapTower(int start, int end)
    {
        GameObject tempStart;
        GameObject tempEnd;

        if (start == end || start == 0 || start > 5)
        {
            return;
        }

        if (start == 1)
        {
            tempStart = tower1;
            RemoveTower(1);
            if (end == 2)
            {
                tempEnd = tower2;
                RemoveTower(2);
                tower1 = tempEnd;
                tower2 = tempStart;
            }
            if (end == 3)
            {
                tempEnd = tower3;
                RemoveTower(3);
                tower1 = tempEnd;
                tower3 = tempStart;
            }
            if (end == 4)
            {
                tempEnd = tower4;
                RemoveTower(4);
                tower1 = tempEnd;
                tower4 = tempStart;
            }
            if (end == 5)
            {
                tempEnd = tower5;
                RemoveTower(5);
                tower1 = tempEnd;
                tower5 = tempStart;
            }
        }
        if (start == 2)
        {
            tempStart = tower2;
            RemoveTower(1);
            if (end == 1)
            {
                tempEnd = tower1;
                RemoveTower(1);
                tower2 = tempEnd;
                tower1 = tempStart;
            }
            if (end == 3)
            {
                tempEnd = tower3;
                RemoveTower(3);
                tower2 = tempEnd;
                tower3 = tempStart;
            }
            if (end == 4)
            {
                tempEnd = tower4;
                RemoveTower(4);
                tower2 = tempEnd;
                tower4 = tempStart;
            }
            if (end == 5)
            {
                tempEnd = tower5;
                RemoveTower(5);
                tower2 = tempEnd;
                tower5 = tempStart;
            }
        }
        if (start == 3)
        {
            tempStart = tower3;
            RemoveTower(3);
            if (end == 2)
            {
                tempEnd = tower2;
                RemoveTower(2);
                tower3 = tempEnd;
                tower2 = tempStart;
            }
            if (end == 1)
            {
                tempEnd = tower1;
                RemoveTower(1);
                tower3 = tempEnd;
                tower1 = tempStart;
            }
            if (end == 4)
            {
                tempEnd = tower4;
                RemoveTower(4);
                tower3 = tempEnd;
                tower4 = tempStart;
            }
            if (end == 5)
            {
                tempEnd = tower5;
                RemoveTower(5);
                tower3 = tempEnd;
                tower5 = tempStart;
            }
        }
        if (start == 4)
        {
            tempStart = tower4;
            RemoveTower(4);
            if (end == 2)
            {
                tempEnd = tower2;
                RemoveTower(2);
                tower4 = tempEnd;
                tower2 = tempStart;
            }
            if (end == 3)
            {
                tempEnd = tower3;
                RemoveTower(3);
                tower4 = tempEnd;
                tower3 = tempStart;
            }
            if (end == 1)
            {
                tempEnd = tower1;
                RemoveTower(1);
                tower4 = tempEnd;
                tower1 = tempStart;
            }
            if (end == 5)
            {
                tempEnd = tower5;
                RemoveTower(5);
                tower4 = tempEnd;
                tower5 = tempStart;
            }
        }
        if (start == 5)
        {
            tempStart = tower5;
            RemoveTower(5);
            if (end == 2)
            {
                tempEnd = tower2;
                RemoveTower(2);
                tower5 = tempEnd;
                tower2 = tempStart;
            }
            if (end == 3)
            {
                tempEnd = tower3;
                RemoveTower(3);
                tower5 = tempEnd;
                tower3 = tempStart;
            }
            if (end == 4)
            {
                tempEnd = tower4;
                RemoveTower(4);
                tower5 = tempEnd;
                tower4 = tempStart;
            }
            if (end == 1)
            {
                tempEnd = tower1;
                RemoveTower(1);
                tower5 = tempEnd;
                tower1 = tempStart;
            }
        }
    }

    public void TowerStore(int towerNum)
    {
        if(towerNum == 1 && tower1 != null)
        {
            tower1.transform.position = towerLocation1Holder;
        }
        if (towerNum == 2 && tower2 != null)
        {
            tower2.transform.position = towerLocation2Holder;
        }
        if (towerNum == 3 && tower3 != null)
        {
            tower3.transform.position = towerLocation3Holder;
        }
        if (towerNum == 4 && tower4 != null)
        {
            tower4.transform.position = towerLocation4Holder;
        }
        if (towerNum == 5 && tower5 != null)
        {
            tower5.transform.position = towerLocation5Holder;
        }
    }

    public void TowerGrab(int towerNum, Vector2 position)
    {
        if (towerNum == 1)
        {
            tower1.transform.position = position;
        }
        if (towerNum == 2)
        {
            tower2.transform.position = position;
        }
        if (towerNum == 3)
        {
            tower3.transform.position = position;
        }
        if (towerNum == 4)
        {
            tower4.transform.position = position;
        }
        if (towerNum == 5)
        {
            tower5.transform.position = position;
        }
    }

    public void userPlaceTower(int towerNum)
    {
        if (tower1 != null)
        {
            if (towerNum == 1)
            {
                if (tower1.GetComponent<Tower>().towerPlaced == false)
                {

                    FindObjectOfType<AudioManager>().Play("B_PlaceTower");
                    tower1.GetComponent<Tower>().towerPlaced = true;
                    tower1.transform.position = rayPos;
                }
            }
        }

        if (tower2 != null)
        {
            if (towerNum == 2)
            {
                if (tower2.GetComponent<Tower>().towerPlaced == false)
                {
                    FindObjectOfType<AudioManager>().Play("B_PlaceTower");
                    tower2.GetComponent<Tower>().towerPlaced = true;
                    tower2.transform.position = rayPos;
                }
            }
        }

        if (tower3 != null)
        {
            if (towerNum == 3)
            {
                if (tower3.GetComponent<Tower>().towerPlaced == false)
                {
                    FindObjectOfType<AudioManager>().Play("B_PlaceTower");
                    tower3.GetComponent<Tower>().towerPlaced = true;
                    tower3.transform.position = rayPos;
                }
            }
        }

        if (tower4 != null)
        {
            if (towerNum == 4)
            {
                if (tower4.GetComponent<Tower>().towerPlaced == false)
                {
                    FindObjectOfType<AudioManager>().Play("B_PlaceTower");
                    tower4.GetComponent<Tower>().towerPlaced = true;
                    tower4.transform.position = rayPos;
                }
            }
        }

        if (tower5 != null)
        {
            if (towerNum == 5)
            {
                if (tower5.GetComponent<Tower>().towerPlaced == false)
                {
                    FindObjectOfType<AudioManager>().Play("B_PlaceTower");
                    tower5.GetComponent<Tower>().towerPlaced = true;
                    tower5.transform.position = rayPos;
                }
            }
        }
    }
}
