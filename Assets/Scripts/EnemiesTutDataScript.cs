using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesTutDataScript : MonoBehaviour
{
    //Hold all the tutorial monster stats
    private string[] monsterInfo = new string[5];

    private void Start()
    {
        if (gameObject.name.Contains("Gelatinous"))
        {
            monsterInfo[0] = "weak gelatinous cube";
            monsterInfo[1] = "30";
            monsterInfo[2] = "20";
            monsterInfo[3] = "3";
        }
        if (gameObject.name.Contains("Terror"))
        {
            monsterInfo[0] = "weak terror bat";
            monsterInfo[1] = "10";
            monsterInfo[2] = "5";
            monsterInfo[3] = "5";
        }
        if (gameObject.name.Contains("Treant"))
        {
            monsterInfo[0] = "weak treant";
            monsterInfo[1] = "30";
            monsterInfo[2] = "30";
            monsterInfo[3] = "6";
        }
    }

    private string getMonsterDamage()
    {
        int monsterHP = int.Parse(monsterInfo[1]);
        int monsterAtk = int.Parse(monsterInfo[2]);
        int monsterDef = int.Parse(monsterInfo[3]);
        int playerAtk = TutorialScript.tutorial.playerAtk;
        int playerDef = TutorialScript.tutorial.playerDef;
        int damageDealt = 0;
        int times = 0;

        int playerDmg = playerAtk - monsterDef;
        int enemyDmg = monsterAtk - playerDef;
        monsterHP -= playerDmg;
        while (monsterHP > 0)
        {
            monsterHP -= playerDmg;
            damageDealt += enemyDmg;
            times++;
        }
        return enemyDmg + " * " + times + " = " + damageDealt.ToString();


    }
    private void OnMouseEnter()
    {
        if (!TutorialScript.tutorial.battleActive)
        {
            monsterInfo[4] = getMonsterDamage();
            CanvasStatsScript.instance.updateStats(monsterInfo);
        }

    }
    private void OnMouseExit()
    {
        if (!TutorialScript.tutorial.battleActive)
            CanvasStatsScript.instance.cleanStats();
    }
}
