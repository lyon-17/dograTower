using System.Collections;
using System.Collections.Generic;

using UnityEngine;

/**
 * Contains the info that going to be saved and serialized
 */

[System.Serializable]
public class GameSave
{
    public static GameSave current;
    //Testing
    public GameData gameData;
    public List<Item> itemList;

    public GameSave()
    {
        itemList = new List<Item>();
        gameData = new GameData();
    }

}
