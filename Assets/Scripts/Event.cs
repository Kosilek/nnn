using UnityEngine;
using UnityEngine.Events;
public static class Event
{
    public static UnityEvent<int> OnScoreCoinsSouls = new UnityEvent<int>();
    public static UnityEvent<GameObject, int, int> OnAddStackableItem = new UnityEvent<GameObject, int, int>();
    public static UnityEvent<GameObject> OnAddUnStackableItem = new UnityEvent<GameObject>();
    public static UnityEvent<float, float> OnReWeapon = new UnityEvent<float, float>();
    public static UnityEvent<float, float, float> OnReHelmet = new UnityEvent<float, float, float>();
    public static UnityEvent<float, float, float> OnReBib = new UnityEvent<float, float, float>();
    public static UnityEvent<float, float, float> OnReGloves = new UnityEvent<float, float, float>();
    public static UnityEvent<float, float, float, float> OnReBoots = new UnityEvent<float, float, float, float>();
    public static UnityEvent<float, float> OnReAmulet = new UnityEvent<float, float>();
    public static UnityEvent<float, float> OnReRing1 = new UnityEvent<float, float>();
    public static UnityEvent<float, float> OnReRing2 = new UnityEvent<float, float>();
    public static UnityEvent<float, float> OnReBracelete = new UnityEvent<float, float>();

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

    public static void SendReWeapon(float damage, float vampirism)
    {
        OnReWeapon.Invoke(damage, vampirism);
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
    }
}
