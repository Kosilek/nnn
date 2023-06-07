using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DamageObject : MonoBehaviour
{
    [SerializeField] private float baseDamage;
    public float damage;// potom skrit
    [Space(5)]
    [Header("Player?")]
    #region Player?
    [SerializeField] private bool isPlayer;
    #endregion
    [Space(5)]
    [Header("Type Damage")]
    #region TypeDamge
    [SerializeField] private bool physical;
    [SerializeField] private bool magic;
    #endregion
    [Space(5)]
    [Header("typeMagic")]
    #region TypeMagic
    [SerializeField] private bool poison;
    [SerializeField] private bool fire;
    [SerializeField] private bool electric;
    [Space (10)]
    [SerializeField] private float valuePoison;
    [SerializeField] private float timePoison;
    [Space (10)]
    [SerializeField] private float fireDamage;
    [Space (10)]
    [SerializeField] private float valueElectric;
    [SerializeField] private float timerElectric;
    #endregion
    public bool detroyOnDamage;
    public bool ignor;

    private void Awake()
    {
       // Event.OnReDamage.AddListener(ReDamage);
    }

    private void Start()
    {
        damage = baseDamage;
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
        if (other.GetComponent<Health>())
        {
            SwitchTypeMagic(poison, fire, electric, other);
            other.GetComponent<Health>().TakeDamage(damage, other.gameObject, other.gameObject.GetComponent<Health>().anim);
            Debug.Log("Attack");
        }
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
        if (collision.gameObject.GetComponent<Enemy>())
        {
            if (ignor)
            {
                gameObject.GetComponent<Collider2D>().isTrigger = true;
            }
        }
        if (collision.gameObject.GetComponent<Health>() && !collision.gameObject.GetComponent<Enemy>())
        {
            SwitchTypeMagic(poison, fire, electric, collision);
            collision.gameObject.GetComponent<Health>().TakeDamage(damage, collision.gameObject, collision.gameObject.GetComponent<Health>().anim);
            Debug.Log("Attack");
        }
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
        if (collision.gameObject.GetComponent<Enemy>())
        {
            if (ignor)
            {
                gameObject.GetComponent<Collider2D>().isTrigger = false;
            }
        }
    }
    #endregion
    /* public void ReDamage(float reDamage)
    {
        damage = baseDamage + reDamage;
    }*/
}
