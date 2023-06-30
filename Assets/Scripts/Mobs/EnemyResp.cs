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
        InvokeEvent();
        NameTypeEnemy();
        InstValues();
    }

    private void InstValues()
    {
        enemyRef.GetComponent<Enemy>().levelLocation = lvlLocation;
        enemyRef.GetComponent<Enemy>().randMonstr = randMonstr;
        enemyRef.GetComponent<Enemy>().typeEnemy = type;
        enemyRef.GetComponent<Enemy>().typeSpecial = typeSpecial;
        enemyCopy = (GameObject)Instantiate(enemyRef, transform.position, transform.rotation);
        enemyCopy.transform.position = transform.position;
    }

    private void InvokeEvent()
    {
        Event.SendEnemyResp(gameObject);
        Event.SendInstIndexEnemyResp();
    }

    private void NameTypeEnemy()
    {
        switch(typeSpecial)
        {
            case TypeEnemySpecial.normal:
                LoadEnemy(MeaningString.enemy);
                break;
            case TypeEnemySpecial.knight:
                LoadEnemy(MeaningString.enemyKnight);
                break;
            case TypeEnemySpecial.magic:
                LoadEnemy(MeaningString.enemyMagic);
                break;
            case TypeEnemySpecial.sniper:
                LoadEnemy(MeaningString.enemySniper);
                break;
            case TypeEnemySpecial.vampir:
                LoadEnemy(MeaningString.enemyVampire);
                break;
        }
    }

    private void LoadEnemy(string nameEnemy)
    {
        enemyRef = Resources.Load(nameEnemy);
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
