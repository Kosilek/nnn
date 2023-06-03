using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int countSouls;
    private int minRand;
    [SerializeField] private int lvlMonstor;

    private void Start()
    {
        if (lvlMonstor <= 5) minRand = 3;
        else if (lvlMonstor > 6) minRand = 2;
        countSouls = Random.Range(1 * lvlMonstor, minRand * lvlMonstor);
    }

    public void AddSoulsCoins()
    {
        Event.SendScoreCoinsSouls(countSouls);
    }
}
