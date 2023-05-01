using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Hold the unit information
public class EnemiesDataScript : MonoBehaviour
{
    //Hold all the monster stats
    private string[] monsterInfo = new string[5];

    private void Start()
    {
        if (gameObject.name.Contains("Gelatinous"))
        {
            int[] stats = EnemyStatList.enemyListing["gelatinouscube"];
            monsterInfo[0] = "Gelatinous Cube";
            monsterInfo[1] = stats[0].ToString();
            monsterInfo[2] = stats[1].ToString();
            monsterInfo[3] = stats[2].ToString();
        }
        if (gameObject.name.Contains("Terror"))
        {
            int[] stats = EnemyStatList.enemyListing["terrorbat"];
            monsterInfo[0] = "Terror Bat";
            monsterInfo[1] = stats[0].ToString();
            monsterInfo[2] = stats[1].ToString();
            monsterInfo[3] = stats[2].ToString();
        }
        if (gameObject.name.Contains("Brown"))
        {
            int[] stats = EnemyStatList.enemyListing["brownSlime"];
            monsterInfo[0] = "Brown Slime";
            monsterInfo[1] = stats[0].ToString();
            monsterInfo[2] = stats[1].ToString();
            monsterInfo[3] = stats[2].ToString();
        }
        if (gameObject.name.Contains("Small"))
        {
            int[] stats = EnemyStatList.enemyListing["smallSlime"];
            monsterInfo[0] = "Small Slime";
            monsterInfo[1] = stats[0].ToString();
            monsterInfo[2] = stats[1].ToString();
            monsterInfo[3] = stats[2].ToString();
        }
        if (gameObject.name.Contains("Teal"))
        {
            int[] stats = EnemyStatList.enemyListing["tealSlime"];
            monsterInfo[0] = "Teal Slime";
            monsterInfo[1] = stats[0].ToString();
            monsterInfo[2] = stats[1].ToString();
            monsterInfo[3] = stats[2].ToString();
        }
        if (gameObject.name.Contains("Treant"))
        {
            int[] stats = EnemyStatList.enemyListing["treant"];
            monsterInfo[0] = "Treant";
            monsterInfo[1] = stats[0].ToString();
            monsterInfo[2] = stats[1].ToString();
            monsterInfo[3] = stats[2].ToString();
        }
        if (gameObject.name.Contains("Vengeful"))
        {
            int[] stats = EnemyStatList.enemyListing["vengefulSpirit"];
            monsterInfo[0] = "Vengeful Spirit";
            monsterInfo[1] = stats[0].ToString();
            monsterInfo[2] = stats[1].ToString();
            monsterInfo[3] = stats[2].ToString();
        }
        if (gameObject.name.Contains("Watcher"))
        {
            int[] stats = EnemyStatList.enemyListing["watcher"];
            monsterInfo[0] = "Watcher";
            monsterInfo[1] = stats[0].ToString();
            monsterInfo[2] = stats[1].ToString();
            monsterInfo[3] = stats[2].ToString();
        }
        if (gameObject.name.Contains("Winged"))
        {
            int[] stats = EnemyStatList.enemyListing["wingedDemon"];
            monsterInfo[0] = "Winged Demon";
            monsterInfo[1] = stats[0].ToString();
            monsterInfo[2] = stats[1].ToString();
            monsterInfo[3] = stats[2].ToString();
        }
        if (gameObject.name.Contains("Dogra"))
        {
            int[] stats = EnemyStatList.enemyListing["Dogra"];
            monsterInfo[0] = "Dogra, The Abyssal Worm";
            monsterInfo[1] = stats[0].ToString();
            monsterInfo[2] = stats[1].ToString();
            monsterInfo[3] = stats[2].ToString();
        }
    }

    private string getMonsterDamage()
    {
        int monsterHP = int.Parse(monsterInfo[1]);
        int monsterAtk = int.Parse(monsterInfo[2]);
        int monsterDef = int.Parse(monsterInfo[3]);
        int playerAtk = GameManager.playerAtk;
        int playerDef = GameManager.playerDef;
        int damageDealt = 0;
        int times = 0;

        int playerDmg = playerAtk - monsterDef;
        int enemyDmg = monsterAtk - playerDef;
        //Enemy defense is greater than atk
        if( playerDmg <= 0)
        {
            return "Can't be hurt";
        }
        //First hit from the player, either killed or not, is removed from the formula
        monsterHP -= playerDmg;
        while (monsterHP > 0)
        {
            monsterHP -= playerDmg;
            damageDealt += enemyDmg;
            times++;
        } 
        return enemyDmg + " * "+ times + " = "+damageDealt.ToString();


    }
    private void OnMouseEnter()
    {
        if (!GameManager.instance.battleActive)
        {
            monsterInfo[4] = getMonsterDamage();
            CanvasStatsScript.instance.updateStats(monsterInfo);
        }
        
    }
    private void OnMouseExit()
    {
        if (!GameManager.instance.battleActive)
            CanvasStatsScript.instance.cleanStats();
    }
}
