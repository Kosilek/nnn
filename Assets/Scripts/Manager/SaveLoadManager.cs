using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class SaveLoadManager : Singleton<SaveLoadManager>
{
    #region Save
    public static List<Item> itemSave = new List<Item>();
    #endregion
    #region Manager
    [SerializeField]private Inventory inventory;
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
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath, FileMode.Create);
        Save save = new Save();

        save.SaveItems(inventory.item);

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
}

[System.Serializable] public class Save
{
    #region InventoryData
    [System.Serializable] public struct itemSaveData
    {
        public TypeItem typeItem;
        public string nameItem;
        public int id;
        public int countItem;
        public bool isStackable;
        public string pathIcon;
        public string pathPrefab;
        public itemSaveData(TypeItem typeItem, string nameItem, int id, int countItem, bool isStackable, string pathIcon, string pathPrefab)
        {
            this.typeItem = typeItem;
            this.nameItem = nameItem;
            this.id = id;
            this.countItem = countItem;
            this.isStackable = isStackable;
            this.pathIcon = pathIcon;
            this.pathPrefab = pathPrefab;
        }
    }

    public List<itemSaveData> itemsData = new List<itemSaveData>();

    public void SaveItems(List<Item> item)
    {
        foreach (var go in item)
        {
            var it = go;

            it.typeItem = go.typeItem;
            it.nameItem = go.nameItem;
            it.id = go.id;
            it.countItem = go.countItem;
            it.isStackable = go.isStackable;
            it.pathIcon = go.pathIcon;
            it.pathPrefab = go.pathPrefab;

            itemsData.Add(new itemSaveData(it.typeItem, it.nameItem, it.id, it.countItem, it.isStackable, it.pathIcon, it.pathPrefab));
        }
    }
    #endregion
}
