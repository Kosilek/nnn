using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttributesItem
{
    GeneralActive ga = new GeneralActive();
    #region AttributesItem
    public float Damage(int levelItem)
    {
        int min, max;
        float damage;
        (min, max) = ReceivingValue(MeaningString.weapon);
        damage = ga.Rand(levelItem, min, max);
        return damage;
    }

    public float Armor(int levelItem)
    {
        int min, max;
        (min, max) = ReceivingValue(MeaningString.armor);
        float armor = ga.Rand(levelItem, min, max);
        return armor;
    }

    public float Heathl (int levelItem)
    {
        int min, max;
        (min, max) = ReceivingValue(MeaningString.heatlh);
        float heathl = ga.Rand(levelItem, min, max);
        return heathl;
    }
    
    public float Resistance (int levelItem)
    {
        int min, max;
        (min, max) = ReceivingValue(MeaningString.resistance);
        float resistance = ga.Rand(levelItem, min, max);
        return resistance;
    }

    public float Spike (int levelItem)
    {
        int min, max;
        (min, max) = ReceivingValue(MeaningString.spike);
        float spike = ga.Rand(levelItem, min, max);
        return spike;
    }

    public float Speed (int levelItem)
    {
        int min, max;
        (min, max) = ReceivingValue(MeaningString.speed);
        float speed = ga.Rand(levelItem, min, max);
        return speed;
    }

    public float Vampirism(int levelItem)
    {
        int min, max;
        (min, max) = ReceivingValue(MeaningString.vampirism);
        float vampirism = ga.Rand(levelItem, min, max);
        return vampirism;
    }
    #endregion

    private (int, int) ReceivingValue(string type)
    {
        int min = 0;
        int max = 0;
        switch (type)
        {
            case MeaningString.weapon:
                min = 7;
                max = 14;
                break;
            case MeaningString.armor:
                min = 5;
                max = 10;
                break;
            case MeaningString.heatlh:
                min = 20;
                max = 40;
                break;
            case MeaningString.resistance:
                min = 2;
                max = 4;
                break;
            case MeaningString.spike:
                min = 1;
                max = 4;
                break;
            case MeaningString.speed:
                min = 1;
                max = 2;
                break;
            case MeaningString.vampirism:
                min = 1;
                max = 2;
                break;
        }
        return (min, max);
    }
}
