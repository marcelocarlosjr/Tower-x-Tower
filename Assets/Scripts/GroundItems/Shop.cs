using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private InventoryController inventory;

    public List<int> randNums;

    public List<Transform> itemTransform;
    public List<GameObject> item;

    public enum ShopType
    {
        upgrades,
        towers
    }
    public ShopType shopType;

    private void Start()
    {
        inventory = FindObjectOfType<InventoryController>();

        if(shopType == ShopType.towers)
        {
            for (int i = 0; i < 5; i++)
            {

                randNums[i] = Random.Range(0, inventory.towerPickups.Count);

                itemTransform[i] = this.transform.GetChild(i).gameObject.GetComponent<Transform>();
                item[i] = Instantiate(inventory.towerPickups[randNums[i]].gameObject, itemTransform[i].position, Quaternion.identity);
                item[i].transform.SetParent(this.transform);

                Object.Destroy(itemTransform[i].gameObject);
            }
        }

        if(shopType == ShopType.upgrades)
        {
            for (int i = 0; i < 5; i++)
            {
                    randNums[i] = Random.Range(0, inventory.upgradePickups.Count);

                    itemTransform[i] = this.transform.GetChild(i).gameObject.GetComponent<Transform>();
                    item[i] = Instantiate(inventory.upgradePickups[randNums[i]].gameObject, itemTransform[i].position, Quaternion.identity);
                    item[i].transform.SetParent(this.transform);

                    Object.Destroy(itemTransform[i].gameObject);
            }
        }
    }
}
