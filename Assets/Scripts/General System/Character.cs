using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : Singleton<Character>
{
    public static int playersSelect;

    protected override void Awake()
    {
        base.Awake();
        if (PlayerPrefs.HasKey(MeaningString.playerSelect))
        {
            playersSelect = PlayerPrefs.GetInt(MeaningString.playerSelect);
        }
    }

    public static void Run(Rigidbody2D rb, float speed, float direction)
    {
        rb.velocity = new Vector2(speed * direction, rb.velocity.y);
    }

    public static void Fall(Rigidbody2D rb, float fall)
    {
        rb.velocity = new Vector2(rb.velocity.x, fall);
    }

    public static void Jump(Rigidbody2D rb, float vSpeed, bool isGrounder)
    {
        if (isGrounder)
        {
            rb.AddForce(new Vector2(0, vSpeed));
        }
    }

    public static bool Flip(Transform transform, bool facingRight)
    {
        transform.Rotate(0f, 180f, 0f);
        return !facingRight;
    }
    public static void Shoot(GameObject bullet, Transform firePoint)
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }

    public static void SetAnimatorJump(Animator anim, bool isGrounder, Rigidbody2D rb)
    {
        anim.SetBool("isGround", isGrounder);
        anim.SetFloat("vSpeed", rb.velocity.y);
    }

    public static void SetAnimatorDeath(Animator anim)
    {
        anim.SetInteger(MeaningString.state, 7);
    }
}
