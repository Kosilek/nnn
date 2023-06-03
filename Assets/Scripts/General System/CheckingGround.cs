using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CheckingGround : MonoBehaviour
{
    [SerializeField] private bool isPlayer;
    [SerializeField] private GameObject mob;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Tilemap>() != null)
        {
            if (isPlayer)
            {
                mob.GetComponent<Player>().isGround = true;
            } else if (!isPlayer)
            {
                // mob.GetComponent<Enemy>().isGround = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Tilemap>() != null)
        {
            if (isPlayer)
            {
                mob.GetComponent<Player>().isGround = false;
            }
            else if (!isPlayer)
            {
                // mob.GetComponent<Enemy>().isGround = false;
            }
        }
    }
}
