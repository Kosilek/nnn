using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    //переменные связанные с упр персонажа
    private float speed;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float vSpeed;
    private float direction;
    // булевы переменные для проверок
    public bool isGround;
    private bool facingRight = true;
    // переменные для управления встроенными компонентами 
    private Rigidbody2D rb;
    private Animator anim;
    // переменные для передачи данных в другие скрипты
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

    private void Flip() //разворот
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

    //управление
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
