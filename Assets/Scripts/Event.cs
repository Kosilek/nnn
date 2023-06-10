using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
public static class Event
{
    public static UnityEvent<int> OnScoreCoinsSouls = new UnityEvent<int>();
    public static UnityEvent<GameObject, int, int> OnAddStackableItem = new UnityEvent<GameObject, int, int>();
    public static UnityEvent<GameObject> OnAddUnStackableItem = new UnityEvent<GameObject>();
    public static UnityEvent<float, float> OnReDamage = new UnityEvent<float, float>();
    public static UnityEvent<float, float> OnReArmor = new UnityEvent<float, float>();
    public static UnityEvent<float, float> OnReHealth = new UnityEvent<float, float>();
    public static UnityEvent<float, float> OnReResistiance = new UnityEvent<float, float>();
    public static UnityEvent<float, float> OnReSpike = new UnityEvent<float, float>();
    public static UnityEvent<float, float> OnReSpeed = new UnityEvent<float, float>();
    public static UnityEvent<float, float> OnReVampirism = new UnityEvent<float, float>();
    public static UnityEvent<GameObject> OnDropItem = new UnityEvent<GameObject>();
    public static UnityEvent<int> OnRemoveDropItem = new UnityEvent<int>();
    public static UnityEvent OnInstIndexDropItem = new UnityEvent();

    public static void SendReDamage(float oldF, float newF)
    {
        OnReDamage.Invoke(oldF, newF);
    }

    public static void SendReArmor(float oldF, float newF)
    {
        OnReArmor.Invoke(oldF, newF);
    }
    public static void SendReHealth(float oldF, float newF)
    {
        OnReHealth.Invoke(oldF, newF);
    }
    public static void SendReResistiance(float oldF, float newF)
    {
        OnReResistiance.Invoke(oldF, newF);
    }
    public static void SendReSpike(float oldF, float newF)
    {
        OnReSpike.Invoke(oldF, newF);
    }
    public static void SendReSpeed(float oldF, float newF)
    {
        OnReSpeed.Invoke(oldF, newF);
    }
    public static void SendReVampirism(float oldF, float newF)
    {
        OnReVampirism.Invoke(oldF, newF);
    }
    public static void SendDropItem(GameObject drop)
    {
        OnDropItem.Invoke(drop);
    }
    public static void SendRemoveDropItem(int index)
    {
        OnRemoveDropItem.Invoke(index);
    }

    public static void SendInstIndexDropItem()
    {
        OnInstIndexDropItem.Invoke();
    }



    /* 
     public static UnityEvent<float, float, float, float> OnReWeapon = new UnityEvent<float, float, float, float>();
     // public static UnityEvent<float, float, float, float, float, float> OnReHelmet = new UnityEvent<float, float, float, float, float, float>();
     public static UnityEvent<float, float, float, float, float> OnReHelmet = new UnityEvent<float, float, float, float, float>();
     public static UnityEvent<float, float, float, float, float, float> OnReBib = new UnityEvent<float, float, float, float, float, float>();
     public static UnityEvent<float, float, float, float, float, float> OnReGloves = new UnityEvent<float, float, float, float, float, float>();
     public static UnityEvent<float, float, float, float, float> OnReBoots = new UnityEvent<float, float, float, float, float>();
    public static UnityEven
     public static UnityEvent<float, float, float, float> OnReAmulet = new UnityEvent<float, float, float, float>();
     public static UnityEvent<float, float, float, float> OnReRing1 = new UnityEvent<float, float, float, float>();
     public static UnityEvent<float, float, float, float> OnReRing2 = new UnityEvent<float, float, float, float>();
     public static UnityEvent<float, float, float, float> OnReBracelete = new UnityEvent<float, float, float, float>();
     */
    public static void SendScoreCoinsSouls(int score)
    {
        OnScoreCoinsSouls.Invoke(score);
    }

    public static void SendAddStackableItem(GameObject item, int id, int itemCount)
    {
        OnAddStackableItem.Invoke(item, id, itemCount);
    }

    public static void SendAddUnStackableItem(GameObject item)
    {
        OnAddUnStackableItem.Invoke(item);
    }

   /* public static void SendReWeapon(float damage, float vampirism, float oldDamage, float oldVampirisme)
    {
        OnReWeapon.Invoke(damage, vampirism, oldDamage, oldVampirisme);
    }

    public static void SendReHelmet(float armor, float heatlh, float spike)
    {
        OnReHelmet.Invoke(armor, heatlh, spike);
    }

    public static void SendReBib(float armor, float heatlh, float spike)
    {
        OnReBib.Invoke(armor, heatlh, spike);
    }

    public static void SendReGloves(float armor, float heatlh, float spike)
    {
        OnReGloves.Invoke(armor, heatlh, spike);
    }

    public static void SendReBoots(float armor, float heatlh, float spike, float speed)
    {
        OnReBoots.Invoke(armor, heatlh, spike, speed);
    }

    public static void SendReAmulet(float heatlh, float resistiance)
    {
        OnReAmulet.Invoke(heatlh, resistiance);
    }

    public static void SendReRing1(float heatlh, float resistiance)
    {
        OnReRing1.Invoke(heatlh, resistiance);
    }

    public static void SendReRing2(float heatlh, float resistiance)
    {
        OnReRing2.Invoke(heatlh, resistiance);
    }

    public static void SendReBracelete(float heatlh, float resistiance)
    {
        OnReBracelete.Invoke(heatlh, resistiance);
    }*/
}
   