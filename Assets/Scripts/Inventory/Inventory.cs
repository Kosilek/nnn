using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : Singleton<Inventory>
{
    public static List<Item> item;
    public List<GameObject> itemPubl;
    public List<Vector3> positionCell;
    //[SerializeField] private GameObject cellContainer;
    //public static GameObject saveCellContainer;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        item = new List<Item>();
        positionCell = new List<Vector3>();
        for (int i = 0; i < itemPubl.Count; i++)
        {
            //cellContainer.transform.GetChild(i).GetComponent<CurrentItem>().index = i;
            itemPubl[i].GetComponent<CurrentItem>().index = i;
            positionCell.Add(itemPubl[i].transform.position);
                item.Add(new Item());
        }
        
    }

   /* public static GameObject GetChild(int index)
    {
        return saveCellContainer.transform.GetChild(index).gameObject;
    }*/

    public static void PickupTrigger(GameObject items, bool isStackable, int id, int itemCount)
    {
        if (isStackable)
        {
            AddStackableItem(items, id, itemCount);
        }
        else if (!isStackable)
        {
            AddUnStackableItem(items);
        }
    }

    private static void AddStackableItem(GameObject items, int id, int itemCount)
    {
        for (int i = 0; i < item.Count; i++)
        {
            if (item[i].id == id)
            {
                item[i].countItem += itemCount;
                Destroy(items);
                return;
            }
        }
        AddUnStackableItem(items);
    }

    private static void AddUnStackableItem(GameObject items)
    {
        for (int i = 0; i < item.Count; i++)
        {
            if (item[i].id == 0)
            {
                item[i] = items.GetComponent<Item>();
                Destroy(items);
                break;
            }
        }
    }

    public static void DeleteItem(int index, GameObject icon, Text txt, GameObject answer)
    {
        item[index].id = 0;
        icon.GetComponent<Image>().enabled = false;
        icon.GetComponent<Image>().sprite = null;
        txt.text = null;
    }


    public void DisplayItem()
    {
        for (int i = 0; i < item.Count; i++)
        {
            //Transform cell = cellContainer.transform.GetChild(i);
            Transform cell = itemPubl[i].transform;
            Transform icon = cell.GetChild(0);
            Transform count = icon.GetChild(0);
            Text txt = count.GetComponent<Text>();
            Image img = icon.GetComponent<Image>();
            if (item[i].id != 0)
            {
                img.enabled = true;
                img.sprite = Resources.Load<Image>(item[i].pathIcon).sprite;
                if (item[i].countItem == 1)
                {
                    txt.text = null;
                }
                else if (item[i].countItem != 1)
                {
                    txt.text = item[i].countItem.ToString();
                }
            }
            else if (item[i].id == 0)
            {
                img.enabled = false;
                img.sprite = null;
                txt.text = null;
            }
        }
    }
}
