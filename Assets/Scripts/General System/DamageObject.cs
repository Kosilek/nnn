using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DamageObject : MonoBehaviour
{
    public float damage;
    [Space(5)]
    [Header("Player?")]
    #region Player?
    [SerializeField] private bool isPlayer;
    #endregion
    [Space(5)]
    [Header("Type Damage")]
    [Space(5)]
    [Header("typeMagic")]
    #region TypeMagic
    public bool poison;
    public bool fire;
    public bool electric;
    [Space (10)]
    public float valuePoison;
    public float timePoison;
    [Space (10)]
    public float fireDamage;
    [Space (10)]
    public float valueElectric;
    public float timerElectric;
    #endregion
    public bool detroyOnDamage;
    public bool ignor;
    [Tooltip("Vampirism = true")]
    [SerializeField] private GameObject mob;
    [Tooltip("Attack with sword")]
    [SerializeField] private bool triggerDamage;

    private void Start()
    {
        Event.OnReDamage.AddListener(ReDamage);
    }

    private void SwitchTypeMagic(bool poison, bool fire, bool electric, Collider2D other)
    {
        if (poison)
        {
            other.GetComponent<Health>().TakeDamagePoosion(valuePoison, timePoison, other.gameObject, other.GetComponent<Health>().anim);
        }

        if (fire)
        {
            other.GetComponent<Health>().TakeFireDamage(fireDamage, other.gameObject, other.GetComponent<Health>().anim);
        }

        if (electric)
        {
            other.GetComponent<Health>().ElectricsEffects(valueElectric, timerElectric);
        }
    }

    private void SwitchTypeMagic(bool poison, bool fire, bool electric, Collision2D other)
    {
        if (poison)
        {
            other.gameObject.GetComponent<Health>().TakeDamagePoosion(valuePoison, timePoison, other.gameObject, other.gameObject.GetComponent<Health>().anim);
        }

        if (fire)
        {
            other.gameObject.GetComponent<Health>().TakeFireDamage(fireDamage, other.gameObject, other.gameObject.GetComponent<Health>().anim);
        }

        if (electric)
        {
            other.gameObject.GetComponent<Health>().ElectricsEffects(valueElectric, timerElectric);
        }
    }

    #region ActionDamage
    private void OnTriggerEnter2D(Collider2D other)
    {
        Damage(other);
        DestroyObject(other);
    }

    private void Damage(Collider2D other)
    {
        if (other.GetComponent<Health>())
        {
            if (poison != false || fire != false || electric != false)
            {
                SwitchTypeMagic(poison, fire, electric, other);
            }
            if (damage >= 0)
            {
                if (mob != null)
                {
                    if (mob.GetComponent<Health>().vampirizme > 0f)
                    {
                        mob.GetComponent<Health>().Vampirism(damage);
                    }
                }
                other.GetComponent<Health>().TakeDamage(damage, other.gameObject, other.gameObject.GetComponent<Health>().anim);
            }
        }
    }

    private void DestroyObject(Collider2D other)
    {
        if (other.GetComponent<Collider2D>() != null)
        {
            if (detroyOnDamage)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IgnorObject(collision);
        Damage(collision);
        DestroyObject(collision);
    }

    private void Damage(Collision2D collision)
    {
        if (triggerDamage == true)
        {
            return;
        }
        else if (triggerDamage == false)
        {
            if (collision.gameObject.GetComponent<Health>() && !collision.gameObject.GetComponent<Enemy>())
            {
                SwitchTypeMagic(poison, fire, electric, collision);
                collision.gameObject.GetComponent<Health>().TakeDamage(damage, collision.gameObject, collision.gameObject.GetComponent<Health>().anim);
            }
        }
    }

    private void IgnorObject(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            if (ignor)
            {
                gameObject.GetComponent<Collider2D>().isTrigger = true;
            }
        }
    }

    private void DestroyObject(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Collider2D>() != null)
        {
            if (detroyOnDamage)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IgnorObject(collision);
    }

    private void IgnorObject(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>())
        {
            if (ignor)
            {
                gameObject.GetComponent<Collider2D>().isTrigger = false;
            }
        }
    }

    #endregion
    public void ReDamage(float oldDamage, float newDamage)
    {
        if (isPlayer)
            damage = damage + oldDamage - newDamage;
    }
}
