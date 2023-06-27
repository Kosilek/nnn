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

    public void Txt(int i)
    {
        Debug.Log($"{i} = {itemMagazinSellStat[i].damage}");
    }
}
