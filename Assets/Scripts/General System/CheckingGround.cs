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
        CheckGround(collision, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CheckGround(collision, false);
    }

    private void CheckGround(Collider2D collision, bool check)
    {
        if (collision.GetComponent<Tilemap>() != null)
        {
            if (isPlayer)
            {
                mob.GetComponent<Player>().isGround = check;
            }
            else if (!isPlayer)
            {
                // mob.GetComponent<Enemy>().isGround = check;
            }
        }
    }
}
