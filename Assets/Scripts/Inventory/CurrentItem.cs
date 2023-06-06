using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CurrentItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
  //  [HideInInspector]
    public int index;
    [SerializeField]private TypeItem type;
    [SerializeField] private GameObject delete;
    public GameObject icon;
    public Text txt;
    [SerializeField] private GameObject answer;
    [SerializeField] private GameObject inventoryManager;
    private Inventory inventory;

    private bool checkId0;

    private void Start()
    {
        inventory = inventoryManager.GetComponent<Inventory>();
        delete.SetActive(false);
        inventory.panelStatistics.SetActive(false);
        inventory.DeleteArray();
        Physics2D.queriesStartInColliders = false;
    }

    public void DeleteItem()
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
            if (delete.activeInHierarchy == true)
            {
                delete.SetActive(false);
                inventory.panelStatistics.SetActive(false);
                inventory.DeleteArray();
            }
            else if (delete.activeInHierarchy == false)
            {
                delete.SetActive(true);
                inventory.panelStatistics.SetActive(true);
                inventory.panelStatiscticsActive(index);
            }
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
            ReItemIndex(oldIndex, newIndex, true);
           // Debug.Log("ReItem if");
        }
        else if (newIndex < inventory.item.Count - 9)
        {
            ReItemIndex(oldIndex, newIndex, false);
           // Debug.Log("ReItem if else if");
        } 
        else if (inventory.item.Count - 9 <= newIndex && newIndex < inventory.item.Count)
        {          
                CheckItemPlayer(oldIndex, newIndex);
           // Debug.Log("ReItem if else if else if");
        }
        ReDisplay(oldIndex, newIndex);
    }

    private void CheckItemPlayer(int oldIndex, int newIndex)
    {
        switch (newIndex)
        {
            case 36:
                CheckItemType(oldIndex, newIndex, TypeItem.weapon);
                break;
            case 37:
                CheckItemType(oldIndex, newIndex, TypeItem.helmet);
                break;
            case 38:
                CheckItemType(oldIndex, newIndex, TypeItem.bib);
                break;
            case 39:
                CheckItemType(oldIndex, newIndex, TypeItem.gloves);
                break;
            case 40:
                CheckItemType(oldIndex, newIndex, TypeItem.boots);
                break;
            case 41:
                CheckItemType(oldIndex, newIndex, TypeItem.amulet);
                break;
            case 42:
                CheckItemType(oldIndex, newIndex, TypeItem.ring);
                break;
            case 43:
                CheckItemType(oldIndex, newIndex, TypeItem.ring);
                break;
            case 44:
                CheckItemType(oldIndex, newIndex, TypeItem.bracelete);
                break;

        }
    }

    private void CheckItemType(int oldIndex, int newIndex, TypeItem type)
    {
        if (inventory.item[oldIndex].typeItem == type)
        {
            CheckTheSameId(oldIndex, newIndex);
            Debug.Log("CheckItemType if");
        }
        else
        {
            ReturnItem(oldIndex);
            Debug.Log("CheckItemType else");
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
           // Debug.Log("ReItemIndex if");
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
                            CheckTheSameId(oldIndex, newIndex);
                         //   Debug.Log("ReItemIndex if else switch true if");
                        } 
                        else if (inventory.itemPubl[newIndex].GetComponent<CurrentItem>().type != TypeItem.rest)
                        {
                            ReturnItem(oldIndex);
                        //    Debug.Log("ReItemIndex if else switch true else if");
                        }
                        
                    }
                    else if (inventory.item[newIndex].id != 0)
                    {
                        ReturnItem(oldIndex);
                     //   Debug.Log("ReItemIndex if else switch true if else if");
                    }
                    break;
                case false:
                    CheckTheSameId(oldIndex, newIndex);
                   // Debug.Log("\"ReItemIndex if else switch false");
                    break;
            }
            
        }
    }

    private void CheckTheSameId(int oldIndex, int newIndex)
    {
            if (inventory.item[oldIndex].id == inventory.item[newIndex].id)
            {
                (inventory.item[oldIndex], inventory.item[newIndex]) = (inventory.item[newIndex], inventory.item[oldIndex]);
                inventory.itemPubl[oldIndex].transform.position = inventory.positionCell[oldIndex];
                inventory.itemPubl[newIndex].transform.position = inventory.positionCell[newIndex];
      //     Debug.Log("CheckTheSameId if");
            }
            else if (inventory.item[oldIndex].id != inventory.item[newIndex].id)
            {
                (inventory.item[oldIndex], inventory.item[newIndex]) = (inventory.item[newIndex], inventory.item[oldIndex]);
                inventory.itemPubl[oldIndex].transform.position = inventory.positionCell[oldIndex];
                inventory.itemPubl[newIndex].transform.position = inventory.positionCell[newIndex];
          //  Debug.Log("CheckTheSameId if else if");
        }     
    }

    private void ReDisplay(int oldIndex, int newIndex)
    {
        inventoryManager.GetComponent<Inventory>().DisplayItem();
        if (inventoryManager.GetComponent<Inventory>().itemPubl[newIndex].GetComponent<CurrentItem>().delete.activeInHierarchy)
        {
            delete.SetActive(false);
            inventory.panelStatistics.SetActive(false);
            inventory.DeleteArray();
        }
        if (inventoryManager.GetComponent<Inventory>().itemPubl[oldIndex].GetComponent<CurrentItem>().delete.activeInHierarchy)
        {
            delete.SetActive(false);
            inventory.panelStatistics.SetActive(false);
            inventory.DeleteArray();
        }
    }
    #endregion
}
