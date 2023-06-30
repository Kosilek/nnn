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
    private Enemy enemy;

    private void Start()
    {
        InstValues();
    }

    private void InstValues()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        cliffInfo = Physics2D.Raycast(cliffDetection.position, Vector3.down, distCliffInfo);
    }

    public void AttackRay()
    {
        if (enemy.facingRight)
        {
            attackRay = Physics2D.Raycast(blockDetection.position, blockDetection.localScale.x * Vector2.right, attackRayDistance, enemy.layerMaskOnlyPlayer);
        }
        else if (!GetComponent<Enemy>().facingRight)
        {
            attackRay = Physics2D.Raycast(blockDetection.position, blockDetection.localScale.x * Vector2.left, attackRayDistance, enemy.layerMaskOnlyPlayer);
        }
        if (attackRay)
        {
            enemy.moov = false;
            if (attackRay.collider.GetComponent<Player>())
            {
                enemy.Attack();
            }
        }
        else if (!attackRay)
        {
            enemy.moov = true;
        }
    }

    public void FlipEnemy()
    {
        if (enemy.facingRight)
        {
            blockInfo = Physics2D.Raycast(blockDetection.position, blockDetection.localScale.x * Vector2.right, distBlockInfo);
        }
        else if (!GetComponent<Enemy>().facingRight)
        {
            blockInfo = Physics2D.Raycast(blockDetection.position, blockDetection.localScale.x * Vector2.left, distBlockInfo);
        }
        if (blockInfo)
        {
            if (blockInfo.collider.name == MeaningString.ground && !enemy.facingRight)
            {
                enemy.direction = 1;
                enemy.facingRight = Character.Flip(transform, enemy.facingRight);
            }
            else if (blockInfo.collider.name == MeaningString.ground && enemy.facingRight)
            {
                enemy.direction = -1;
                enemy.facingRight = Character.Flip(transform, enemy.facingRight);
            }
        }
        if (cliffInfo == false && !enemy.facingRight)
        {
            enemy.direction = 1;
            enemy.facingRight = Character.Flip(transform, enemy.facingRight);

        }
        else if (cliffInfo == false && enemy.facingRight)
        {
            enemy.direction = -1;
            enemy.facingRight = Character.Flip(transform, enemy.facingRight);
        }
    }

    public void FlipEnemyTrigger()
    {
        if (enemy.facingRight)
        {
            playerDetection = Physics2D.Raycast(transform.position, transform.localScale.x * Vector3.left, playerDistance, enemy.layerMaskOnlyPlayer);
        }
        else if (!enemy.facingRight)
        {
            playerDetection = Physics2D.Raycast(transform.position, transform.localScale.x * Vector3.right, playerDistance, enemy.layerMaskOnlyPlayer);
        }
        if (playerDetection)
        {
            if (playerDetection.collider.GetComponent<Player>() && !enemy.facingRight)
            {
                enemy.direction = 1;
                enemy.facingRight = Character.Flip(transform, enemy.facingRight);
            }
            else if (playerDetection.collider.GetComponent<Player>() && enemy.facingRight)
            {
                enemy.direction = -1;
                enemy.facingRight = Character.Flip(transform, enemy.facingRight);
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
