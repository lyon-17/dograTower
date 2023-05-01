using System.Collections;
//Use generic lists
using System.Collections.Generic;
//write/read files
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoad 
{
    //public static List<FloorScript> savedGames = new List<FloorScript>();
    public static List<GameSave> savedGames = new List<GameSave>();

    public static void Save()
    {
        //current game is saved into the list. Is going to be serialized
        GameManager.updGameData();
        savedGames.Add(GameSave.current);
        //Handle the serialization work
        BinaryFormatter bf = new BinaryFormatter();
        //Open the stream and writes into the path
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, savedGames);
        string json = JsonUtility.ToJson(GameSave.current);
        Debug.Log("Saving as JSON: " + json);
        file.Close();
        GameManager.refreshData();
    }

    public static void Load()
    {
        //Check the file exist
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            //Handle the serialization
            BinaryFormatter bf = new BinaryFormatter();
            //Get the file open
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            //Deserialize the file (into binary) and is casted to the list
            SaveLoad.savedGames = (List<GameSave>)bf.Deserialize(file);
            file.Close();
        }
    }
}
