using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    Formulas form = new Formulas();
    public bool isPlayer;
    private float health;
    public Animator anim;
    [Header("for not Player")]
    [Space(15)]
    #region StatPers
    public float maxHealth;
    public float armor;
    public float resistance;
    public float spike;
    public float vampirizme;
    #endregion
    [SerializeField] Image hpBar;
    private float fill = 1f;
    [Space(10)]
    [Header("Immunity")]
    #region Immunity
    public bool immunPosion;
    public bool immunFire;
    public bool immunElectric;
    #endregion
    #region Poison
    private float timerStartPoison;
    private float damagePoison;
    private float tickPoison = 0;
    private GameObject gameObjectTargetPoison;
    private Animator animTargetPoison;
    #endregion
    #region Electric
    private float timerStartElectric;
    private float valueElectric;
    private float saveArmor;
    #endregion

    private void Start()
    {
        health = maxHealth;
        timerStartElectric = 0f;
        timerStartPoison = 0f;
        Event.OnReArmor.AddListener(ReArmor);
        Event.OnReHealth.AddListener(ReHealth);
        Event.OnReResistiance.AddListener(ReResistiance);
        Event.OnReSpike.AddListener(ReSpike);
        Event.OnReVampirism.AddListener(ReVampirism);
        Event.OnStrenght.AddListener(ReStrenght);
        Event.OnDexterity.AddListener(Redexterity);
        Event.OnIntelligance.AddListener(ReIntelligance);
    }

    private void Update()
    {
        ImageFill();
    }

    private void FixedUpdate()
    {
        TimerPoison();
        TimerElectric();
    }
    #region PoisonDamage
    private void TimerPoison()
    {
        if (timerStartPoison != 0)
        {
            if (timerStartPoison > 0f)
            {
                timerStartPoison -= Time.deltaTime;
                TickPoison();
            }
            else if (timerStartPoison <= 0f)
            {
                damagePoison = 0f;
                gameObjectTargetPoison = null;
                animTargetPoison = null;
                timerStartPoison = 0;
            }
        }
    }
    private void TickPoison()
    {
        if (tickPoison >= 1)
        {
            tickPoison = 0;
            Damage(gameObjectTargetPoison, animTargetPoison, damagePoison);
        }
        else if (tickPoison < 1)
        {
            tickPoison += Time.deltaTime;
        }
    }

    public void TakeDamagePoosion(float value, float time, GameObject gameObject, Animator anim)
    {
        if (!immunPosion)
        {
            timerStartPoison = time;
            damagePoison = value;
            gameObjectTargetPoison = gameObject;
            animTargetPoison = anim;
        }
    }
    #endregion

    #region FireDamage
    public void TakeFireDamage(float fireDamage, GameObject gameObject, Animator anim)
    {
        if (!immunFire)
        {
            Damage(gameObject, anim, fireDamage);
        }
    }
    #endregion

    #region ElectricEffects
    private void TimerElectric()
    {
        if (timerStartElectric != 0)
        {
            if (timerStartElectric > 0f)
            {
                timerStartElectric -= Time.deltaTime;
            }
            else if (timerStartElectric <= 0f)
            {
                armor = saveArmor;
                timerStartElectric = 0f;
            }
        }
    }
               
    public void ElectricsEffects(float value, float timer)
    {
        if (!immunElectric)
        {
            timerStartElectric = timer;
            valueElectric = value;
            armor -= value;
        }
    }
    #endregion

    public void TakeDamage(float damage, GameObject gameObject, Animator anim)
    {
        Damage(gameObject, anim, damage);
    }

    public void HitOrDeath(GameObject gameObject, Animator anim)
    {
        if (health > 0)
        {
            //        anim.SetBool(MeaningString.hit, true);
            //       Invoke(MeaningString.notHit, 0.6f);
        }
        if (health <= 0)
        {
            Death(gameObject, anim);
        }
    }

    public void ImageFill()
    {
        hpBar.fillAmount = fill;
        fill = ((health * 100) / maxHealth) / 100;
    }

    private void Damage(GameObject gameObject, Animator anim, float damage)
    {
        damage = form.DamageForm(damage, armor);
        Debug.Log(damage);
        if (damage <= 0)
        {
            damage = 1f;
        }
        health -= damage;
        HitOrDeath(gameObject, anim);
    }

    public void Death(GameObject gameObject, Animator anim)
    {
        if (gameObject.GetComponent<Enemy>() != null)
        {
            Event.SendRemoveEnemy(gameObject.GetComponent<Enemy>().index);
            Event.SendInstIndexEnemy();
            gameObject.GetComponent<Enemy>().AddSoulsCoins();
            gameObject.GetComponent<Enemy>().DropItem();
            Event.SendAddXp(gameObject.GetComponent<Enemy>().xp);
        }
        if (gameObject.GetComponent<DamageObject>())
        {
            gameObject.GetComponent<DamageObject>().damage = 0;
        }
        gameObject.GetComponent<Collider2D>().isTrigger = true;
        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        anim.SetInteger(MeaningString.state, 7);
        Destroy(gameObject, 1f);
    }

   private void NotHit()
    {
     //   anim.SetBool(MeaningString.hit, false);
    }
    #region EventReIten

    public void ReArmor(float oldArmor, float newArmor)
    {
        if (isPlayer)
        armor = armor - oldArmor + newArmor;
    }

    public void ReHealth(float oldHealth, float newHealth)
    {
        if (isPlayer)
        {
            health = health - oldHealth + newHealth;
            maxHealth = maxHealth - oldHealth + newHealth;
        }
    }

    public void ReResistiance(float oldResist, float newResist)
    {
        if (isPlayer)
            resistance = resistance - oldResist + newResist;
    }

    public void ReSpike(float oldSpike, float newSpike)
    {
        if (isPlayer)
            spike = spike - oldSpike + newSpike;
    }

    public void ReVampirism(float oldVampirism, float newVampirism)
    {
        vampirizme = vampirizme - oldVampirism + newVampirism;
    }
    #endregion

    #region EventStat
    public void ReStrenght()
    {
        if (isPlayer)
        {
            health += form.Strenght(1);
            maxHealth += form.Strenght(1);
        }
    }

    public void Redexterity()
    {
        if (isPlayer) 
        armor += form.Dedexterity(1);
    }

    public void ReIntelligance()
    {
        if (isPlayer)
        resistance += form.Intelligance(1);
    }
    #endregion
}
