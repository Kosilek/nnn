using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CurrentItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
  //  [HideInInspector]
    public int index;
    [SerializeField] private GameObject delete;
    [SerializeField] private GameObject icon;
    [SerializeField] private Text txt;
    [SerializeField] private GameObject answer;
    [SerializeField] private GameObject inventoryManager;
    private Transform saveOldTransform;
   // private Transform saveNewTransform;
    private bool checkId0;

    private void Start()
    {
        
        delete.SetActive(false);
        Physics2D.queriesStartInColliders = false;
    }

    public void DeleteItem()
    {
        Inventory.DeleteItem(index, icon, txt, answer);
        delete.SetActive(false);
    }

    public void BackAnswer()
    {
        delete.SetActive(false);
    }


    public void OnActiveDeleteItem()
    {
        if (Inventory.item[index].id != 0)
        {
            if (delete.activeInHierarchy == true)
            {
                delete.SetActive(false);
            }
            else if (delete.activeInHierarchy == false)
            {
                delete.SetActive(true);
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        saveOldTransform = inventoryManager.GetComponent<Inventory>().itemPubl[index].transform;
        delete?.SetActive(false);
        if (Inventory.item[index].id == 0)
        {
            checkId0 = true;
            
        } else if (Inventory.item[index].id != 0)
        {
            checkId0 = false;
           // saveOldTransform = transform;
          //  Debug.Log(saveOldTransform.position);
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
            for (int i = 0; i < inventoryManager.GetComponent<Inventory>().itemPubl.Count; i++)
            {
                if (Vector3.Distance(transform.position, inventoryManager.GetComponent<Inventory>().itemPubl[i].transform.position) < 100f)
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
         //   Debug.Log($"old = {oldIndex}, new = {newIndex}");
           // Debug.Log($"check = {check}");
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
        //   Debug.Log($"oldtra {saveOldTransform.position}, newtra {inventoryManager.GetComponent<Inventory>().itemPubl[oldIndex].transform.position} tra {transform.position} traCell {inventoryManager.GetComponent<Inventory>().positionCell[index]}");
        inventoryManager.GetComponent<Inventory>().itemPubl[oldIndex].transform.position = inventoryManager.GetComponent<Inventory>().positionCell[oldIndex];
        disp();
    }

    private void ReItem(int oldIndex, int newIndex)
    {
        /*var saveItem = Inventory.item[oldIndex];
        Inventory.item[oldIndex] = Inventory.item[newIndex];
        Inventory.item[newIndex] = saveItem;*/
        if (Inventory.item[oldIndex].id == Inventory.item[newIndex].id && Inventory.item[oldIndex].isStackable == true)
        {
            Inventory.item[newIndex].countItem += Inventory.item[oldIndex].countItem;
            Inventory.DeleteItem(oldIndex, inventoryManager.GetComponent<Inventory>().itemPubl[oldIndex].GetComponent<CurrentItem>().icon,
                inventoryManager.GetComponent<Inventory>().itemPubl[oldIndex].GetComponent<CurrentItem>().txt,
                inventoryManager.GetComponent<Inventory>().itemPubl[oldIndex].GetComponent<CurrentItem>().answer);
            Debug.Log("1");
        } else
            if (Inventory.item[oldIndex].id == Inventory.item[newIndex].id)
        {
           // inventoryManager.GetComponent<Inventory>().itemPubl[oldIndex].transform.position = saveOldTransform.position;
         //   inventoryManager.GetComponent<Inventory>().itemPubl[newIndex].transform.position = saveNewTransform.position;
            Debug.Log("2");
        }
        else if (Inventory.item[oldIndex].id != Inventory.item[newIndex].id)
        {
            (Inventory.item[oldIndex], Inventory.item[newIndex]) = (Inventory.item[newIndex], Inventory.item[oldIndex]);
            Debug.Log("3");
        }


        disp();
      //  if (inventoryManager.GetComponent<CurrentItem>().delete.activeInHierarchy == true)
      if (inventoryManager.GetComponent<Inventory>().itemPubl[newIndex].GetComponent<CurrentItem>().delete.activeInHierarchy)
        {
            delete.SetActive(false);
        }
    }

    private void disp()
    {
        inventoryManager.GetComponent<Inventory>().DisplayItem();
        Debug.Log("Display");
    }
}
