using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Shows various information to the player
//Shows the enemy information whenever mouse is over one.
public class CanvasStatsScript : MonoBehaviour
{

    public TMP_Text text_hp;
    public TMP_Text text_atk;
    public TMP_Text text_def;
    public TMP_Text text_yk;
    public TMP_Text text_gk;
    public TMP_Text text_rk;
    public TMP_Text text_bk;

    public TMP_Text text_eName;
    public TMP_Text text_hp_enemy;
    public TMP_Text text_atk_enemy;
    public TMP_Text text_def_enemy;
    public TMP_Text textdmg;

    public static CanvasStatsScript instance = null;

    private void Awake()
    {
        //Create a new instance when loaded
        if (instance == null)
            instance = this;

        //If instance is already loaded, destroy the new overlap instance
        else if (instance != null)
            Destroy(gameObject);

        //Don't destroy the gameManager and first canvas when changing scenes
        DontDestroyOnLoad(gameObject);
    }

    /**
     * Update all the stats
     */
    public void updatePlayerStats(int[] stats)
    {
        text_hp.text = ": "+stats[0].ToString();
        text_atk.text = ": " + stats[1].ToString();
        text_def.text = ": "+stats[2].ToString();
        text_yk.text = ": "+stats[3].ToString();
        text_gk.text = ": "+stats[4].ToString();
        text_rk.text = ": " + stats[5].ToString();
        text_bk.text = ": " + stats[6].ToString();
    }

    //Update the UI with a new value
    public void updateStat(string type, int number)
    {
        string text = ": "+number.ToString();
        switch (type)
        {
            case "hp":
                text_hp.text = text;
                break;
            case "atk":
                text_atk.text = text;
                break;
            case "def":
                text_def.text = text;
                break;
            case "yk":
                text_yk.text = text;
                break;
            case "gk":
                text_gk.text = text;
                break;
            case "rk":
                text_rk.text = text;
                break;
            case "bk":
                text_bk.text = text;
                break;
            case "elife":
                text_hp_enemy.text = text;
                break;
        }
    }
    //Update UI with combat (remove expected damage)
    public void updateEnemyStats(string[] enemyStats)
    {
        if (GameManager.instance != null)
            GameManager.instance.activateIcons();
        else
            TutorialScript.tutorial.activateIcons();
        text_eName.text = enemyStats[0];
        text_hp_enemy.SetText(": " + enemyStats[1]);
        text_atk_enemy.SetText(": " + enemyStats[2]);
        text_def_enemy.SetText(": " + enemyStats[3]);
        textdmg.SetText("");
    }
    //Update the enemies stats whenever there isn't a combat
    public void updateStats(string[] stats)
    {
        if (GameManager.instance != null)
            GameManager.instance.activateIcons();
        else
            TutorialScript.tutorial.activateIcons();
        text_eName.text = stats[0];
        text_hp_enemy.SetText(": " + stats[1]);
        text_atk_enemy.SetText(": " + stats[2]);
        text_def_enemy.SetText(": " + stats[3]);
        textdmg.SetText("Damage expected\n"+ stats[4]);
    }
    //Clean the enemy stat when combat finish
    public void cleanStats()
    {
        if (GameManager.instance != null)
            GameManager.instance.disactivateIcons();
        else
            TutorialScript.tutorial.disactivateIcons();
        text_eName.text = "";
        text_hp_enemy.SetText(" ");
        text_atk_enemy.SetText(" ");
        text_def_enemy.SetText(" ");
        textdmg.SetText("");
    }

    //Clean the stats from main menu
    public void cleanAllStats()
    {
        text_hp.text = "";
        text_atk.text = "";
        text_def.text = "";
        text_yk.text = "";
        text_gk.text = "";
        text_rk.text = "";
        text_bk.text = "";
        text_eName.text = "";
        text_hp_enemy.SetText(" ");
        text_atk_enemy.SetText(" ");
        text_def_enemy.SetText(" ");
    }
}
