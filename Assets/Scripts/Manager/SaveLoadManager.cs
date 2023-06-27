using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using System;

public class SaveLoadManager : Singleton<SaveLoadManager>
{
    #region Save
   // public static List<Item> itemSave = new List<Item>();
    #endregion
    #region Manager
    [SerializeField] private Inventory inventory;
    [SerializeField] private DropItem dropItem;
    [SerializeField] private LevelManager levelManager;
    #endregion

    private string filePath;

    protected override void Awake()
    {
        base.Awake();
        filePath = Application.persistentDataPath + "/save.gameSaveMain";
    }

    private void Start()
    {
        inventory = GetComponent<Inventory>();
        dropItem = GetComponent<DropItem>();
        levelManager = GetComponent<LevelManager>();
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath, FileMode.Create);
        Save save = new Save();

        save.SaveItems(inventory.item, save.itemsData);
        save.SaveItems(dropItem.dropItem, save.dropItemData);
        save.SaveEnemy(levelManager.enemySave, save.enemyData);
        save.SaveEnemyResps(levelManager.enemyResp, save.enemyRespsData);
        Debug.Log($"enemySave = {levelManager.enemySave.Count}, enemySaveData = {save.enemyData.Count}");
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
            save.SaveItems(dropItem.dropItem, save.dropItemData);//мб переделать под восоздание предмета
        LoadDropItem(save, dropItem);
            save.SaveEnemy(levelManager.enemySave, save.enemyData);
        LoadEnemy(save, levelManager);
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

    private void LoadEnemy(Save save, LevelManager levelManager)
    {
        int j = 0;
        foreach (var enemy in save.enemyData)
        {
            levelManager.LoadData(enemy, j);
            j++;
        }
    }

