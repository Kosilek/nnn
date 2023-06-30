using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEmptyWall : MonoBehaviour
{
    [SerializeField] private GameObject wall;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            wall.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        wall.SetActive(true);
    }
}
