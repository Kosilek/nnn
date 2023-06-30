using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int indexDropItem;
    [HideInInspector] public float x;
    [HideInInspector] public float y;
    GeneralActive ga = new GeneralActive();
    AttributesItem aT = new AttributesItem();
    [HideInInspector]public int valueAttributes;
    public int levelLocation;
    private int lenghtAttributes;
    [Space]
    [Header("MainVariables")]
    #region MainVariables
    [Space(10)]
    public int levelItem;
    [Space(10)]
    public string nameItem;
    public int id;
    public int countItem;
    public bool isStackable;
    public string pathIcon;
    public string pathPrefab;
    public bool customizable;
    public bool dropItemBool;
    [TextArea]
    public string description;
    #endregion

    [Space]
    [Header("TypeItem")]
    #region TypeItem
    public TypeItem typeItem;
    #endregion

    [Space]
    [Header("AttributesItems != rest")]
    #region AttributesItems
    [Tooltip("TypeItem weapon")]
    public float damage;
    [Tooltip("TypeItem helmet, bib, gloves, boots")]
    public float armor;
    [Tooltip("TypeItem helmet, bib, gloves, boots, ring, amulet, bracelete")]
    public float health;
    [Tooltip("TypeItem ring, amulet, bracelete")]
    public float resistance;
    [Tooltip("TypeItem helmet, bib, gloves, boots")]
    public float spike;
    [Tooltip("TypeItem boots")]
    public float speed;
    [Tooltip("TypeItem weapon")]
    public float vampirism;
    #endregion

    [Space]
    [Header("TypeItem == rest")]
    #region AttributesItems
    public TypeRest typeRest;
    #endregion

    [Space]
    [Header("AttribytesRest")]
    #region AttribytesRest
    [Tooltip("TypeRest Heatlh")]
    public float treatment;
    #endregion

    [Space]
    [Header("BoolAttribures customizable = true")]
    #region BoolAttributes
    public bool damageBool = false;
    public bool armorBool = false;
    public bool healthBool = false;
    public bool resistanceBool = false;
    public bool spikeBool = false;
    public bool speedBool = false;
    public bool vampirismBool = false;
    [Space]
    [Header("if (customizable == true)")]
    public int damageInt = 0;
    public int armorInt = 0;
    public int healthInt = 0;
    public int resistanceInt = 0;
    public int spikeInt = 0;
    public int speedInt = 0;
    public int vampirismInt = 0;
    #endregion

    public bool sellItem;
    public int coefCost = 15;
    public int coefCostSell = 5;
    public int cost;


    private void Start()
    {
        DropItem();
        CustomizbleItem();
    }

    private void CustomizbleItem()
    {
        if (customizable == true)
        {
            return;
        }
        else if (customizable == false)
        {
            GenerationOfCharacter();
        }
    }

    private void GenerationOfCharacter()
    {              
        GenerationValues();
        AreCommonCharacter();
        if (sellItem == false)
            cost = coefCost * levelItem - coefCostSell * levelItem;
    }

    private void DropItem()
    {
        if (dropItemBool)
        {
            AddEvent();
            InstValuesDrop();
        }
    }    

    private void InstValuesDrop()
    {
        x = transform.position.x;
        y = transform.position.y;
    }

    private void AddEvent()
    {
        Event.SendDropItem(gameObject);
        Event.SendInstIndexDropItem();
    }

    public void RandItem(TypeItem typeItem)
    {
        GenerationValues();
        damageInt = 0;
        damage = 0;
        vampirismInt = 0;
        vampirism = 0;
        AreCommonCharacter();
        if (sellItem == true)
            cost = coefCost * levelItem;
    }

    private void GenerationValues()
    {
        if (isStackable == true)
        {
            countItem = 2; // remake
        }
        else if (isStackable == false)
        {
            countItem = 1;
        }
    }

    private void AreCommonCharacter()
    {
        GenerationLevel();
        valueAttributes = levelItem + 1;
        SwitchAttributes(typeItem);
        for (int i = 0; i < valueAttributes; i++)
        {
            InstallAttribyte(typeItem);
        }
        InstallAtrributesStatics(typeItem);
    }

    private void GenerationLevel()
    {
        if (levelLocation >= 2)
        {
            levelItem = ga.Rand(levelLocation - 1, levelLocation + 2);
        }
        else if (levelLocation < 2)
        {
            levelItem = ga.Rand(1, levelLocation + 2);
        }
    }
    #region instAttributesItem
    private void InstallAttribyte(TypeItem type)
    {
        switch (type)
        {
            case TypeItem.weapon:
                InstallAttribyteWeapon();
                break;
            case TypeItem.helmet:
                InstallAttribyteHelmet();
                break;
            case TypeItem.bib:
                InstallAttribyteBib();
                break;
            case TypeItem.gloves:
                InstallAttribyteGloves();
                break;
            case TypeItem.boots:
                InstallAttribyteBoots();
                break;
            case TypeItem.amulet:
                InstallAttribyteAmulet();
                break;
            case TypeItem.ring:
                InstallAttribyteRing();
                break;
            case TypeItem.bracelete:
                InstallAttribyteBracelete();
                break;
            default: break;
        }
    }

    private void InstallAttribyteWeapon()
    {
        int i;
        i = ga.Rand(2);
        switch (i)
        {
            case 0:
                damageInt++;
                break;
            case 1:
                vampirismInt++;
                break;
        }
    }

    private void InstallAttribyteHelmet()
    {
        int i;
        i = ga.Rand(3);
        switch (i)
        {
            case 0:
                armorInt++;
                break;
            case 1:
                healthInt++;
                break;
            case 2:
                spikeInt++;
                break;
        }
    }

    private void InstallAttribyteBib()
    {
        int i;
        i = ga.Rand(3);
        switch (i)
        {
            case 0:
                armorInt++;
                break;
            case 1:
                healthInt++;
                break;
            case 2:
                spikeInt++;
                break;
        }
    }

    private void InstallAttribyteGloves()
    {
        int i;
        i = ga.Rand(3);
        switch (i)
        {
            case 0:
                armorInt++;
                break;
            case 1:
                healthInt++;
                break;
            case 2:
                spikeInt++;
                break;
        }
    }

    private void InstallAttribyteBoots()
    {
        int i;
        i = ga.Rand(4);
        switch (i)
        {
            case 0:
                armorInt++;
                break;
            case 1:
                healthInt++;
                break;
            case 2:
                spikeInt++;
                break;
            case 3:
                speedInt++;
                break;
        }
    }

    private void InstallAttribyteAmulet()
    {
        int i;
        i = ga.Rand(2);
        switch (i)
        {
            case 0:
                resistanceInt++;
                break;
            case 1:
                healthInt++;
                break;
        }
    }

    private void InstallAttribyteRing()
    {
        int i;
        i = ga.Rand(2);
        switch (i)
        {
            case 0:
                resistanceInt++;
                break;
            case 1:
                healthInt++;
                break;
        }
    }

    private void InstallAttribyteBracelete()
    {
        int i;
        i = ga.Rand(2);
        switch (i)
        {
            case 0:
                resistanceInt++;
                break;
            case 1:
                healthInt++;
                break;
        }
    }

    private void SwitchAttributes(TypeItem type)
    {
        switch (type)
        {
            case TypeItem.weapon:
                WeaponItem();
                break;
            case TypeItem.helmet:
                ArmorItem();
                break;
            case TypeItem.bib:
                ArmorItem();
                break;
            case TypeItem.gloves:
                ArmorItem();
                break;
            case TypeItem.boots:
                BootsItem();
                break;
            case TypeItem.amulet:
                DecorationItem();
                break;
            case TypeItem.ring:
                DecorationItem();
                break;
            case TypeItem.bracelete:
                DecorationItem();
                break;
                default: break;
        }
    }

    private void WeaponItem()
    {
        damageBool = true;
        vampirismBool = true;
    }

    private void ArmorItem()
    {
        armorBool = true;
        healthBool = true;
        spikeBool = true;
    }

    private void BootsItem()
    {
        armorBool = true;
        healthBool = true;
        spikeBool = true;
        speedBool = true;
    }

    private void DecorationItem()
    {
        resistanceBool = true;
        healthBool = true;
    }
    #endregion

    #region instAttributesItemStatics
    private void InstallAtrributesStatics(TypeItem type)
    {
        switch (type)
        {
            case TypeItem.weapon:
                InstallAtrributesStaticsWeapon();
                break;
            case TypeItem.helmet:
                InstallAtrributesStaticsHelmet();
                break;
            case TypeItem.bib:
                InstallAtrributesStaticsBib();
                break;
            case TypeItem.gloves:
                InstallAtrributesStaticsGloves();
                break;
            case TypeItem.boots:
                InstallAtrributesStaticsBoots();
                break;
            case TypeItem.amulet:
                InstallAtrributesStaticsAmulet();
                break;
            case TypeItem.ring:
                InstallAtrributesStaticsRing();
                break;
            case TypeItem.bracelete:
                InstallAtrributesStaticsBracelete();
                break;
            default: break;
        }
    }

    private void InstallAtrributesStaticsWeapon()
    {
        InstallAtrributesStaticsWeaponItem();
    }

    private void InstallAtrributesStaticsHelmet()
    {
        InstallAtrributesStaticsArmorItem();
    }

    private void InstallAtrributesStaticsBib()
    {
        InstallAtrributesStaticsArmorItem();
    }

    private void InstallAtrributesStaticsGloves()
    {
        InstallAtrributesStaticsArmorItem();
    }

    private void InstallAtrributesStaticsBoots()
    {
        InstallAtrributesStaticsBootsItem();
    }

    private void InstallAtrributesStaticsAmulet()
    {
        InstallAtrributesStaticsDicorationItem();
    }

    private void InstallAtrributesStaticsRing()
    {
        InstallAtrributesStaticsDicorationItem();
    }

    private void InstallAtrributesStaticsBracelete()
    {
        InstallAtrributesStaticsDicorationItem();
    }

    private void InstallAtrributesStaticsWeaponItem()
    {
        damageInt += 1;
        int value = 0;
        AtDamage(value);
        value = 0;
        AtVampirism(value);
    }

    private void InstallAtrributesStaticsArmorItem()
    {
        int value = 0;
        AtArmor(value);
        value = 0;
        AtHealth(value);
        value = 0;
        AtSpike(value);
    }

    private void InstallAtrributesStaticsBootsItem()
    {
        int value = 0;
        AtArmor(value);
        value = 0;
        AtHealth(value);
        value = 0;
        AtSpike(value);
        value = 0;
        AtSpeed(value);
    }

    private void InstallAtrributesStaticsDicorationItem()
    {
        int value = 0;
        AtResistiance(value);
        value = 0;
        AtHealth(value);
    }

    private void AtDamage(int value)
    {
        if (damageInt != 0)
        {
            value = damageInt;
            for (int i = 0; i < value; i++)
            {
                damage += aT.Damage(levelItem);
            }
        }
    }

    private void AtArmor(int value)
    {
        if (armorInt != 0)
        {
            value = armorInt;
            for (int i = 0; i < value; i++)
            {
                armor += aT.Armor(levelItem);
            }
        }
    }

    private void AtHealth(int value)
    {
        if (healthInt != 0)
        {
            value = healthInt;
            for (int i = 0; i < value; i++)
            {
                health += aT.Heathl(levelItem);
            }
        }
    }

    private void AtSpike(int value)
    {
        if (spikeInt != 0)
        {
            value = spikeInt;
            for (int i = 0; i < value; i++)
            {
                spike += aT.Spike(levelItem);
            }
        }
    }

    private void AtResistiance(int value)
    {
        if (resistanceInt != 0)
        {
            value = resistanceInt;
            for (int i = 0; i < value; i++)
            {
                resistance += aT.Resistance(levelItem);
            }
        }
    }

    private void AtSpeed(int value)
    {
        if (speedInt != 0)
        {
            value = speedInt;
            for (int i = 0; i < value; i++)
            {
                speed += aT.Speed(levelItem);
            }
        }
    }

    private void AtVampirism(int value)
    {
        if (vampirismInt != 0)
        {
            value = vampirismInt;
            for (int i = 0; i < value; i++)
            {
                vampirism += aT.Vampirism(levelItem);
            }
        }
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PickUpAnObject(collision);
    }

    private void PickUpAnObject(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            Event.SendRemoveDropItem(indexDropItem);
            Event.SendInstIndexDropItem();
            Inventory.PickupTrigger(gameObject, isStackable, id, countItem);
        }
    }
}
public enum TypeItem
{
    weapon,
    helmet,
    bib,
    gloves,
    boots,
    amulet,
    ring,
    bracelete,
    rest
}

public enum TypeRest
{
    heatlh,
    forSale,
    gain,
    craft,
    None
}
