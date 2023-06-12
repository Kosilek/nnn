using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;

public class SaveLoadManager : Singleton<SaveLoadManager>
{
    #region Save
   // public static List<Item> itemSave = new List<Item>();
    #endregion
    #region Manager
    [SerializeField]private Inventory inventory;
    [SerializeField] private DropItem dropItem;
    #endregion

    private string filePath;

    protected override void Awake()
    {
        base.Awake();
        filePath = Application.persistentDataPath + "/save.gameSave";
    }

    private void Start()
    {
        inventory = GetComponent<Inventory>();
        dropItem = GetComponent<DropItem>();
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath, FileMode.Create);
        Save save = new Save();

        save.SaveItems(inventory.item, save.itemsData);
        save.SaveItems(dropItem.dropItem, save.dropItemData);
        Debug.Log(save.dropItemData.Count);
        bf.Serialize(fs, save);
        fs.Close();
    }

    public void LoadGame()
    {
        if (!File.Exists(filePath))
        {
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath, FileMode.Open);
        Save save = (Save)bf.Deserialize(fs);
        fs.Close();

        LoadItem(save, inventory);
        if (dropItem.dropItem.Count != save.dropItemData.Count)
            Debug.Log($"real = {dropItem.dropItem.Count}");
        Debug.Log($"save = {save.dropItemData.Count}");
            save.SaveItems(dropItem.dropItem, save.dropItemData);//мб переделать под восоздание предмета
        LoadDropItem(save, dropItem);
    }

    private void LoadItem(Save save, Inventory inventory)
    {
        int j = 0;
        foreach (var item in save.itemsData)
        {
            inventory.LoadData(item, j);
            j++;
        }
    }
    
    private void LoadDropItem(Save save, DropItem dropItem)
    {
        int j = 0;
        foreach (var item in save.dropItemData)
        {
            dropItem.LoadData(item, j);
            j++;
        }
    }
}

[System.Serializable] public class Save
{
    [System.Serializable]
    public struct Vec3
    {
        public float x, y, z;

        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    #region InventoryData
    [System.Serializable] public struct itemSaveData
    {
        public TypeItem typeItem;
        public int levelItem;
        public string nameItem;
        public int id;
        public int countItem;
        public bool isStackable;
        public string pathIcon;
        public string pathPrefab;
        public bool customizable;
        public string description;
        public float damage;
        public float armor;
        public float health;
        public float resistance;
        public float spike;
        public float speed;
        public float vampirism;
        public TypeRest typeRest;
        //
        public bool damageBool;
        public bool armorBool;
        public bool healthBool;
        public bool resistanceBool;
        public bool spikeBool;
        public bool speedBool;
        public bool vampirismBool;
        public int damageInt;
        public int armorInt;
        public int healthInt;
        public int resistanceInt;
        public int spikeInt;
        public int speedInt;
        public int vampirismInt;
        public bool dropItemBool;
        public float x;
        public float y;
        public itemSaveData(TypeItem typeItem, int levelItem, string nameItem, int id, int countItem, bool isStackable, string pathIcon, string pathPrefab,
            bool customizable, string description,
            float damage, float armor, float health, float resistance, float spike, float speed, float vampirism, 
            TypeRest typeRest,
            bool damageBool, bool armorBool, bool healthBool, bool resistanceBool, bool spikeBool, bool speedBool, bool vampirismBool,
            int damageInt, int armorInt, int healthInt, int resistanceInt, int spikeInt, int speedInt, int vampirismInt,
            bool dropItemBool, float x, float y)
        {
            this.typeItem = typeItem;
            this.levelItem = levelItem;
            this.nameItem = nameItem;
            this.id = id;
            this.countItem = countItem;
            this.isStackable = isStackable;
            this.pathIcon = pathIcon;
            this.pathPrefab = pathPrefab;
            this.customizable = customizable;
            this.description = description;
            this.damage = damage;
            this.armor = armor;
            this.health = health;
            this.resistance = resistance;
            this.spike = spike;
            this.speed = speed;
            this.vampirism = vampirism;
            this.typeRest = typeRest;
            this.damageBool = damageBool;
            this.armorBool = armorBool;
            this.healthBool = healthBool;
            this.resistanceBool = resistanceBool;
            this.spikeBool = spikeBool;
            this.speedBool = speedBool;
            this.vampirismBool = vampirismBool;
            this.damageInt = damageInt;
            this.armorInt = armorInt;
            this.healthInt = healthInt;
            this.resistanceInt = resistanceInt;
            this.spikeInt = spikeInt;
            this.speedInt = speedInt;
            this.vampirismInt = vampirismInt;
            this.dropItemBool = dropItemBool;
            this.x = x;
            this.y = y;
        }
    }

    public List<itemSaveData> itemsData = new List<itemSaveData>();
    public List<itemSaveData> dropItemData = new List<itemSaveData>();

    public void SaveItems(List<Item> item, List<itemSaveData> itemData)
    {
        itemData.Clear();
        foreach (var go in item)
        {
            var it = go;
            it.typeItem = go.typeItem;
            it.levelItem = go.levelItem;
            it.nameItem = go.nameItem;
            it.id = go.id;
            it.countItem = go.countItem;
            it.isStackable = go.isStackable;
            it.pathIcon = go.pathIcon;
            it.pathPrefab = go.pathPrefab;
            it.customizable = go.customizable;
            it.description = go.description;
            it.damage = go.damage;
            it.armor = go.armor;
            it.health = go.health;
            it.resistance = go.resistance;
            it.spike = go.spike;
            it.speed = go.speed;
            it.vampirism = go.vampirism;
            it.typeRest = go.typeRest;
            it.damageBool = go.damageBool;
            it.armorBool = go.armorBool;
            it.healthBool = go.healthBool;
            it.resistanceBool = go.resistanceBool;
            it.spikeBool = go.spikeBool;
            it.speedBool = go.speedBool;
            it.vampirismBool = go.vampirismBool;
            it.damageInt = go.damageInt;
            it.armorInt = go.armorInt;
            it.healthInt = go.healthInt;
            it.resistanceInt = go.resistanceInt;
            it.spikeInt = go.spikeInt;
            it.speedInt = go.speedInt;
            it.vampirismInt = go.vampirismInt;
            it.dropItemBool = go.dropItemBool;
            it.x = go.x;
            it.y = go.y;
            
            

            itemData.Add(new itemSaveData(it.typeItem, it.levelItem ,it.nameItem, it.id, it.countItem, it.isStackable, it.pathIcon, it.pathPrefab, it.customizable, it.description, it.damage, it.armor,
                it.health, it.resistance, it.spike, it.speed, it.vampirism, it.typeRest, it.damageBool, it.armorBool, it.healthBool, it.resistanceBool, it.spikeBool, it.speedBool, it.vampirismBool,
                it.damageInt, it.armorInt, it.healthInt, it.resistanceInt, it.spikeInt, it.speedInt, it.vampirismInt, it.dropItemBool, it.x, it.y));
        }
    }
    #endregion
}
