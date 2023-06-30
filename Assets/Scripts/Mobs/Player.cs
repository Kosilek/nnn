using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Cntr")]
    public float speed;
    [SerializeField] private float vSpeed;
    private float direction;
    // bool check
    public bool isGround;
    private bool facingRight = true;

    private Rigidbody2D rb;
    private Animator anim;

    public static GameObject player;
    public float xp;
    public static int lvl = 1;
    public int limitationXpLvl;

    [SerializeField] private AudioSource effectsAttack;

    private void Awake()
    {
        InstValuesAwake();
    }

    private void InstValuesAwake()
    {
        player = gameObject;
        Physics2D.queriesStartInColliders = false;
    }

    private void Start()
    {
        InstValues();
        AddEvent();
    }

    private void InstValues()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void AddEvent()
    {
        Event.OnReSpeed.AddListener(ReSpeed);
        Event.OnAddXp.AddListener(xpPlus);
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

    private void Flip()
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

    public void ReSpeed(float oldSpeed, float newSpeed)
    {
        speed = speed - oldSpeed + newSpeed;
    }

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
        anim.SetTrigger(MeaningString.attack);
        effectsAttack.Play();
    }
    #endregion

    public void xpPlus(float xpEnemy)
    {
        xp += xpEnemy;
        lvlUp();
    }

    private void lvlUp()
    {
        if (xp >= limitationXpLvl)
        {
            lvl++;
            limitationXpLvl *= 2;
            Event.SendLvlUp();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UpCoins(collision);
    }

    private void UpCoins(Collider2D collision)
    {
        if (collision.GetComponent<Coins>() != null)
        {
            collision.GetComponent<Coins>().AddCoinsSouls();
            collision.GetComponent<Coins>().PlayEffects();
        }
    }
}
