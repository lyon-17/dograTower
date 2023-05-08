using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Contains data that will be added into the save file.
 */
[System.Serializable]
public class GameData
{
    public int playerHealth;
    public int playerAtk;
    public int playerDef;
    public int playerYellowKeys;
    public int playerGreenKeys;
    public int playerRedKeys;
    public int playerBlueKeys;
    public int playerX;
    public int playerY;
    public List<Item> itemList;

    public GameData()
    {
        playerHealth = 500;
        playerAtk = 10;
        playerDef = 5;
        playerYellowKeys = 0;
        playerGreenKeys = 0;
        playerRedKeys = 0;
        playerBlueKeys = 0;
        itemList = new List<Item>();
    }

    public void setPlayerPos(int x, int y)
    {
        playerX = x;
        playerY = y;
    }
    public int getPlayerX()
    {
        return playerX;
    }

    public int getPlayerY()
    {
        return playerY;
    }

}
