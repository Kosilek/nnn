using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagazinCntr : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemMagazin;
    [SerializeField] private List<GameObject> itemMagazinIcon;
    private List<int> indexItemMagazin = new List<int>();
    [SerializeField] private List<GameObject> cellMagazin;
    [SerializeField]private List<Item> itemMagazinSell = new List<Item>();
    [SerializeField] private List<Item> itemMagazinSellStat = new List<Item>();
    GeneralActive ga = new GeneralActive();
    /*
     * Then remake it under an adequate option where there will be only one type of text variable
     */
    #region StatShopItem    
    [Header("StatItemSword")]
    [SerializeField] private GameObject StatShopItemSword;
    [SerializeField] private Text lvlItemSw;
    [SerializeField] private Text nameItemSw;
    [SerializeField] private Text damagaItem;
    [SerializeField] private Text vampirismItem;
    [SerializeField] private Text descriptionItem;
    [Header("StatItemArmor")]
    [SerializeField] private GameObject StatShopItemArmor;
    [SerializeField] private Text lvlItemArm;
    [SerializeField] private Text nameItemArm;
    [SerializeField] private Text armorItemArm;
    [SerializeField] private Text healthItemArm;
    [SerializeField] private Text spikeItemArm;
    [SerializeField] private Text descriptionItemArm;
    [Header("StatItemBoots")]
    [SerializeField] private GameObject StatShopItemBoots;
    [SerializeField] private Text lvlItemBoo;
    [SerializeField] private Text nameItemBoo;
    [SerializeField] private Text armorItemBoo;
    [SerializeField] private Text healthItemBoo;
    [SerializeField] private Text spikeItemBoo;
    [SerializeField] private Text speedItem;
    [SerializeField] private Text descriptionItemBoo;
    [Header("StatItemDecoration")]
    [SerializeField] private GameObject StatShopItemDecoration;
    [SerializeField] private Text lvlItemDec;
    [SerializeField] private Text nameItemDec;
    [SerializeField] private Text healthItemDec;
    [SerializeField] private Text resistanceItemDec;
    [SerializeField] private Text descriptionItemDec;
    #endregion
    private int index;
    Inventory inventory = new Inventory();
    private void Start()
    {
        for (int i = 0; i < itemMagazin.Count; i++)
        {
            indexItemMagazin.Add(i);
        }

        for (int i = 0; i < cellMagazin.Count;i++)
        {
            itemMagazinSell.Add(new Item());
        }

        for (int i = 0; i < cellMagazin.Count; i++)
        {
            itemMagazinSellStat.Add(new Item());
        }

        for (int i = 0;i < cellMagazin.Count;i++)
        {
            Item q = new Item();
            int j = ga.Rand(0, itemMagazin.Count);
           // Debug.Log($"j = {j}");
            cellMagazin[i].GetComponent<Image>().sprite = itemMagazinIcon[j].GetComponent<Image>().sprite;
            itemMagazinSell[i] = itemMagazin[j].GetComponent<Item>();
            //itemMagazinSell[i].RandItem();
            q.RandItem(itemMagazinSell[i].typeItem);
            itemMagazinSellStat[i] = q;
        }
    }

    public void StatShopItem(int i)
    {
        if (StatShopItemSword.activeInHierarchy == true && itemMagazinSell[i] != null)
        {
            StatShopItemSword.SetActive(false);
            index = -1;
        }
        else if (StatShopItemSword.activeInHierarchy == false && itemMagazinSell[i] != null)
        {
            StatShopItemSword.SetActive(true);
            index = i;
        }
        if (itemMagazinSell[i] != null)
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
    }
    #region StatShopItemAction
    private void ItemWeapon(int i)
    {
        lvlItemSw.text = "lvl: " + itemMagazinSellStat[i].levelItem.ToString();
        nameItemSw.text = itemMagazinSell[i].name;
        damagaItem.text = "Damage: " + itemMagazinSellStat[i].damage.ToString();
        vampirismItem.text = "Vampirism: " + itemMagazinSellStat[i].vampirism.ToString();
        descriptionItem.text = itemMagazinSell[i].description.ToString();
    }

    private void ItemArmor(int i)
    {
        lvlItemSw.text = "lvl: " +  itemMagazinSellStat[i].levelItem.ToString();
        nameItemArm.text = itemMagazinSellStat[i].name;
        armorItemArm.text = "Armor: " + itemMagazinSellStat[i].armor.ToString();
        healthItemArm.text = "Health: " + itemMagazinSellStat[i].health.ToString();
        spikeItemArm.text = "Spike: " + itemMagazinSellStat[i].spike.ToString();
        descriptionItemArm.text = itemMagazinSellStat[i].description.ToString();
    }

    private void ItemBoots(int i)
    {
        lvlItemSw.text = "lvl: " + itemMagazinSellStat[i].levelItem.ToString();
        nameItemBoo.text = itemMagazinSellStat[i].name;
        armorItemBoo.text = "Armor: " + itemMagazinSellStat[i].armor.ToString();
        healthItemBoo.text = "Health: " + itemMagazinSellStat[i].health.ToString();
        spikeItemBoo.text = "Spike: " + itemMagazinSellStat[i].spike.ToString();
        speedItem.text = "Speed: " + itemMagazinSellStat[i].speed.ToString();
        descriptionItemBoo.text = itemMagazinSellStat[i].description.ToString();
    }

    private void ItemDecotation(int i)
    {
        lvlItemSw.text = "lvl: " + itemMagazinSellStat[i].levelItem.ToString();
        nameItemDec.text = itemMagazinSellStat[i].name;
        resistanceItemDec.text = "Resistance: " + itemMagazinSellStat[i].resistance.ToString();
        healthItemDec.text = "Health: " + itemMagazinSellStat[i].health.ToString() ;
        descriptionItemDec.text = itemMagazinSellStat[i].description.ToString();
    }
    #endregion

    public void BuyItem()
    {
        Debug.Log($"buyItem index = {index}");
        if (index >= 0)
        {
            cellMagazin[index].GetComponent<Image>().sprite = null;
            itemMagazinSell[index] = null;
            itemMagazinSellStat[index] = null;
            StatShopItemSword.SetActive(false);
        }
    }
}
