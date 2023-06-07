using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Animator anim;
    #region StatPers
    [SerializeField] private float damage;
    [SerializeField] private float health;
    [SerializeField] private float armor;
    [SerializeField] private float resistance;
    [SerializeField] private float spike;
    [SerializeField] private float speed;
    [SerializeField] private float vampirizme;
    #endregion
    [SerializeField] Image hpBar;
    private float fill = 1f;
    [Space(10)]
    [Header("Main Stat")]
    #region MainStat
    [SerializeField] private float baseHealth;
    [SerializeField] private float baseArmor;
    [SerializeField] private float baseResistance;
    [SerializeField] private float baseSpike;
    [SerializeField] private float baseVampirizme;
    #endregion
    [Space(5)]
    [Header("Immunity")]
    #region Immunity
    [SerializeField] private bool immunPosion;
    [SerializeField] private bool immunFire;
    [SerializeField] private bool immunElectric;
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
    #region TemporaryVariables
    private float temporaryDamage = 0;

    private float temporaryArmorHelmet = 0;
    private float temporaryArmorBib = 0;
    private float temporaryArmorGloves = 0;
    private float temporaryArmorBoots = 0;

    private float temporaryHealthHelmet = 0;
    private float temporaryHealthBib = 0;
    private float temporaryHealthGloves = 0;
    private float temporaryHealthBoots = 0;
    private float temporaryHealthAmulet = 0;
    private float temporaryHealthRing1 = 0;
    private float temporaryHealthRing2 = 0;
    private float temporaryHealthBracelete = 0;

    private float temporaryResistanceAmulet = 0;
    private float temporaryResistanceRing1 = 0;
    private float temporaryResistanceRing2 = 0;
    private float temporaryResistanceBracelete = 0;

    private float temporarySpikeHelmet = 0;
    private float temporarySpikeBib = 0;
    private float temporarySpikeGloves = 0;
    private float temporarySpikeBoots = 0;

    private float temporarySpeedBoots = 0;

    private float temporaryVampirism = 0;
    #endregion
    private void Start()
    {
        timerStartElectric = 0f;
        timerStartPoison = 0f;
        health = baseHealth;
        armor = baseArmor;
        saveArmor = armor;
        resistance = baseResistance;
        spike = baseSpike;
        vampirizme = baseVampirizme;
        Event.OnReWeapon.AddListener(ReWeapon);
        Event.OnReHelmet.AddListener(ReHelmet);
        Event.OnReBib.AddListener(ReBib);
        Event.OnReGloves.AddListener(ReGloves);
        Event.OnReBoots.AddListener(ReBoots);
        Event.OnReAmulet.AddListener(ReAmulet);
        Event.OnReRing1.AddListener(ReRing1);
        Event.OnReRing2.AddListener(ReRing2);
        Event.OnReBracelete.AddListener(ReBracelete);
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
        fill = ((health * 100) / baseHealth) / 100;
    }

    private void Damage(GameObject gameObject, Animator anim, float damage)
    {
        health -= damage;
        HitOrDeath(gameObject, anim);
    }

    public void Death(GameObject gameObject, Animator anim)
    {
        if (gameObject.GetComponent<Enemy>() != null)
        {
            gameObject.GetComponent<Enemy>().AddSoulsCoins();
            gameObject.GetComponent<Enemy>().DropItem();
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
    public void ReWeapon(float damage, float vampirism)
    {
        this.damage = this.damage + damage - temporaryDamage;
        this.vampirizme = this.vampirizme + vampirism - temporaryVampirism;
        temporaryDamage = damage;
        temporaryVampirism = vampirism;
    }

    public void ReHelmet(float armor, float health, float spike)
    {
        this.armor = this.armor + armor - temporaryArmorHelmet;
        this.baseHealth = this.baseHealth + health - temporaryHealthHelmet;
        this.spike = this.spike + spike - temporarySpikeHelmet;
        this.health = this.health + health - temporaryHealthHelmet;
        temporaryArmorHelmet = armor;
        temporaryHealthHelmet = health;
        temporarySpikeHelmet = spike;
    }

    public void ReBib(float armor, float health, float spike)
    {
        this.armor = this.armor + armor - temporaryArmorBib;
        this.baseHealth = this.baseHealth + health - temporaryHealthBib;
        this.spike = this.spike + spike - temporarySpikeBib;
        this.health = this.health + health - temporaryHealthBib;
        temporaryArmorBib = armor;
        temporaryHealthBib = health;
        temporarySpikeBib = spike;
    }

    public void ReGloves(float armor, float health, float spike)
    {
        this.armor = this.armor + armor - temporaryArmorGloves;
        this.baseHealth = this.baseHealth + health - temporaryHealthGloves;
        this.spike = this.spike + spike - temporarySpikeGloves;
        this.health = this.health + health - temporaryHealthGloves;
        temporaryArmorGloves = armor;
        temporaryHealthGloves = health;
        temporarySpikeGloves = spike;
    }

    public void ReBoots(float armor, float health, float spike, float speed)
    {
        this.armor = this.armor + armor - temporaryArmorBoots;
        this.baseHealth = this.baseHealth + health - temporaryHealthBoots;
        this.spike = this.spike + spike - temporarySpikeBoots;
        this.speed = this.speed + speed - temporarySpeedBoots;
        this.health = this.health + health - temporaryHealthBoots;
        temporaryArmorBoots = armor;
        temporaryHealthBoots = health;
        temporarySpikeBoots = spike;
        temporarySpeedBoots = speed;
    }

    public void ReAmulet(float health, float resistance)
    {
        this.baseHealth = this.baseHealth + health - temporaryHealthAmulet;
        this.resistance = this.resistance + resistance - temporaryResistanceAmulet;
        this.health = this.health + health - temporaryHealthAmulet;
        temporaryHealthAmulet = health;
        temporaryResistanceAmulet = resistance;
    }

    public void ReRing1(float health, float resistance)
    {
        this.baseHealth = this.baseHealth + health - temporaryHealthRing1;
        this.resistance = this.resistance + resistance - temporaryResistanceRing1;
        this.health = this.health + health - temporaryHealthRing1;
        temporaryHealthRing1 = health;
        temporaryResistanceRing1 = resistance;
    }

    public void ReRing2(float health, float resistance)
    {
        this.baseHealth = this.baseHealth + health - temporaryHealthRing2;
        this.resistance = this.resistance + resistance - temporaryResistanceRing2;
        this.health = this.health + health - temporaryHealthRing2;
        temporaryHealthRing2 = health;
        temporaryResistanceRing2 = resistance;
    }

    public void ReBracelete(float health, float resistance)
    {
        this.baseHealth = this.baseHealth + health - temporaryHealthBracelete;
        this.resistance = this.resistance + resistance - temporaryResistanceBracelete;
        this.health = this.health + health - temporaryHealthBracelete;
        temporaryHealthBracelete = health;
        temporaryResistanceBracelete = resistance;
    }
    #endregion
}
