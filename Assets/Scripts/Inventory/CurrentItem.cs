using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CurrentItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    //[HideInInspector]
    public int index;
    [SerializeField]private TypeItem type;
    [SerializeField] private GameObject delete;
    public GameObject icon;
    public Text txt;
    [SerializeField] private GameObject answer;
    [SerializeField] private GameObject inventoryManager;
    private Inventory inventory;
    private bool checkTransfer;
    public static bool sellItem = false;

    private bool checkId0;

    private void Start()
    {
        inventory = inventoryManager.GetComponent<Inventory>();
        UnActive();
        Physics2D.queriesStartInColliders = false;
    }

    private void UnActive()
    {
        delete.SetActive(false);
        inventory.panelStatistics.SetActive(false);
        inventory.DeleteArray();
    }

    public void DeleteItem()
    {
        if (!sellItem)
        {
            DeleteItemInventory();
        }

        if (sellItem)
        {
            int cost = 0;
            cost = inventory.CostSellItem(index);
            Event.SendScoreCoinsSouls(cost);
            DeleteItemInventory();
        }
    }

    private void DeleteItemInventory()
    {
        inventory.DeleteItem(index, icon, txt, answer);
        delete.SetActive(false);
        inventory.panelStatistics.SetActive(false);
        inventory.DeleteArray();
    }

    public void BackAnswer()
    {
        delete.SetActive(false);
        inventory.panelStatistics.SetActive(false);
        inventory.DeleteArray();
    }

    public void OnActiveDeleteItem()
    {
        if (inventory.item[index].id != 0)
        {
            ActiveDelete();
            ActivePanelStatistics();
        }
      
    }

    private void ActiveDelete()
    {
        if (delete.activeInHierarchy == true)
        {
            delete.SetActive(false);

            inventory.DeleteArray();
        }
        else if (delete.activeInHierarchy == false)
        {
            delete.SetActive(true);

            inventory.panelStatiscticsActive(index);
        }
    }

    private void ActivePanelStatistics()
    {
        if (inventory.panelStatistics.activeInHierarchy == true)
        {
            inventory.panelStatistics.SetActive(false);
        }
        else if (inventory.panelStatistics.activeInHierarchy == false)
        {
            inventory.panelStatistics.SetActive(true);
        }
    }

    #region TransferItem
    public void OnBeginDrag(PointerEventData eventData)
    {
        delete?.SetActive(false);
        if (inventory.item[index].id == 0)
        {
            checkId0 = true;
            
        } else if (inventory.item[index].id != 0)
        {
            checkId0 = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!checkId0)
            transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        int check = 0;
        int oldIndex = index;
        int newIndex = 0;
        if (!checkId0)
        {
            for (int i = 0; i < inventory.itemPubl.Count; i++)
            {
                if (Vector3.Distance(transform.position, inventory.itemPubl[i].transform.position) < 100f)
                {
                    if (check == 0)
                    {
                        oldIndex = i;
                        check = 1;
                    }
                    else if (check == 1)
                    {
                        newIndex = i;
                        check = 2;
                        break;
                    }
                }
            }
            if (oldIndex != index)
            {
                (oldIndex, newIndex) = (newIndex, oldIndex);
            }
            if (check == 2)
            {
                ReItem(oldIndex, newIndex);
            } else if (check == 1)
            {
                ReturnItem(oldIndex);
            }
        }
    }


    private void ReturnItem(int oldIndex)
    {
        inventory.itemPubl[oldIndex].transform.position = inventory.positionCell[oldIndex];
        inventory.DisplayItem();
    }

    private void ReItem(int oldIndex, int newIndex)
    {
        if (inventory.item.Count - 9 <= oldIndex && oldIndex < inventory.item.Count)
        {
            Debug.Log("ReItem if");
            ReItemIndex(oldIndex, newIndex, true);
        }
        else if (newIndex < inventory.item.Count - 9)
        {
            Debug.Log("ReItem if else if");
            ReItemIndex(oldIndex, newIndex, false);
        } 
        else if (inventory.item.Count - 9 <= newIndex && newIndex < inventory.item.Count)
        {
            Debug.Log("ReItem if else if else if");
            CheckItemPlayer(oldIndex, newIndex);        
        }
        ReDisplay(oldIndex, newIndex);
    }

    private void CheckItemPlayer(int oldIndex, int newIndex)
    {
        switch (newIndex)
        {
            case 36:
                CheckItemType(oldIndex, newIndex, TypeItem.weapon);
                if (checkTransfer)
                {
                    WeaponItem(newIndex, oldIndex);
                     checkTransfer = false;
                }
                break;
            case 37:
                CheckItemType(oldIndex, newIndex, TypeItem.helmet);
                if (checkTransfer)
                {
                    ArmorItem(newIndex, oldIndex);
                    checkTransfer = false;
                }
                break;
            case 38:
                CheckItemType(oldIndex, newIndex, TypeItem.bib);
                if (checkTransfer)
                {
                    ArmorItem(newIndex, oldIndex);
                    checkTransfer = false;
                }
                break;
            case 39:
                CheckItemType(oldIndex, newIndex, TypeItem.gloves);
                if (checkTransfer)
                {
                    ArmorItem(newIndex, oldIndex);
                    checkTransfer = false;
                }
                break;
            case 40:
                CheckItemType(oldIndex, newIndex, TypeItem.boots);
                if (checkTransfer)
                {
                    BootsItem(newIndex, oldIndex);
                    checkTransfer = false;
                }
                break;
            case 41:
                CheckItemType(oldIndex, newIndex, TypeItem.amulet);
                if (checkTransfer)
                {
                    DicorationItem(newIndex, oldIndex);
                    checkTransfer = false;
                }
                break;
            case 42:
                CheckItemType(oldIndex, newIndex, TypeItem.ring);
                if (checkTransfer)
                {
                    DicorationItem(newIndex, oldIndex);
                    checkTransfer = false;
                }
                break;
            case 43:
                CheckItemType(oldIndex, newIndex, TypeItem.ring);
                if (checkTransfer)
                {
                    DicorationItem(newIndex, oldIndex);
                    checkTransfer = false;
                }
                break;
            case 44:
                CheckItemType(oldIndex, newIndex, TypeItem.bracelete);
                if (checkTransfer)
                {
                    DicorationItem(newIndex, oldIndex);
                    checkTransfer = false;
                }
                break;
        }
    }
  
    private void CheckItemType(int oldIndex, int newIndex, TypeItem type)
    {
        if (inventory.item[oldIndex].typeItem == type)
        {
            checkTransfer = true;
            CheckTheSameId(oldIndex, newIndex);
        }
        else
        {
            ReturnItem(oldIndex);
        }
    }

    private void ReItemIndex(int oldIndex, int newIndex, bool itemPlayer)
    {
        if (inventory.item[oldIndex].id == inventory.item[newIndex].id && inventory.item[oldIndex].isStackable == true)
        {
            inventory.item[newIndex].countItem += inventory.item[oldIndex].countItem;
            inventory.DeleteItem(oldIndex, inventory.itemPubl[oldIndex].GetComponent<CurrentItem>().icon,
            inventory.itemPubl[oldIndex].GetComponent<CurrentItem>().txt,
            inventory.itemPubl[oldIndex].GetComponent<CurrentItem>().answer);
        }
        else 
        {
            switch(itemPlayer)
            {
                case true:
                    if (inventory.item[newIndex].id == 0)
                    {
                        if (inventory.itemPubl[newIndex].GetComponent<CurrentItem>().type == TypeItem.rest)
                        {
                            Debug.Log("ReItemIndex if else switch true if");
                            checkTransfer = true;
                            ReItemSwitch(oldIndex, newIndex);
                            CheckTheSameId(oldIndex, newIndex);
                        } 
                        else if (inventory.itemPubl[newIndex].GetComponent<CurrentItem>().type != TypeItem.rest)
                        {
                            Debug.Log("ReItemIndex if else switch true else if");
                            ReturnItem(oldIndex);
                        }
                        
                    }
                    else if (inventory.item[newIndex].id != 0)
                    {
                        Debug.Log("ReItemIndex if else switch true if else if");
                        ReturnItem(oldIndex);
                    }
                    break;
                case false:
                    Debug.Log("\"ReItemIndex if else switch false");
                    CheckTheSameId(oldIndex, newIndex);
                    break;
            }       
        }
    }

    private void ReItemSwitch(int oldIndex, int newIndex)
    {
        switch (oldIndex)
        {
            case 36:
           
                if (checkTransfer)
                {
                    WeaponItem(newIndex, oldIndex);
                    checkTransfer = false;
                }
                break;
            case 37:
          
                if (checkTransfer)
                {
                    ArmorItem(newIndex, oldIndex);
                    checkTransfer = false;
                }
                break;
            case 38:
            
                if (checkTransfer)
                {
                    ArmorItem(newIndex, oldIndex);
                    checkTransfer = false;
                }
                break;
            case 39:
         
                if (checkTransfer)
                {
                    ArmorItem(newIndex, oldIndex);
                    checkTransfer = false;
                }
                break;
            case 40:
         
                if (checkTransfer)
                {
                    BootsItem(newIndex, oldIndex);
                    checkTransfer = false;
                }
                break;
            case 41:
          
                if (checkTransfer)
                {
                    DicorationItem(newIndex, oldIndex);
                    checkTransfer = false;
                }
                break;
            case 42:

                if (checkTransfer)
                {
                    DicorationItem(newIndex, oldIndex);
                    checkTransfer = false;
                }
                break;
            case 43:
        
                if (checkTransfer)
                {
                    DicorationItem(newIndex, oldIndex);
                    checkTransfer = false;
                }
                break;
            case 44:
      
                if (checkTransfer)
                {
                    DicorationItem(newIndex, oldIndex);
                    checkTransfer = false;
                }
                break;
        }
    }

    #region EventItem
    private void WeaponItem(int newIndex, int oldIndex)
    {
        Event.SendReDamage(inventory.item[newIndex].damage, inventory.item[oldIndex].damage);
        Event.SendReVampirism(inventory.item[newIndex].vampirism, inventory.item[oldIndex].vampirism);
    }

    private void ArmorItem(int newIndex, int oldIndex)
    {
        Event.SendReArmor(inventory.item[newIndex].armor, inventory.item[oldIndex].armor);
        Event.SendReHealth(inventory.item[newIndex].health, inventory.item[oldIndex].health);
        Event.SendReSpike(inventory.item[newIndex].spike, inventory.item[oldIndex].spike);
    }

    private void BootsItem(int newIndex, int oldIndex)
    {
        Event.SendReArmor(inventory.item[newIndex].armor, inventory.item[oldIndex].armor);
        Event.SendReHealth(inventory.item[newIndex].health, inventory.item[oldIndex].health);
        Event.SendReSpike(inventory.item[newIndex].spike, inventory.item[oldIndex].spike);
        Event.SendReSpeed(inventory.item[newIndex].speed, inventory.item[oldIndex].speed);
    }

    private void DicorationItem(int newIndex, int oldIndex)
    {
        Event.SendReHealth(inventory.item[newIndex].health, inventory.item[oldIndex].health);
        Event.SendReResistiance(inventory.item[newIndex].resistance, inventory.item[oldIndex].resistance);
    }
    #endregion
   
    private void CheckTheSameId(int oldIndex, int newIndex)
    {
        (inventory.item[oldIndex], inventory.item[newIndex]) = (inventory.item[newIndex], inventory.item[oldIndex]);
        inventory.itemPubl[oldIndex].transform.position = inventory.positionCell[oldIndex];
        inventory.itemPubl[newIndex].transform.position = inventory.positionCell[newIndex];
    }

    private void ReDisplay(int oldIndex, int newIndex)
    {
        inventoryManager.GetComponent<Inventory>().DisplayItem();
        if (inventoryManager.GetComponent<Inventory>().itemPubl[newIndex].GetComponent<CurrentItem>().delete.activeInHierarchy)
        {
            UnActiveDisp();
        }
        if (inventoryManager.GetComponent<Inventory>().itemPubl[oldIndex].GetComponent<CurrentItem>().delete.activeInHierarchy)
        {
            UnActiveDisp();
        }
    }

    private void UnActiveDisp()
    {
        delete.SetActive(false);
        inventory.panelStatistics.SetActive(false);
        inventory.DeleteArray();
    }
    #endregion
}
