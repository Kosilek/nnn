using UnityEngine;
using UnityEngine.Events;
public static class Event
{
   public static UnityEvent<int> OnScoreCoinsSouls = new UnityEvent<int>();

    public static void SendScoreCoinsSouls(int score)
    {
        OnScoreCoinsSouls.Invoke(score);
    }
}
