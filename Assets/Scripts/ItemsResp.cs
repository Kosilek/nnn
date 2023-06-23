using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsResp : MonoBehaviour
{
    public GameObject[] itemDrop;
    private GameObject item;
    public int lvlLocation;
    GeneralActive ga = new GeneralActive();

    private void Start()
    {
        Rand();
        DropItem();
    }
    public void DropItem()
    {
        int min, max;
        (min, max) = RandLvlItem();
        item.GetComponent<Item>().levelItem = ga.Rand(min, max);
        item.GetComponent<Item>().customizable = false;
        item.GetComponent<Item>().dropItemBool = true;
        if (item.GetComponent<Item>().isStackable == true)
        {
            item.GetComponent<Item>().countItem = ga.Rand(1, 10);
        }
        Instantiate(item, transform.position, transform.rotation);
    }

    private (int, int) RandLvlItem()
    {
        int min = 0, max = 0;
        if (lvlLocation == 1)
        {
            min = 1;
            max = 2;
        }
        else if (lvlLocation > 1)
        {
            min = lvlLocation - 1;
            max = lvlLocation + 1;
        }
        return (min, max);
    }

    private void Rand()
    {
        int i;
        i = ga.Rand(itemDrop.Length);
        item = itemDrop[i];
    }
}
