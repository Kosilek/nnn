using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

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

    #region ExitInventory
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject shopPanel;
    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        InstValues();
        CreateList();
        AddEvent();
        UnActiv();
    }
    private void InstValues()
    {
        checkStatistics = new int[7];
        item = new List<Item>();
        positionCell = new List<Vector3>();
    }

    private void CreateList()
    {
        for (int i = 0; i < itemPubl.Count; i++)
        {
            itemPubl[i].GetComponent<CurrentItem>().index = i;
            positionCell.Add(itemPubl[i].transform.position);
            item.Add(new Item());
        }
    }

    private void UnActiv()
    {
        inventoryPanel.SetActive(false);
        for (int i = 0; i < nameStatistics.Length; i++)
        {
            nameStatistics[i].SetActive(false);
            valueStatistics[i].SetActive(false);
        }
    }

    private void AddEvent()
    {
        Event.OnAddStackableItem.AddListener(AddStackableItem);
        Event.OnAddUnStackableItem.AddListener(AddUnStackableItem);
    }

    #region panelStatstics
    public void panelStatiscticsActive(int index)
    {
        int value;
        string nameStatisticsA = "";
        float valueStatisticsA = 0f;
        value = checkActiveStatistics(index);
        TextMainItem(index);
        PanelStatisticsFilling(value, nameStatisticsA, valueStatisticsA, index);
    }

    private void PanelStatisticsFilling(int value, string nameStatisticsA, float valueStatisticsA, int index)
    {
        for (int i = 0; i < value; i++)
        {
            ActiveUI(i);
            TextFilling(nameStatisticsA, valueStatisticsA, index, i);
        }
    }

    private void ActiveUI(int i)
    {
        nameStatistics[i].SetActive(true);
        valueStatistics[i].SetActive(true);
    }

    private void TextFilling(string nameStatisticsA, float valueStatisticsA,  int index, int i)
    {
        for (int j = 0; j < checkStatistics.Length; j++)
        {
            if (checkStatistics[j] != 0)
            {
                (nameStatisticsA, valueStatisticsA) = AttributeDefinition(j, index);
                txtNameStatistics[i].text = nameStatisticsA.ToString();
                txtValueStatistics[i].text = valueStatisticsA.ToString();
                checkStatistics[j] = 0;
                break;
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
        ZeroingCheck();
        UnActiveUI();
    }

    private void ZeroingCheck()
    {
        for (int i = 0; i < checkStatistics.Length; i++)
        {
            if (checkStatistics[i] != 0)
            {
                checkStatistics[i] = 0;
            }
        }
    }

    private void UnActiveUI()
    {
        for (int i = 0; i < nameStatistics.Length; i++)
        {
            nameStatistics[i].SetActive(false);
            valueStatistics[i].SetActive(false);
        }
    }
    #endregion

    #region BuyItem
    public void BuyItem(Item items)
    {
        if (items.isStackable)
        {
            AddBuyStackableItem(items);
        }
        else if (!items.isStackable)
        {
            AddBuyUnStackableItem(items);
        }
    }

    private void AddBuyStackableItem(Item items)
    {
        for (int i = 0; i < item.Count - 9; i++)
        {
            if (item[i].id == items.id)
            {
                item[i].countItem += items.countItem;
                return;
            }
        }
        AddBuyUnStackableItem(items);
    }

    private void AddBuyUnStackableItem(Item items)
    {
        for (int i = 0; i < item.Count - 9; i++)
        {
            if (item[i].id == 0)
            {
                item[i] = items;
                Debug.Log(item[i].id);
                break;
            }
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
                if (item[i].dropItemBool)
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
                if (item[i].dropItemBool)
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

    public int CostSellItem(int index)
    {
        return item[index].cost;
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

    public void ExitInventory()
    {
        if (!CurrentItem.sellItem)
        {
            inventoryPanel.SetActive(false);
        }
        else if (CurrentItem.sellItem)
        {
            CurrentItem.sellItem = false;
            inventoryPanel.SetActive(false);
            shopPanel.SetActive(true);

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
