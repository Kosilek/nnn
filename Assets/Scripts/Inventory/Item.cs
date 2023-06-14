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

    private void Start()
    {
        if (dropItemBool)
        {
            Event.SendDropItem(gameObject);
            Event.SendInstIndexDropItem();
            x = transform.position.x;
            y = transform.position.y;
        }
        if (customizable == true)
        {
            return;
        }
        else if (customizable == false)
        {
            if (dropItemBool == false)
            {
                levelItem = ga.Rand();
            }           
            valueAttributes = levelItem + 1;
            SwitchAttributes(typeItem);
            lenghtAttributes = 0;
            while (lenghtAttributes != valueAttributes)
            {
                InstallAttribyte(typeItem);
            }
            lenghtAttributes = 0;
            InstallAtrributesStatics(typeItem);
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
        lenghtAttributes++;
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
        lenghtAttributes++;
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
        lenghtAttributes++;
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
        lenghtAttributes++;
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
        lenghtAttributes++;
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
        lenghtAttributes++;
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
        lenghtAttributes++;
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
        lenghtAttributes++;
    }

    private void SwitchAttributes(TypeItem type)
    {
        switch (type)
        {
            case TypeItem.weapon:
                damageBool = true;
                vampirismBool = true;
                break;
            case TypeItem.helmet:
                armorBool = true;
                healthBool = true;
                spikeBool = true;  
                break;
            case TypeItem.bib:
                armorBool = true;
                healthBool = true;
                spikeBool = true;
                break;
            case TypeItem.gloves:
                armorBool = true;
                healthBool = true;
                spikeBool = true;
                break;
            case TypeItem.boots:
                armorBool = true;
                healthBool = true;
                spikeBool = true;
                speedBool = true;
                break;
            case TypeItem.amulet:
                resistanceBool = true;
                healthBool = true;
                break;
            case TypeItem.ring:
                resistanceBool = true;
                healthBool = true;
                break;
            case TypeItem.bracelete:
                resistanceBool = true;
                healthBool = true;
                break;
                default: break;
        }
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
        damageInt += 1;
        int value = 0;
        if (damageInt != 0)
        {
            value = damageInt;
            for (int i = 0; i < value; i++)
            {
                damage += aT.Damage(levelItem);
            }
        }
        value = 0;
        if (vampirismInt != 0)
        {
            value = vampirismInt;
            for (int i = 0; i < value; i++)
            {
                vampirism += aT.Vampirism(levelItem);
            }
        }
    }

    private void InstallAtrributesStaticsHelmet()
    {
        int value = 0;
        if (armorInt != 0)
        {
            value = armorInt;
            for (int i = 0; i < value; i++)
            {
                armor += aT.Armor(levelItem);
            }
        }
        value = 0;
        if (healthInt != 0)
        {
            value = healthInt;
            for (int i = 0; i < value; i++)
            {
                health += aT.Heathl(levelItem);
            }
        }
        value = 0;
        if (spikeInt != 0)
        {
            value = spikeInt;
            for (int i = 0; i < value; i++)
            {
                spike += aT.Spike(levelItem);
            }
        }
    }

    private void InstallAtrributesStaticsBib()
    {
        int value = 0;
        if (armorInt != 0)
        {
            value = armorInt;
            for (int i = 0; i < value; i++)
            {
                armor += aT.Armor(levelItem);
            }
        }
        value = 0;
        if (healthInt != 0)
        {
            value = healthInt;
            for (int i = 0; i < value; i++)
            {
                health += aT.Heathl(levelItem);
            }
        }
        value = 0;
        if (spikeInt != 0)
        {
            value = spikeInt;
            for (int i = 0; i < value; i++)
            {
                spike += aT.Spike(levelItem);
            }
        }
    }

    private void InstallAtrributesStaticsGloves()
    {
        int value = 0;
        if (armorInt != 0)
        {
            value = armorInt;
            for (int i = 0; i < value; i++)
            {
                armor += aT.Armor(levelItem);
            }
        }
        value = 0;
        if (healthInt != 0)
        {
            value = healthInt;
            for (int i = 0; i < value; i++)
            {
                health += aT.Heathl(levelItem);
            }
        }
        value = 0;
        if (spikeInt != 0)
        {
            value = spikeInt;
            for (int i = 0; i < value; i++)
            {
                spike += aT.Spike(levelItem);
            }
        }
    }

    private void InstallAtrributesStaticsBoots()
    {
        int value = 0;
        if (armorInt != 0)
        {
            value = armorInt;
            for (int i = 0; i < value; i++)
            {
                armor += aT.Armor(levelItem);
            }
        }
        value = 0;
        if (healthInt != 0)
        {
            value = healthInt;
            for (int i = 0; i < value; i++)
            {
                health += aT.Heathl(levelItem);
            }
        }
        value = 0;
        if (spikeInt != 0)
        {
            value = spikeInt;
            for (int i = 0; i < value; i++)
            {
                spike += aT.Spike(levelItem);
            }
        }
        value = 0;
        if (speedInt != 0)
        {
            value = speedInt;
            for (int i = 0; i < value; i++)
            {
                speed += aT.Speed(levelItem);
            }
        }
    }

    private void InstallAtrributesStaticsAmulet()
    {
        int value = 0;
        if (resistanceInt != 0)
        {
            value = resistanceInt;
            for (int i = 0; i < value; i++)
            {
                resistance += aT.Resistance(levelItem);
            }
        }
        value = 0;
        if (healthInt != 0)
        {
            value = healthInt;
            for (int i = 0; i < value; i++)
            {
                health += aT.Heathl(levelItem);
            }
        }
    }

    private void InstallAtrributesStaticsRing()
    {
        int value = 0;
        if (resistanceInt != 0)
        {
            value = resistanceInt;
            for (int i = 0; i < value; i++)
            {
                resistance += aT.Resistance(levelItem);
            }
        }
        value = 0;
        if (healthInt != 0)
        {
            value = healthInt;
            for (int i = 0; i < value; i++)
            {
                health += aT.Heathl(levelItem);
            }
        }
    }

    private void InstallAtrributesStaticsBracelete()
    {
        int value = 0;
        if (resistanceInt != 0)
        {
            value = resistanceInt;
            for (int i = 0; i < value; i++)
            {
                resistance += aT.Resistance(levelItem);
            }
        }
        value = 0;
        if (healthInt != 0)
        {
            value = healthInt;
            for (int i = 0; i < value; i++)
            {
                health += aT.Heathl(levelItem);
            }
        }
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
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
