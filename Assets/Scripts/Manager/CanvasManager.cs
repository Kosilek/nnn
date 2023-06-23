using UnityEngine;
using UnityEngine.UI;
public class CanvasManager : Singleton<CanvasManager>
{
    //переменные панели статистики
    public static int strenght = 0;
    public static int dexterity = 0;
    public static int intelligance = 0;
    public static int freeHar = 14;
    [SerializeField] private Text strenghtPoint;
    [SerializeField] private Text dexterityPoint;
    [SerializeField] private Text intelligencePoint;
    [SerializeField] private Text lvlTxt;
    [SerializeField] private Text freeHarTxt;
    //переменные текста на канвасе
    [SerializeField] private Text soulsCoins;
    //переменные таймеров
    [SerializeField] private float maxAttackDelay;
    private float timerAttackDelay;
    public Money money;

    protected override void Awake()
    {
        money = GetComponent<Money>();
        base.Awake();
        Event.OnDispSoulCoin.AddListener(DispCoin);
        Event.OnLvlUp.AddListener(lvlUp);
    }

    public void DispCoin()
    {
        soulsCoins.text = ("Souls: " + money.countSouls.ToString());
    }

    private void Start()
    {
        strenghtPoint.text = strenght.ToString();
        dexterityPoint.text = dexterity.ToString();
        intelligencePoint.text = intelligance.ToString();
        lvlTxt.text = ("lvl " + Player.lvl.ToString());
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

    public void StopAttack()
    {
        Player.player.GetComponent<Player>().StopAttack();
    }

    #endregion

    
    public void strenghtPlus()
    {
        if(freeHar > 0)
        {
            strenght++;
            FreeHarMinus();
            strenghtPoint.text = strenght.ToString();
            Event.SendStrenght();
        }      
    }

    public void dexterityPlus()
    {
        if (freeHar > 0)
        {
            dexterity++;
            FreeHarMinus();
            dexterityPoint.text = dexterity.ToString();
            Event.SendDexterity();
        }
    }

    public void intelligancePlus()
    {
        if (freeHar > 0)
        {
            intelligance++;
            FreeHarMinus();
            intelligencePoint.text = intelligance.ToString();
            Event.SendIntelligance();
        }
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
}
