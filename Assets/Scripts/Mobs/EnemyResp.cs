using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyResp : MonoBehaviour
{
    private UnityEngine.Object enemyRef;
    public int index;
    [SerializeField]private float timer;
    public float maxTimer;
    private GameObject enemyCopy;
    public int lvlLocation;
    public bool randMonstr;
    public TypeEnemy type;
    public TypeEnemySpecial typeSpecial;
    private void Start()
    {
        Event.SendEnemyResp(gameObject);
        Event.SendInstIndexEnemyResp();
        NameTypeEnemy();
        enemyRef.GetComponent<Enemy>().levelLocation = lvlLocation;
        enemyRef.GetComponent<Enemy>().randMonstr = randMonstr;
        enemyRef.GetComponent<Enemy>().typeEnemy = type;
        enemyRef.GetComponent<Enemy>().typeSpecial = typeSpecial;
        enemyCopy = (GameObject)Instantiate(enemyRef, transform.position, transform.rotation);
        enemyCopy.transform.position = transform.position;
    }

    private void NameTypeEnemy()
    {
        switch(typeSpecial)
        {
            case TypeEnemySpecial.normal:
                enemyRef = Resources.Load(MeaningString.enemy);
                break;
            case TypeEnemySpecial.knight:
                enemyRef = Resources.Load(MeaningString.enemyKnight);
                break;
            case TypeEnemySpecial.magic:
                enemyRef = Resources.Load(MeaningString.enemyMagic);
                break;
            case TypeEnemySpecial.sniper:
                enemyRef = Resources.Load(MeaningString.enemySniper);
                break;
            case TypeEnemySpecial.vampir:
                enemyRef = Resources.Load(MeaningString.enemyVampire);
                break;
        }
    }

    private void Update()
    {
        if (enemyCopy == null)
        {
            TimeResp();
        }
    }

    private void TimeResp()
    {
        if (timer <= 0)
        {
            timer = maxTimer;
            enemyCopy = (GameObject)Instantiate(enemyRef, transform.position, transform.rotation);
        } else if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
}
