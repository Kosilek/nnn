
using System.Collections.Generic;
using UnityEngine;

public class DropItem : Singleton<DropItem>
{
    public List<Item> dropItem = new List<Item>();
    public List<GameObject> gameObjectDropItem = new List<GameObject>(); 

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        Event.OnDropItem.AddListener(AddDropItem);
        Event.OnInstIndexDropItem.AddListener(InstIndexDropItem);
        Event.OnRemoveDropItem.AddListener(RemoveDropItem);
    }

    public void AddDropItem(GameObject drop)
    {
        dropItem.Add(drop.GetComponent<Item>());
    }

    public void InstIndexDropItem()
    {
        for (int i = 0; i < dropItem.Count; i++)
        {
            dropItem[i].indexDropItem = i;
        }
    }

    public void RemoveDropItem(int index)
    {
        dropItem.RemoveAt(index);
    }

}
