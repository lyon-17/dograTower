using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Holds the entire enemy stats
public class EnemyStatList : MonoBehaviour
{
    public static Dictionary<string, int[]> enemyListing = new Dictionary<string, int[]>();

    public static void loadDictionary()
    {
        /**
         * first value = health
         * second value = attack
         * third value = defense
         */

        //Tutorial stats
        enemyListing.Add("terrorbattut", new int[3] { 20, 10, 10 });
        enemyListing.Add("gelatinouscubetut", new int[3] { 60, 10, 10 });
        enemyListing.Add("treanttut", new int[3] { 120, 10, 10 });
        //Base game stats
        enemyListing.Add("smallSlime", new int[3] { 60, 10, 1 });
        enemyListing.Add("terrorbat",new int[3] {40,20,2});
        enemyListing.Add("tealSlime", new int[3] { 60, 20, 5 });
        enemyListing.Add("brownSlime", new int[3] { 30, 25, 10 });
        enemyListing.Add("gelatinouscube", new int[3] { 80, 18, 6 });
        enemyListing.Add("watcher", new int[3] { 30, 25, 15 });
        enemyListing.Add("treant", new int[3] { 100, 30, 10 });
        enemyListing.Add("wingedDemon", new int[3] { 110, 35, 7 });
        enemyListing.Add("vengefulSpirit", new int[3] { 100, 40, 0 });
        enemyListing.Add("Dogra", new int[3] { 250, 50, 8 });
    }
}
