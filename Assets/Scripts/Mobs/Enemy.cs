using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("MainOptions")]
    public bool randMonstr;
    public int levelLocation;
    public TypeEnemy typeEnemy;
    public TypeEnemySpecial typeSpecial;
    public int index;
    #region Stats
    [Header("mainStats")]
    public int lvlMonstr;
    public float damage;
    public float armor;
    public float maxHeathl;
    public float resistiance;
    public float spike;
    public float speed = 4f;
    public float vampirism;
    [Header("mainDamage")]
    public bool physDamage;
    public bool magicDamage;
    #endregion
    #region TypeMagic
    [Header("typeMagicDamage")]
    public bool poison;
    public bool fire;
    public bool electric;
    [Space(10)]
    public float valuePoison;
    public float timePoison;
    [Space(10)]
    public float fireDamage;
    [Space(10)]
    public float valueElectric;
    public float timerElectric;
    #endregion
    #region Immunity
    public bool immunPosion;
    public bool immunFire;
    public bool immunElectric;
    #endregion
    GeneralActive ga = new GeneralActive();
    #region dropDeathMonsrt
    public int countSouls;
    public float xp;
    public GameObject[] dropItemPre;
    public GameObject dropItem;
    #endregion
    public int coefA;

    private void Awake()
    {
        if (randMonstr)
        {
            CoefTypeEnemy();
            lvlMonstr = RandLevelMonstr();
            countSouls = ValueCountSouls();
            xp = ValueXp();
            (damage, armor, maxHeathl, resistiance) = InstallStat();
            InstallStatTypeEnemy();
        }
        else if (!randMonstr)
        {

        }
        TypeDropItem();
        GetComponent<Health>().armor = armor;
        GetComponent<Health>().maxHealth = maxHeathl;
        GetComponent<Health>().resistance = resistiance;
        GetComponent<Health>().spike = spike;
        GetComponent<Health>().vampirizme = vampirism;
      //  GetComponent<DamageObject>.damage = damage; 
    }

    private void Start()
    {
        Event.SendEnemy(gameObject);
        Event.SendInstIndexEnemy();
    }
    #region InstallRandStatEnemy
    private void InstallStatTypeEnemy()
    {
        Debug.Log("7");
        switch (typeSpecial)
        {
            case TypeEnemySpecial.normal:
                physDamage = true;
                magicDamage = false;
                break;
            case TypeEnemySpecial.fly:
                physDamage = true;
                magicDamage = false;
                maxHeathl -= 15 * lvlMonstr;
                GetComponent<Rigidbody2D>().gravityScale = 0;
                speed += 2f;
                armor = 0f;
                break;
            case TypeEnemySpecial.magic:
                magicDamage = true;
                physDamage = false;
                armor -= 5f;
                resistiance += 10f;
                damage = 0f;
                TypeMagicDamage();
                break;
            case TypeEnemySpecial.knight:
                speed -= 1.5f;
                armor += 7f * lvlMonstr;
                damage -= 3f * lvlMonstr;
                maxHeathl += 60f * lvlMonstr;
                resistiance += 2f * lvlMonstr;
                spike += 5f + 3f * lvlMonstr;
                immunElectric = true;
                immunFire = true;
                immunPosion = true;
                break;
            case TypeEnemySpecial.vampir:
                speed += 1f;
                armor -= 3f * lvlMonstr;
                damage += 4f * lvlMonstr;
                vampirism += 4f * lvlMonstr;
                break;
            case TypeEnemySpecial.sniper:
                speed = 0f;
                damage += 15f * lvlMonstr;
                armor = 0f;
                resistiance = 0f;
                break;
        }    
    }

    private void Posion()
    {
        timePoison = (float)ga.Rand(2 * lvlMonstr, 4 * lvlMonstr);
        valuePoison = (float)ga.Rand(lvlMonstr, 4 * lvlMonstr);
    }

    private void Fire()
    {
        fireDamage = (float)ga.Rand(5 * lvlMonstr, 10 * lvlMonstr);
    }

    private void Electric()
    {
        timerElectric = (float)ga.Rand(2 * lvlMonstr, 4 * lvlMonstr);
        valueElectric = (float)ga.Rand(lvlMonstr, 4 * lvlMonstr);
    }

    private void TypeMagicDamage()
    {
        int i = ga.Rand(3);
        switch (i)
        {
            case 0:
                poison = true;
                Posion();
                break;
            case 1:
                fire = true;
                Fire();
                break;
            case 2:
                electric = true;
                Electric();
                break;
                default: break;
        }
    }

    private (float, float, float, float) InstallStat()
    {
        Debug.Log("6");
        float damage = 10f, armor = 0f, health = 60f, resistiance = 0f;
        damage = (damage + lvlMonstr) * lvlMonstr * coefA;
        armor = (armor + lvlMonstr + 3) * lvlMonstr * coefA;
        health = (health + lvlMonstr + 10) * lvlMonstr * coefA;
        resistiance = (resistiance + lvlMonstr + 1) * lvlMonstr * coefA;
        return (damage, armor, health, resistiance);
    }

    private void TypeDropItem()
    {
        Debug.Log("5");
        Debug.Log(dropItemPre.Length);
        int i = ga.Rand(dropItemPre.Length);
        Debug.Log(i);
        dropItem = dropItemPre[i];
    }

    private int ValueXp()
    {
        Debug.Log("4");
        return ga.Rand(lvlMonstr * 2 * coefA, lvlMonstr * 4 * coefA);
    }
     
    private int ValueCountSouls()
    {
        Debug.Log("3");
        return ga.Rand(lvlMonstr * coefA, (lvlMonstr + 10) * coefA);
    }

    private int RandLevelMonstr()
    {
        Debug.Log("2");
        int min = 0, max = 0;
        if (levelLocation == 1)
        {
            min = 1;
            max = 2;
        } else if (levelLocation > 1)
        {
            min = levelLocation - 1;
            max = levelLocation + 1;
        }
        return ga.Rand(min, max);
    }

    private void CoefTypeEnemy()
    {
        Debug.Log("1");
        switch (typeEnemy)
        {
            case TypeEnemy.normal:
                coefA = 1;
                break;
            case TypeEnemy.elit:
                coefA = 2;
                break;
            case TypeEnemy.boss:
                coefA = 3;
                break;
            case TypeEnemy.mainBoss:
                coefA = 4;
                break;
        }
    }
    #endregion
    public void AddSoulsCoins()
    {
        Event.SendScoreCoinsSouls(countSouls);
    }

    public void DropItem()
    {
        int min, max;
        (min, max) = RandLvlItem();
        dropItem.GetComponent<Item>().levelItem = ga.Rand(min, max);
        dropItem.GetComponent<Item>().customizable = false;
        dropItem.GetComponent<Item>().dropItemBool = true;
        Instantiate(dropItem, transform.position, transform.rotation);
    }

    private (int, int) RandLvlItem()
    {
        int min = 0, max = 0;
        if (lvlMonstr == 1)
        {
            min = 1; 
            max = 2;
        } 
        else if (lvlMonstr > 1 )
        {
            min = lvlMonstr - 1;
            max = lvlMonstr + 1;
        }
        return (min, max);
    }
}

public enum TypeEnemy
{
    normal,
    elit,
    boss,
    mainBoss
}

public enum TypeEnemySpecial
{
    normal,
    fly,
    magic,
    knight,
    vampir,
    sniper
}

public enum TypeMoov
{
    patrul,
    jump,
    security
}
