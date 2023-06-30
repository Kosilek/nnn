using UnityEngine;
using UnityEngine.UI;
public class CanvasManager : Singleton<CanvasManager>
{
    [Header("Panel Statistics")]
    public static int strenght = 0;
    public static int dexterity = 0;
    public static int intelligance = 0;
    public static int freeHar = 14;
    [SerializeField] private Text strenghtPoint;
    [SerializeField] private Text dexterityPoint;
    [SerializeField] private Text intelligencePoint;
    [SerializeField] private Text lvlTxt;
    [SerializeField] private Text freeHarTxt;
    [Header("Text Canvas")]
    [SerializeField] private Text soulsCoins;
    [Header("Timer")]
    [SerializeField] private float maxAttackDelay;
    private float timerAttackDelay;
    public Money money;

    protected override void Awake()
    {       
        base.Awake();
    }

    private void Start()
    {
        AddEvent();
        InstValues();
    }

    private void InstValues()
    {
        money = GetComponent<Money>();
        InstText();
    }

    private void InstText()
    {
        strenghtPoint.text = strenght.ToString();
        dexterityPoint.text = dexterity.ToString();
        intelligencePoint.text = intelligance.ToString();
        freeHarTxt.text = freeHar.ToString();
        lvlTxt.text = ("lvl " + Player.lvl.ToString());
    }

    private void AddEvent()
    {
        Event.OnDispSoulCoin.AddListener(DispCoin);
        Event.OnLvlUp.AddListener(lvlUp);
    }

    public void DispCoin()
    {
        soulsCoins.text = ("Souls: " + money.countSouls.ToString());
    }
 
    private void FixedUpdate()
    {
        TimerAttack();
    }
    #region Controll

    public void MoovLeft()
    {
        Player.player.GetComponent<Player>().MoovLeft();
    }

    public void MoovRight()
    {
        Player.player.GetComponent<Player>().MoovRight();
    }

    public void StopMoov()
    {
        Player.player.GetComponent<Player>().MoovStop();
    }

    public void Jump()
    {
        Player.player.GetComponent<Player>().Jump();
    }

    public void Attack()
    {
        if (timerAttackDelay == 0)
        {
            Player.player.GetComponent<Player>().Attack();
            timerAttackDelay = maxAttackDelay;
        }       
    }

    private void TimerAttack()
    {
        if (timerAttackDelay > 0)
        {
            timerAttackDelay--;
        }
    }

    #endregion 

    #region StatisticsPlayer
    public void strenghtPlus()
    {
        if(freeHar > 0)
        {
            PlusStat(ref strenght, strenghtPoint);
            Event.SendStrenght();
        }      
    }

    public void dexterityPlus()
    {
        if (freeHar > 0)
        {
            PlusStat(ref dexterity, dexterityPoint);
            Event.SendDexterity();
        }
    }

    public void intelligancePlus()
    {
        if (freeHar > 0)
        {
            PlusStat(ref intelligance, intelligencePoint);
            Event.SendIntelligance();
        }
    }

    private void PlusStat(ref int stat, Text statText)
    {
        stat++;
        Debug.Log(stat);
        FreeHarMinus();
        statText.text = stat.ToString();
    }

    private void FreeHarMinus()
    {
        freeHar--;
        freeHarTxt.text = freeHar.ToString();
    }

    public void lvlUp()
    {
        freeHar++;
        freeHarTxt.text = freeHar.ToString();
        lvlTxt.text = ("lvl " + Player.lvl.ToString());
    }
    #endregion
}
