using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiliENemy : MonoBehaviour
{
    #region RaycastHit2D
    RaycastHit2D blockInfo;
    [SerializeField] private float distBlockInfo;
    RaycastHit2D cliffInfo;
    [SerializeField] private float distCliffInfo;
    RaycastHit2D playerDetection;
    [SerializeField] private float playerDistance;
    RaycastHit2D attackRay;
    [SerializeField] private float attackRayDistance;
    #endregion
    [SerializeField] private Transform cliffDetection;
    [SerializeField] private Transform blockDetection;
    int layerMaskOnlyPlayer = 1 << 8;

    private void Update()
    {
        cliffInfo = Physics2D.Raycast(cliffDetection.position, Vector3.down, distCliffInfo);
    }

    public void AttackRay()
    {
        if (GetComponent<Enemy>().facingRight)
        {
            attackRay = Physics2D.Raycast(blockDetection.position, blockDetection.localScale.x * Vector2.right, attackRayDistance, layerMaskOnlyPlayer);
        }
        else if (!GetComponent<Enemy>().facingRight)
        {
            attackRay = Physics2D.Raycast(blockDetection.position, blockDetection.localScale.x * Vector2.left, attackRayDistance, layerMaskOnlyPlayer);
        }
        if (attackRay)
        {
            GetComponent<Enemy>().moov = false;
            Debug.Log($"attackRay = {attackRay.collider}");
            if (attackRay.collider.GetComponent<Player>())
            {
                GetComponent<Enemy>().Attack();
            }
        }
        else if (!attackRay)
        {
            GetComponent<Enemy>().moov = true;
        }
    }

    public void FlipEnemy()
    {
        if (GetComponent<Enemy>().facingRight)
        {
            blockInfo = Physics2D.Raycast(blockDetection.position, blockDetection.localScale.x * Vector2.right, distBlockInfo);
        }
        else if (!GetComponent<Enemy>().facingRight)
        {
            blockInfo = Physics2D.Raycast(blockDetection.position, blockDetection.localScale.x * Vector2.left, distBlockInfo);
        }
        if (blockInfo)
        {
            if (blockInfo.collider.name == MeaningString.ground && !GetComponent<Enemy>().facingRight)
            {
                GetComponent<Enemy>().direction = 1;
                GetComponent<Enemy>().facingRight = Character.Flip(transform, GetComponent<Enemy>().facingRight);
            }
            else if (blockInfo.collider.name == MeaningString.ground && GetComponent<Enemy>().facingRight)
            {
                GetComponent<Enemy>().direction = -1;
                GetComponent<Enemy>().facingRight = Character.Flip(transform, GetComponent<Enemy>().facingRight);
            }
        }
        if (cliffInfo == false && !GetComponent<Enemy>().facingRight)
        {
            GetComponent<Enemy>().direction = 1;
            GetComponent<Enemy>().facingRight = Character.Flip(transform, GetComponent<Enemy>().facingRight);

        }
        else if (cliffInfo == false && GetComponent<Enemy>().facingRight)
        {
            GetComponent<Enemy>().direction = -1;
            GetComponent<Enemy>().facingRight = Character.Flip(transform, GetComponent<Enemy>().facingRight);
        }
    }

    public void FlipEnemyTrigger()
    {
        if (GetComponent<Enemy>().facingRight)
        {
            playerDetection = Physics2D.Raycast(transform.position, transform.localScale.x * Vector3.left, playerDistance, layerMaskOnlyPlayer);
        }
        else if (!GetComponent<Enemy>().facingRight)
        {
            playerDetection = Physics2D.Raycast(transform.position, transform.localScale.x * Vector3.right, playerDistance, layerMaskOnlyPlayer);
        }
        if (playerDetection)
        {
            //  Debug.Log(playerDetection.collider.name);
            if (playerDetection.collider.GetComponent<Player>() && !GetComponent<Enemy>().facingRight)
            {
                //  Debug.Log("left");
                GetComponent<Enemy>().direction = 1;
                GetComponent<Enemy>().facingRight = Character.Flip(transform, GetComponent<Enemy>().facingRight);
            }
            else if (playerDetection.collider.GetComponent<Player>() && GetComponent<Enemy>().facingRight)
            {
                //   Debug.Log("right");
                GetComponent<Enemy>().direction = -1;
                GetComponent<Enemy>().facingRight = Character.Flip(transform, GetComponent<Enemy>().facingRight);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(blockDetection.position, blockDetection.position + Vector3.right * distBlockInfo);
        Gizmos.DrawLine(blockDetection.position, blockDetection.position + Vector3.left * distBlockInfo);

        Gizmos.DrawLine(cliffDetection.position, cliffDetection.position + Vector3.down * distCliffInfo);

        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * playerDistance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * playerDistance);

        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * attackRayDistance);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * attackRayDistance);
    }
}
