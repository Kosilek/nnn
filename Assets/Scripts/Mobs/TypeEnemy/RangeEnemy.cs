using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{
    [SerializeField] private float timerFlip;
    [SerializeField] private float timerFlipMax;
    RaycastHit2D attackRay;
    [SerializeField] private float attackRayDistance;
    int layerMaskOnlyPlayer = 1 << 8;

    private void Start()
    {
        timerFlip = timerFlipMax;
    }

    private void FixedUpdate()
    {
        TimerFlip();
    }

    public void AttackRay()
    {
        //if (GetComponent<Enemy>().facingRight == true)
      //  {
            attackRay = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, attackRayDistance, layerMaskOnlyPlayer);
      //  }
      //  else if (GetComponent<Enemy>().facingRight == false)
      //  {
       //     attackRay = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.left, attackRayDistance, layerMaskOnlyPlayer);
      //  }
        if(attackRay)
        {
           // if (attackRay.collider.GetComponent<Player>())
           // {
                GetComponent<Enemy>().RangAttack();
            Debug.Log("attack");
           // }
        }
      // if (attackRay)
      //  {
        //    Debug.Log($"attackRay = {attackRay.collider}");
           /* if (attackRay.collider.GetComponent<Player>())
            {
                GetComponent<Enemy>().Attack();
            }
        }
        else if (!attackRay)
        {
            GetComponent<Enemy>().moov = true;*/
      //  }
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
     // Gizmos.DrawLine(transform.position, transform.position + Vector3.left * attackRayDistance);
    }
}
