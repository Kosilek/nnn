using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagazinCntr : MonoBehaviour
{
    public int levelLocation;

    [SerializeField] private List<GameObject> itemMagazin;
    [SerializeField] private List<GameObject> itemMagazinIcon;
    private List<int> indexItemMagazin = new List<int>();
    [SerializeField] private List<GameObject> cellMagazin;
    [SerializeField]private List<Item> itemMagazinSell = new List<Item>();
    [SerializeField] private List<Item> itemMagazinSellStat = new List<Item>();
    GeneralActive ga = new GeneralActive();
    [SerializeField] private GameObject panelShop;
    #region StatShopItem    
    [Header("StatShopItem")]
    [SerializeField] private GameObject statShopText;
    [SerializeField] private Text nameItemText;
    [SerializeField] private Text levelItemText;
    [SerializeField] private Text statText1Text;
    [SerializeField] private Text statText2Text;
    [SerializeField] private Text statText3Text;
    [SerializeField] private Text statText4Text;
    [SerializeField] private Text descriptionStatText;
    [SerializeField] private Text costItem;
    #endregion
    private int index;
    public Inventory inventory = new Inventory();
    public Money money = new Money();
    private void Start()
    {
        InstValues();
       // CreateListIndexItem();
       // CreateListItemStat();
        UnActivUI();
        TextNull();
        AddEvent();
    }

    private void AddEvent()
    {
        Event.OnLevelLoction.AddListener(TakeLevelLocation);
        Event.OnCreateListIndexItem.AddListener(CreateListIndexItem);
        Event.OnCreateListItemStat.AddListener(CreateListItemStat);
    }

    private void UnActivUI()
    {
        panelShop.SetActive(false);
        statShopText.SetActive(false);
    }

    private void CreateListItemStat()
    {
        for (int i = 0; i < cellMagazin.Count; i++)
        {
            Item q = new Item();
            int j = ga.Rand(0, itemMagazin.Count);
            cellMagazin[i].GetComponent<Image>().sprite = itemMagazinIcon[j].GetComponent<Image>().sprite;
            itemMagazinSell.Add(itemMagazin[j].GetComponent<Item>());
            if (itemMagazinSell[i].isStackable)
            q.isStackable = true;
            q.sellItem = true;
            q.levelLocation = levelLocation;
            q.RandItem(itemMagazinSell[i].typeItem);
            itemMagazinSellStat.Add(q);
        }
    }

    private void CreateListIndexItem()
    {
        for (int i = 0; i < itemMagazin.Count; i++)
        {
            indexItemMagazin.Add(i);
        }
    }

    private void InstValues()
    {
        inventory = GetComponent<Inventory>();
        money = GetComponent<Money>();
    }

    private void TextNull()
    {
        statText1Text.text = null;
        statText2Text.text = null;
        statText3Text.text = null;
        statText4Text.text = null;
    }

    #region StatShopItemAction
    public void StatShopItem(int i)
    {
        if (itemMagazinSell[i].typeItem != TypeItem.rest)
        {
            ActiveStatShop(i);
            if (itemMagazinSell[i] != null)
            {
                SetTextMain(i);
                SwitchItemType(i);
            }
        }
    }

    private void SetTextMain(int i)
    {
        nameItemText.text = itemMagazinSell[i].name;
        levelItemText.text = "lvl: " + itemMagazinSellStat[i].levelItem;
        descriptionStatText.text = itemMagazinSell[i].description.ToString();
        costItem.text = itemMagazinSellStat[i].cost.ToString();
    }

    private void ActiveStatShop(int i)
    {
        if (statShopText.activeInHierarchy == true && itemMagazinSell[i] != null)
        {
            statShopText.SetActive(false);
            index = -1;
        }
        else if (statShopText.activeInHierarchy == false && itemMagazinSell[i] != null)
        {
            statShopText.SetActive(true);
            TextNull();
            index = i;
        }
    }

    private void SwitchItemType(int i)
    {
        switch (itemMagazinSellStat[i].typeItem)
        {
            case TypeItem.weapon:
                ItemWeapon(i);
                break;
            case TypeItem.helmet:
                ItemArmor(i);
                break;
            case TypeItem.bib:
                ItemArmor(i);
                break;
            case TypeItem.gloves:
                ItemArmor(i);
                break;
            case TypeItem.boots:
                ItemBoots(i);
                break;
            case TypeItem.amulet:
                ItemDecotation(i);
                break;
            case TypeItem.ring:
                ItemDecotation(i);
                break;
            case TypeItem.bracelete:
                ItemDecotation(i);
                break;
        }
    }

    private void ItemWeapon(int i)
    {
       
        statText1Text.text = "Damage: " + itemMagazinSellStat[i].damage.ToString();
        statText2Text.text = "Vampirism: " + itemMagazinSellStat[i].vampirism.ToString();
        statText3Text.text = null;
        statText4Text.text = null;
    }

    private void ItemArmor(int i)
    {
     
        statText1Text.text = "Armor: " + itemMagazinSellStat[i].armor.ToString();
        statText2Text.text = "Health: " + itemMagazinSellStat[i].health.ToString();
        statText3Text.text = "Spike: " + itemMagazinSellStat[i].spike.ToString();
        statText4Text.text = null;
    }

    private void ItemBoots(int i)
    {
      
        statText1Text.text = "Armor: " + itemMagazinSellStat[i].armor.ToString();
        statText2Text.text = "Health: " + itemMagazinSellStat[i].health.ToString();
        statText3Text.text = "Spike: " + itemMagazinSellStat[i].spike.ToString();
        statText4Text.text = "Speed: " + itemMagazinSellStat[i].speed.ToString();
    }

    private void ItemDecotation(int i)
    {
        
        statText1Text.text = "Resistance: " + itemMagazinSellStat[i].resistance.ToString();
        statText2Text.text = "Health: " + itemMagazinSellStat[i].health.ToString() ;
        statText3Text.text = null;
        statText4Text.text = null;
    }
    #endregion

    public void BuyItem()
    {
        if (index >= 0)
        {
            Item n = new Item();
            n = CopyItem(n);
            if (money.countSouls >= n.cost)
            {
                inventory.BuyItem(n);
                Event.SendTakeCoinsSouls(n.cost);
                cellMagazin[index].GetComponent<Image>().sprite = null;
                itemMagazinSell[index] = null;
                itemMagazinSellStat[index] = null;
                statShopText.SetActive(false);
                n.cost = n.coefCost * n.levelItem - n.coefCostSell * n.levelItem;
            }
            else return;
        }
    }

    private Item CopyItem(Item items)
    {
        items.cost = itemMagazinSellStat[index].cost;
        items.levelItem = itemMagazinSellStat[index].levelItem;
        items.id = itemMagazinSell[index].id;
        items.nameItem = itemMagazinSell[index].nameItem;
        items.pathIcon = itemMagazinSell[index].pathIcon;
        items.description = itemMagazinSell[index].description;
        items.countItem = itemMagazinSellStat[index].countItem;
        items.damage = itemMagazinSellStat[index].damage;
        items.damageInt = itemMagazinSellStat[index].damageInt;
        items.damageBool = itemMagazinSellStat[index].damageBool;
        items.armor = itemMagazinSellStat[index].armor;
        items.armorInt = itemMagazinSellStat[index].armorInt;
        items.armorBool = itemMagazinSellStat[index].armorBool;
        items.health = itemMagazinSellStat[index].health;
        items.healthInt = itemMagazinSellStat[index].healthInt;
        items.healthBool = itemMagazinSellStat[index].healthBool;
        items.resistance = itemMagazinSellStat[index].resistance;
        items.resistanceInt = itemMagazinSellStat[index].resistanceInt;
        items.resistanceBool = itemMagazinSellStat[index].resistanceBool;
        items.spike = itemMagazinSellStat[index].spike;
        items.spikeInt = itemMagazinSellStat[index].spikeInt;
        items.spikeBool = itemMagazinSellStat[index].spikeBool;
        items.speed = itemMagazinSellStat[index].speed;
        items.speedInt = itemMagazinSellStat[index].speedInt;
        items.speedBool = itemMagazinSellStat[index].speedBool;
        items.vampirism = itemMagazinSellStat[index].vampirism;
        items.vampirismInt = itemMagazinSellStat[index].vampirismInt;
        items.vampirismBool = itemMagazinSellStat[index].vampirismBool;
        return items;
    }

    public void ButtonSell()
    {
        statShopText.SetActive(false);
        inventory.DisplayItem();
        CurrentItem.sellItem = true;
    }

    public void TakeLevelLocation(int levelLocation)
    {
        this.levelLocation = levelLocation;
    }
}
