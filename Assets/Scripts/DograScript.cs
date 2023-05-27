using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DograScript : MonoBehaviour
{
    public int baseHealth = 250;
    public int atk = 50;
    public int def = 8;
    public string dograName = "Dogra, The Abyssal Worm";

    private string getMonsterDamage()
    {
        int health = baseHealth;
        int playerAtk = GameManager.playerAtk;
        int playerDef = GameManager.playerDef;
        int damageDealt = 0;
        int times = 0;

        int playerDmg = playerAtk - def;
        int enemyDmg = atk - playerDef;
        //Enemy defense is greater than atk
        if (playerDmg <= 0)
        {
            return "Can't be hurt";
        }
        //First hit from the player, either killed or not, is removed from the formula
        health -= playerDmg;
        while (health > 0)
        {
            health -= playerDmg;
            damageDealt += enemyDmg;
            times++;
        }
        return enemyDmg + " * " + times + " = " + damageDealt.ToString();
    }
    private void OnMouseEnter()
    {
        if (!GameManager.instance.battleActive)
        {
            string[] dograInfo = { dograName, baseHealth.ToString(), atk.ToString(), def.ToString(), getMonsterDamage().ToString() };

            CanvasStatsScript.instance.updateStats(dograInfo);
        }

    }
    private void OnMouseExit()
    {
        if (!GameManager.instance.battleActive)
            CanvasStatsScript.instance.cleanStats();
    }
}
