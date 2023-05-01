using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatScript : MonoBehaviour
{
    private static float currentTime;
    public static void InitBattle(List<int> playerStats, List<int> enemyStats)
    {
        int player_health = playerStats[0];
        int player_atk = playerStats[1];
        int player_def = playerStats[2];
        int enemy_health = enemyStats[0];
        int enemy_atk = enemyStats[1];
        int enemy_def = enemyStats[2];


        while(player_health > 0 || enemy_health > 0 && Time.time > currentTime + 0.8f)
        {
            DoCombat(player_health, player_atk, player_def, enemy_health, enemy_atk, enemy_def);
            currentTime = Time.time;
        }
    }

    private static void DoCombat(int player_health, int player_atk, int player_def, int enemy_health, int enemy_atk, int enemy_def)
    {
        int playerDmg = player_atk - enemy_def;
        int enemyDmg = enemy_atk - player_def;

        enemy_health -= playerDmg;
        if(enemy_health < 0)
            return;
        player_health -= enemyDmg;

    }
}
