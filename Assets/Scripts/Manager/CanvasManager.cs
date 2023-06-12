using UnityEngine;
using UnityEngine.UI;
public class CanvasManager : Singleton<CanvasManager>
{
    //переменные панели статистики
    public static int strenght = 10;
    public static int dexterity = 8;
    public static int intelligance = 5;
    public static int freeHar;
    [SerializeField] private Text strenghtPoint;
    [SerializeField] private Text dexterityPoint;
    [SerializeField] private Text intelligencePoint;
    [SerializeField] private Text lvlTxt;
    [SerializeField] private Text freeHarTxt;
    //переменные текста на канвасе
    [SerializeField] private Text soulsCoins;
    private int countSouls;
    //переменные таймеров
    [SerializeField] private float maxAttackDelay;
    private float timerAttackDelay;

    protected override void Awake()
    {
        base.Awake();
        soulsCoins.text = ("Souls: " + countSouls.ToString());
        Event.OnScoreCoinsSouls.AddListener(AddCoinsSouls);
        Event.OnLvlUp.AddListener(lvlUp);
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

    public void AddCoinsSouls(int count)
    {
        countSouls += count;
        soulsCoins.text = ("Souls: " + countSouls.ToString());
    }
    public void strenghtPlus()
    {
        if(freeHar > 0)
        {
            strenght++;
            FreeHarMinus();
            strenghtPoint.text = strenght.ToString();
        }      
    }

    public void dexterityPlus()
    {
        if (freeHar > 0)
        {
            dexterity++;
            FreeHarMinus();
            dexterityPoint.text = dexterity.ToString();
        }
    }

    public void intelligancePlus()
    {
        if (freeHar > 0)
        {
            intelligance++;
            FreeHarMinus();
            intelligencePoint.text = intelligance.ToString();
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
