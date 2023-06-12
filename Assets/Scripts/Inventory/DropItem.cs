
using System.Collections.Generic;
using UnityEngine;

public class DropItem : Singleton<DropItem>
{
    public List<Item> dropItem = new List<Item>();

    protected override void Awake()
    {
        base.Awake();
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

    public void LoadData(Save.itemSaveData save, int index)
    {
        dropItem[index].typeItem = save.typeItem;
        dropItem[index].levelItem = save.levelItem;
        dropItem[index].nameItem = save.nameItem;
        dropItem[index].id = save.id;
        dropItem[index].countItem = save.countItem;
        dropItem[index].isStackable = save.isStackable;
        dropItem[index].pathIcon = save.pathIcon;
        dropItem[index].pathPrefab = save.pathPrefab;
        dropItem[index].customizable = save.customizable;
        dropItem[index].description = save.description;
        dropItem[index].damage = save.damage;
        dropItem[index].armor = save.armor;
        dropItem[index].health = save.health;
        dropItem[index].resistance = save.resistance;
        dropItem[index].spike = save.spike;
        dropItem[index].speed = save.speed;
        dropItem[index].vampirism = save.vampirism;
        dropItem[index].typeRest = save.typeRest;
        dropItem[index].damageBool = save.damageBool;
        dropItem[index].armorBool = save.armorBool;
        dropItem[index].healthBool = save.healthBool;
        dropItem[index].resistanceBool = save.resistanceBool;
        dropItem[index].spikeBool = save.spikeBool;
        dropItem[index].speedBool = save.speedBool;
        dropItem[index].vampirismBool = save.vampirismBool;
        dropItem[index].damageInt = save.damageInt;
        dropItem[index].armorInt = save.armorInt;
        dropItem[index].healthInt = save.healthInt;
        dropItem[index].resistanceInt = save.resistanceInt;
        dropItem[index].spikeInt = save.spikeInt;
        dropItem[index].speedInt = save.speedInt;
        dropItem[index].vampirismInt = save.vampirismInt;
        dropItem[index].dropItemBool = save.dropItemBool;
        dropItem[index].transform.position = new Vector3(save.x, save.y, 0);
    }
}
