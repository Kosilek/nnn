using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public int countSouls;

    private void Start()
    {
        AddEvent();
    }

    private void AddEvent()
    {
        Event.SendDispSoulCoin();
        Event.OnScoreCoinsSouls.AddListener(AddCoinsSouls);
        Event.OnTakeCoinsSouls.AddListener(TakeCoinsSouls);
    }

    public void SeeSouls(int money)
    {

    }

    public void AddCoinsSouls(int count)
    {
        countSouls += count;      
        Event.SendDispSoulCoin();
    }

    public void TakeCoinsSouls(int money)
    {
        if (countSouls >= money)
        {
            countSouls -= money;
        }
        Event.SendDispSoulCoin();
    }
}
