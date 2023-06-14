using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formulas
{
    private const float coefficientArmor = 0.9f;
   public float DamageForm(float damage, float armor)
    {
        return (damage - (armor * coefficientArmor));
    }

    public float Strenght(int strenght)
    {
        return strenght * 7.5f;
    }

    public float Dedexterity(float dedexterity)
    {
        return dedexterity * 1.5f;
    }

    public float Intelligance(float intelligance)
    {
        return intelligance * 1.25f;
    }
}