    private void LoadEnemyResp(Save save, LevelManager levelManager)
    {
        int j = 0;
        foreach (var enemyResp in save.enemyRespsData)
        {
            levelManager.LoadDataResp(enemyResp, j);
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
    
    public struct DamageObject
    {
        public float damage;
        public bool physDamage;
        public bool magicDamage;
        public bool poison;
        public bool fire;
        public bool electric;
        public float valuePoison;
        public float timePoison;
        public float fireDamage;
        public float valueElectric;
        public float timerElectric;
        public DamageObject(float damage, bool physDamage, bool magicDamage, bool poison, bool fire, bool electric, float valuePoison, float timePoison, float fireDamage, float valueElectric, float timerElectric)
        {
            this .damage = damage;
            this .physDamage = physDamage;
            this .magicDamage = magicDamage;
            this .poison = poison;
            this .fire = fire;
            this .electric = electric;
            this .valuePoison = valuePoison;
            this.timePoison = timePoison;
            this .fireDamage = fireDamage;
            this .valueElectric = valueElectric;
            this .timerElectric = timerElectric;

        }
    }

    public struct Health
    {
        public float health;
        public float maxHealth;
        public float armor;
        public float resistance;
        public float spike;
        public float vampirizme;
        public bool immunPosion;
        public bool immunFire;
        public bool immunElectric;
        public Health(float health, float maxHealth, float armor, float resistance, float spike, float vampirizme,
            bool immunPosion, bool immunFire, bool immunElectric)
        {
            this.health = health;
            this.maxHealth = maxHealth;
            this.armor = armor;
            this.resistance = resistance;
            this.spike = spike;
            this.vampirizme = vampirizme;
            this.immunPosion = immunPosion;
            this.immunFire = immunFire;
            this.immunElectric = immunElectric;

        }
    }

    public struct EnemyData
    {
        public float speed;
        public bool randMonstr;
        public int levelLocation;
        public TypeEnemy typeEnemy;
        public int index;
        public int lvlMonstr;
        public int countSouls;
        public float xp;
        public int coefA;
        public EnemyData(float speed, bool randMonstr, int levelLocation, TypeEnemy typeEnemy, int index, int lvlMonstr, int countSouls, float xp, int coefA)
        {
            this.speed = speed;
            this.randMonstr = randMonstr;
            this.levelLocation = levelLocation;
            this.typeEnemy = typeEnemy;
            this.index = index;
            this.lvlMonstr = lvlMonstr;
            this.countSouls = countSouls;
            this.xp = xp;
            this.coefA = coefA;
        }
    }

    public struct PlayerData
    {
        public float speed;
        public float xp;
        public int limitationXpLvl;
        public PlayerData(float speed, float xp, int limitationXpLvl)
        {
            this.speed = speed;
            this.xp = xp;
            this.limitationXpLvl = limitationXpLvl;
        }
    }

    #region EnemyData
    [System.Serializable] public struct enemySaveData
    {
        public bool randMonstr;
        public int levelLocation;
        public TypeEnemy typeEnemy;
        public TypeEnemySpecial typeSpecial;
        public int index;
        public int lvlMonstr;
        public float damage;
        public float armor;
        public float maxHeathl;
        public float resistiance;
        public float spike;
        public float speed;
        public float vampirism;
        public bool physDamage;
        public bool magicDamage;
        public bool poison;
        public bool fire;
        public bool electric;
        public float valuePoison;
        public float timePoison;
        public float fireDamage;
        public float valueElectric;
        public float timerElectric;
        public bool immunPosion;
        public bool immunFire;
        public bool immunElectric;
        public int countSouls;
        public float xp;
      //  public GameObject[] dropItemPre;
       // public GameObject dropItem;
        public int coefA;
        public Vec3 Position;
        public enemySaveData(bool randMonstr, int levelLocation, TypeEnemy typeEnemy, TypeEnemySpecial typeSpecial,
            int index, int lvlMonstr, float damage, float armor, float maxHeathl, float resistiance, float spike, 
            float speed, float vampirism, bool physDamage, bool magicDamage, bool poison, bool fire, bool electric,
            float valuePoison, float timePoison, float fireDamage, float valueElectric, float timerElectric,
            bool immunPosion, bool immunFire, bool immunElectric, int countSouls, float xp,/* GameObject[] dropItemPre,
            GameObject dropItem,*/ int coefA, Vec3 pos)
        {
            this.randMonstr = randMonstr;
            this.levelLocation = levelLocation;
            this.typeEnemy = typeEnemy;
            this.typeSpecial = typeSpecial;
            this.index = index;
            this.lvlMonstr = lvlMonstr;
            this.damage = damage;
            this.armor = armor;
            this.maxHeathl = maxHeathl;
            this.resistiance = resistiance;
            this.spike = spike;
            this.speed = speed;
            this.vampirism = vampirism;
            this.physDamage = physDamage;
            this.magicDamage = magicDamage;
            this.poison = poison;
            this.fire = fire;
            this.electric = electric;
            this.valuePoison = valuePoison;
            this.timePoison = timePoison;
            this.fireDamage = fireDamage;
            this.valueElectric = valueElectric;
            this.timerElectric = timerElectric;
            this.immunPosion = immunPosion;
            this.immunFire = immunFire;
            this.immunElectric = immunElectric;
            this.countSouls = countSouls;
            this.xp = xp;
          //  this.dropItemPre = dropItemPre;
          //  this.dropItem = dropItem;
            this.coefA = coefA;
            Position = pos;
        }
    }

    public List<enemySaveData> enemyData = new List<enemySaveData>();

    public void SaveEnemy(List<Enemy> enemy, List<enemySaveData> enemyData)
    {
        enemyData.Clear();
        foreach(var go in enemy)
        {
            var it = go;
            it.randMonstr = go.randMonstr;
            it.levelLocation = go.levelLocation;
            it.typeEnemy = go.typeEnemy;
            it.typeSpecial = go.typeSpecial;
            it.index = go.index;
            it.lvlMonstr = go.lvlMonstr;
            it.damage = go.damage;
            it.armor = go.armor;
            it.maxHeathl = go.maxHeathl;
            it.resistiance = go.resistiance;
            it.spike = go.spike;
            it.speed = go.speed;
            it.vampirism = go.vampirism;
            it.physDamage = go.physDamage;
            it.magicDamage = go.magicDamage;
            it.poison = go.poison;
            it.fire = go.fire;
            it.electric = go.electric;
            it.valuePoison = go.valuePoison;
            it.timePoison = go.timePoison;
            it.fireDamage = go.fireDamage;
            it.valueElectric = go.valueElectric;
            it.timerElectric = go.timerElectric;
            it.immunPosion = go.immunPosion;
            it.immunFire = go.immunFire;
            it.immunElectric = go.immunElectric;
            it.countSouls = go.countSouls;
            it.xp = go.xp;
           // it.dropItemPre = go.dropItemPre;
          //  it.dropItem = go.dropItem;
            it.coefA = go.coefA;
            Vec3 pos = new Vec3(go.transform.position.x, go.transform.position.y, go.transform.position.z);

            enemyData.Add(new enemySaveData(it.randMonstr, it.levelLocation, it.typeEnemy, it.typeSpecial, it.index, it.lvlMonstr, it.damage, it.armor, it.maxHeathl, it.resistiance,
                it.spike, it.speed, it.vampirism, it.physDamage, it.magicDamage, it.poison, it.fire, it.electric, it.valuePoison, it.timePoison, it.fireDamage, it.valueElectric, it.timerElectric,
                it.immunPosion, it.immunFire, it.immunElectric, it.countSouls, it.xp, /*it.dropItemPre, it.dropItem, */it.coefA, pos));
        }
    }
    #endregion
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
    #region EnemyRespData
    [System.Serializable] public struct enemyRespData
    {
        public int index;
        public float maxTimer;
        public int lvlLocation;
        public bool randMonstr;
        public TypeEnemy type;
        public TypeEnemySpecial typeSpecial;
        public enemyRespData (int index, float maxTimer, int lvlLocation, bool randMonstr, TypeEnemy type, TypeEnemySpecial typeSpecial)
        {
            this.index = index;
            this.maxTimer = maxTimer;
            this.lvlLocation = lvlLocation;
            this.randMonstr = randMonstr;
            this.type = type;
            this.typeSpecial = typeSpecial;
        }
    }

    public List<enemyRespData> enemyRespsData = new List<enemyRespData> ();

    public void SaveEnemyResps(List<EnemyResp> enemyResp, List<enemyRespData> enemyRespData)
    {
        enemyRespData.Clear ();
        foreach (var go in enemyResp)
        {
            var it = go;
            it.index = go.index;
            it.maxTimer = go.maxTimer;
            it.lvlLocation = go.lvlLocation;
            it.randMonstr = go.randMonstr;
            it.type = go.type;
            it.typeSpecial = go.typeSpecial;
        }
    }
    #endregion
}
