using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealerCntr : MonoBehaviour
{
    public int levelLocation;
    [SerializeField] private GameObject buttonMagazin;
    private void Start()
    {
        UnActivUI();
    }

    private void UnActivUI()
    {
        buttonMagazin.SetActive(false);
    }


    public void SendLevelLocation()
    {
        Event.SendLevelLocation(levelLocation);
        Event.SendCreateListIndexItem();
        Event.SendCreateListItemStat();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            buttonMagazin.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            buttonMagazin.SetActive(false);
        }
    }
}
