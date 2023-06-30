using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{
    [SerializeField] private float timerFlip;
    [SerializeField] private float timerFlipMax;
    RaycastHit2D attackRay;
    [SerializeField] private float attackRayDistance;
    private Enemy enemy;

    private void Start()
    {
        InstValues();
    }

    private void InstValues()
    {
        enemy = GetComponent<Enemy>();
        timerFlip = timerFlipMax;
    }

    private void FixedUpdate()
    {
        TimerFlip();
    }

    public void AttackRay()
    {
        attackRay = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, attackRayDistance, enemy.layerMaskOnlyPlayer);

        if(attackRay)
        {
            GetComponent<Enemy>().RangAttack();
        }
    }

    private void TimerFlip()
    {
        if (timerFlip > 0)
        {
            timerFlip -= Time.deltaTime;
        }
        else if (timerFlip <= 0)
        {
            Character.Flip(transform, GetComponent<Enemy>().facingRight);
            timerFlip = timerFlipMax;
            attackRayDistance *= -1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * attackRayDistance);
    }
}
