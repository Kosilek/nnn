using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealerCntr : MonoBehaviour
{
    [SerializeField] private GameObject buttonMagazin;

    private void Start()
    {
        buttonMagazin.SetActive(false);
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
