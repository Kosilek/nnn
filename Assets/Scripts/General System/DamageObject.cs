using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DamageObject : MonoBehaviour
{
    public float damage;
    public bool detroyOnDamage;
    public bool ignor;
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Health>())
        {
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
}
