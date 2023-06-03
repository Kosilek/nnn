using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private int coins;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    public void AddCoinsSouls()
    {
        Event.SendScoreCoinsSouls(coins);
        ObjectManager.Destroy(gameObject, anim);
    }


}
