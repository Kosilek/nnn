using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public List<Enemy> enemySave = new List<Enemy>();
    public List<EnemyResp> enemyResp = new List<EnemyResp>();

    protected override void Awake()
    {
        base.Awake();
        Event.OnEnemy.AddListener(AddEnemy);
        Event.OnRemoveEnemy.AddListener(RemoveEnemy);
        Event.OnInstIndexEnemy.AddListener(InstIndexEnemy);
        Event.OnEnemyResp.AddListener(AddEnemyResp);
        Event.OnInstIndexEnemyResp.AddListener(InstIndexEnemyResp);
        Event.OnRemoveEnemyResp.AddListener(RemoveEnemyResp);
    }

    #region EnemyList
    public void AddEnemy(GameObject enemy)
    {
        enemySave.Add(enemy.GetComponent<Enemy>());
    }

    public void InstIndexEnemy()
    {
        for (int i = 0; i < enemySave.Count; i++)
        {
            enemySave[i].GetComponent<Enemy>().index = i;
        }
    }

    public void RemoveEnemy(int index)
    {
        if (index != 0)
        {
            enemySave.RemoveAt(index);
        }
        else if (index == 0)
        {
            enemySave.Clear();
        }
        
    }
    #endregion

    #region EnemyRespList
    public void AddEnemyResp(GameObject enemyR)
    {
        enemyResp.Add(enemyR.GetComponent<EnemyResp>());
    }

    public void InstIndexEnemyResp()
    {
        for (int i = 0; i < enemyResp.Count; i++)
        {
            enemyResp[i].GetComponent<EnemyResp>().index = i;
        }
    }

    public void RemoveEnemyResp(int index)
    {
        enemyResp.RemoveAt(index);
    }
    #endregion

    public void LoadData(Save.enemySaveData save, int index)
    {
        enemySave[index].randMonstr = save.randMonstr;
        enemySave[index].levelLocation = save.levelLocation;
        enemySave[index].typeEnemy = save.typeEnemy;
        enemySave[index].typeSpecial = save.typeSpecial;
        enemySave[index].index = save.index;
        enemySave[index].lvlMonstr = save.lvlMonstr;
        enemySave[index].damage = save.damage;
        enemySave[index].armor = save.armor;
        enemySave[index].maxHeathl = save.maxHeathl;
        enemySave[index].resistiance = save.resistiance;
        enemySave[index].spike = save.spike;
        enemySave[index].speed = save.speed;
        enemySave[index].vampirism = save.vampirism;
        enemySave[index].physDamage = save.physDamage;
        enemySave[index].magicDamage = save.magicDamage;
        enemySave[index].poison = save.poison;
        enemySave[index].fire = save.fire;
        enemySave[index].electric = save.electric;
        enemySave[index].valuePoison = save.valuePoison;
        enemySave[index].timePoison = save.timePoison;
        enemySave[index].fireDamage = save.fireDamage;
        enemySave[index].valueElectric = save.valueElectric;
        enemySave[index].timerElectric = save.timerElectric;
        enemySave[index].immunPosion = save.immunPosion;
        enemySave[index].immunFire = save.immunFire;
        enemySave[index].immunElectric = save.immunElectric;
        enemySave[index].countSouls = save.countSouls;
        enemySave[index].xp = save.xp;
       // enemySave[index].dropItemPre = save.dropItemPre;
       // enemySave[index].dropItem = save.dropItem;
        enemySave[index].coefA = save.coefA;
        transform.position = new Vector3(save.Position.x, save.Position.y, save.Position.z);
    }

    public void LoadDataResp(Save.enemyRespData save, int index)
    {
        enemyResp[index].index = save.index;
        enemyResp[index].maxTimer = save.maxTimer;
        enemyResp[index].lvlLocation = save.lvlLocation;
        enemyResp[index].randMonstr = save.randMonstr;
        enemyResp[index].type = save.type;
        enemyResp[index].typeSpecial = save.typeSpecial;
    }
}
