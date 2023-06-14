using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyResp : MonoBehaviour
{
    private UnityEngine.Object enemyRef;
    [SerializeField]private float timer;
    [SerializeField]private float maxTimer;
    GameObject enemyCopy;
    [SerializeField] private int lvlLocation;
    [SerializeField] private bool randMonstr;
    [SerializeField] private TypeEnemy type;
    [SerializeField] private TypeEnemySpecial typeSpecial;
    private void Start()
    {
        enemyRef = Resources.Load("Enemy");
        enemyRef.GetComponent<Enemy>().levelLocation = lvlLocation;
        enemyRef.GetComponent<Enemy>().randMonstr = randMonstr;
        enemyRef.GetComponent<Enemy>().typeEnemy = type;
        enemyRef.GetComponent<Enemy>().typeSpecial = typeSpecial;
        enemyCopy = (GameObject)Instantiate(enemyRef, transform.position, transform.rotation);
        enemyCopy.transform.position = transform.position;
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
