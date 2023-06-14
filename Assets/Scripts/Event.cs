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
    public static UnityEvent OnLvlUp = new UnityEvent();
    public static UnityEvent OnStrenght = new UnityEvent();
    public static UnityEvent OnDexterity = new UnityEvent();
    public static UnityEvent OnIntelligance = new UnityEvent();
    public static UnityEvent<float> OnAddXp = new UnityEvent<float>();
    public static UnityEvent<GameObject> OnEnemy = new UnityEvent<GameObject>();
    public static UnityEvent<int> OnRemoveEnemy = new UnityEvent<int>();
    public static UnityEvent OnInstIndexEnemy = new UnityEvent();

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
    public static void SendLvlUp()
    {
        OnLvlUp.Invoke();
    }
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
    public static void SendStrenght()
    {
        OnStrenght.Invoke();
    }
    public static void SendDexterity()
    {
        OnDexterity.Invoke();
    }
    public static void SendIntelligance()
    {
        OnIntelligance.Invoke();
    }
    public static void SendAddXp(float xp)
    {
        OnAddXp.Invoke(xp);
    }
    public static void SendEnemy(GameObject enemy)
    {
        OnEnemy.Invoke(enemy);
    }
    public static void SendRemoveEnemy(int index)
    {
        OnRemoveEnemy.Invoke(index);
    }
    public static void SendInstIndexEnemy()
    {
        OnInstIndexEnemy.Invoke();
    }
}
   