using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : Singleton<Inventory>
{
    public List<Item> item;
    public List<GameObject> itemPubl;
    public List<Vector3> positionCell;

    public GameObject panelStatistics;
    public Text nameItem;
    public Text lvlItem;
    public Text description;
    public GameObject[] nameStatistics;
    public Text[] txtNameStatistics;
    public GameObject[] valueStatistics;
    public Text[] txtValueStatistics;

    private int[] checkStatistics;

    protected override void Awake()
    {
        base.Awake();

    }

    private void Start()
    {
        checkStatistics = new int[7];
        item = new List<Item>();
        positionCell = new List<Vector3>();
        for (int i = 0; i < itemPubl.Count; i++)
        {
            itemPubl[i].GetComponent<CurrentItem>().index = i;
            positionCell.Add(itemPubl[i].transform.position);
            item.Add(new Item());
        }

        for (int i = 0; i < nameStatistics.Length; i++)
        {
            nameStatistics[i].SetActive(false);
            valueStatistics[i].SetActive(false);
        }

        Event.OnAddStackableItem.AddListener(AddStackableItem);
        Event.OnAddUnStackableItem.AddListener(AddUnStackableItem);
    }

    #region panelStatstics
    public void panelStatiscticsActive(int index)
    {
        int value;
        string nameStatisticsA;
        float valueStatisticsA;
        value = checkActiveStatistics(index);
       // Debug.Log(value);
        TextMainItem(index);
        for (int i = 0; i < value; i++)
        {
            nameStatistics[i].SetActive(true);
            valueStatistics[i].SetActive(true);
            for (int j = 0; j < checkStatistics.Length; j++)
            {
                if (checkStatistics[j] != 0)
                {
                   // Debug.Log($"j = {j}");
                    (nameStatisticsA, valueStatisticsA) = AttributeDefinition(j, index);
                    txtNameStatistics[i].text = nameStatisticsA.ToString();
                    txtValueStatistics[i].text = valueStatisticsA.ToString();
                    Debug.Log($"i = {i}, name = {nameStatisticsA}, value = {valueStatisticsA}");
                    checkStatistics[j] = 0;
                    break;
                }
            }
        }
    }

    private void TextMainItem(int index)
    {
        nameItem.text = item[index].nameItem.ToString();
        lvlItem.text = item[index].levelItem.ToString();
        description.text = item[index].description.ToString();
    }

    private (string, float) AttributeDefinition(int index, int itemIndex)
    {
        string str = "";
        float f = 0f;
        switch(index)
        {
            case 0:
                (str, f) = (MeaningString.damage, item[itemIndex].damage);
                break;
            case 1:
                (str, f) = (MeaningString.armor, item[itemIndex].armor);
                break;
            case 2:
                (str, f) = (MeaningString.heatlh, item[itemIndex].health);
                break;
            case 3:
                (str, f) = (MeaningString.resistance, item[itemIndex].resistance);
                break;
            case 4:
                (str, f) = (MeaningString.spike, item[itemIndex].spike);
                break;
            case 5:
                (str, f) = (MeaningString.speed, item[itemIndex].speed);
                break;
            case 6:
                (str, f) = (MeaningString.vampirism, item[itemIndex].vampirism);
                break;
        }
        return (str, f);
    }

    private int checkActiveStatistics(int index)
    {
        int value = 0;
        (checkStatistics[0], checkStatistics[1], checkStatistics[2], checkStatistics[3], checkStatistics[4], checkStatistics[5], checkStatistics[6]) = 
            (item[index].damageInt, item[index].armorInt, item[index].healthInt, item[index].resistanceInt, item[index].spikeInt, item[index].speedInt, item[index].vampirismInt);
        for (int i = 0; i < checkStatistics.Length; i++)
        {
            if (checkStatistics[i] != 0)
            {
                value++;
            }
        }
        return value;
    }

    public void DeleteArray()
    {
        for (int i = 0; i < checkStatistics.Length; i++)
        {
            if (checkStatistics[i] != 0)
            {
                checkStatistics[i] = 0;
            }
        }
        for(int i = 0; i < nameStatistics.Length; i++)
        {
            nameStatistics[i].SetActive(false);
            valueStatistics[i].SetActive(false);
        }
    }
    #endregion

    public static void PickupTrigger(GameObject items, bool isStackable, int id, int itemCount)
    {
        if (isStackable)
        {
            Event.SendAddStackableItem(items, id, itemCount);
        }
        else if (!isStackable)
        {
           Event.SendAddUnStackableItem(items);
        }
    }

    private void AddStackableItem(GameObject items, int id, int itemCount)
    {
        for (int i = 0; i < item.Count - 9; i++)
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

    private void AddUnStackableItem(GameObject items)
    {
        for (int i = 0; i < item.Count - 9; i++)
        {
            if (item[i].id == 0)
            {
                item[i] = items.GetComponent<Item>();
                Destroy(items);
                break;
            }
        }
    }

    public void DeleteItem(int index, GameObject icon, Text txt, GameObject answer)
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

    public void LoadData(Save.itemSaveData save, int index)
    {
        item[index].typeItem = save.typeItem;
        item[index].levelItem = save.levelItem;
        item[index].nameItem = save.nameItem;
        item[index].id = save.id;
        item[index].countItem = save.countItem;
        item[index].isStackable = save.isStackable;
        item[index].pathIcon = save.pathIcon;
        item[index].pathPrefab = save.pathPrefab;
        item[index].customizable = save.customizable;
        item[index].description = save.description;
        item[index].damage = save.damage;
        item[index].armor = save.armor;
        item[index].health = save.health;
        item[index].resistance = save.resistance;
        item[index].spike = save.spike;
        item[index].speed = save.speed;
        item[index].vampirism = save.vampirism;
        item[index].typeRest = save.typeRest;
        item[index].damageBool = save.damageBool;
        item[index].armorBool = save.armorBool;
        item[index].healthBool = save.healthBool;
        item[index].resistanceBool = save.resistanceBool;
        item[index].spikeBool = save.spikeBool;
        item[index].speedBool = save.speedBool;
        item[index].vampirismBool = save.vampirismBool;
        item[index].damageInt = save.damageInt;
        item[index].armorInt = save.armorInt;
        item[index].healthInt = save.healthInt;
        item[index].resistanceInt = save.resistanceInt;
        item[index].spikeInt = save.spikeInt;
        item[index].speedInt = save.speedInt;
        item[index].vampirismInt = save.vampirismInt;
    }
}
