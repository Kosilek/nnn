using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string nameItem;
    public int id;
    [HideInInspector] public int countItem;
    public bool isStackable;
    public string pathIcon;
    public string pathPrefab;

    [SerializeField] private int saveCount;

    private void Start()
    {
        countItem = saveCount;
    }

    //PickupTrigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            Inventory.PickupTrigger(gameObject, isStackable, id, countItem);
        }
    }
}
