using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    //���������� ��������� � ��� ���������
    private float speed;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float vSpeed;
    private float direction;
    // ������ ���������� ��� ��������
    public bool isGround;
    private bool facingRight = true;
    // ���������� ��� ���������� ����������� ������������ 
    private Rigidbody2D rb;
    private Animator anim;
    // ���������� ��� �������� ������ � ������ �������
    public static GameObject player;

    private void Awake()
    {
        player = gameObject;
        Physics2D.queriesStartInColliders = false;
    }

    private void Start()
    {
        speed = baseSpeed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        //Event.OnReSpeed.AddListener(ReSpeed);
    }

    private void Update()
    {
        Character.SetAnimatorJump(anim, isGround, rb);
    }

    private void FixedUpdate()
    {
        anim.SetFloat("Speed", Mathf.Abs(direction));
        Character.Run(rb, speed, direction);
        Flip();
    }

    private void Flip() //��������
    {
        if (direction > 0 && !facingRight)
        {
            facingRight = Character.Flip(transform, facingRight);

        }
        else if (direction < 0 && facingRight)
        {
            facingRight = Character.Flip(transform, facingRight);
        }
    }

   /* public void ReSpeed(float reSpeed)
    {
        speed += baseSpeed + reSpeed;
    }*/

    //����������
    #region Controll 
    public void MoovLeft()
    {
        direction = -1;
    }

    public void MoovRight()
    {
        direction = 1;
    }

    public void MoovStop()
    {
        direction = 0;
    }

    public void Jump()
    {
        Character.Jump(rb, vSpeed, isGround);
    }

    public void Attack()
    {
        anim.SetBool("Attack", true);
    }

    public void StopAttack()
    {
        anim.SetBool("Attack", false);
    }
    #endregion

}
