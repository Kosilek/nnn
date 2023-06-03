using UnityEngine;
using UnityEngine.UI;
public class CanvasManager : Singleton<CanvasManager>
{
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
}
