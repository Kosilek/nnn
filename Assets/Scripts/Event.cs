using UnityEngine;
using UnityEngine.Events;
public static class Event
{
    public static UnityEvent<int> OnScoreCoinsSouls = new UnityEvent<int>();
    public static UnityEvent<GameObject, int, int> OnAddStackableItem = new UnityEvent<GameObject, int, int>();
    public static UnityEvent<GameObject> OnAddUnStackableItem = new UnityEvent<GameObject>();

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
}
