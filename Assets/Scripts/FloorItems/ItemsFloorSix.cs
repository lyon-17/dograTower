using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsFloorSix : MonoBehaviour
{
    //All items are going to be 0,0+base
    private static int baseY = 95;
    private static List<Item> sixFloorList = new List<Item>();

    private static void generateList()
    {
        //Keys
        sixFloorList.Add(new Item("yellowkey", 1, 8 + baseY));
        sixFloorList.Add(new Item("yellowkey", 1, 4 + baseY));
        sixFloorList.Add(new Item("yellowkey", 5, 8 + baseY));
        sixFloorList.Add(new Item("yellowkey", 13, 8 + baseY));

        sixFloorList.Add(new Item("greenkey", 9, 12 + baseY));
        sixFloorList.Add(new Item("greenkey", 9, 0 + baseY));

        sixFloorList.Add(new Item("redkey", 13, 12 + baseY));

        //doors

        sixFloorList.Add(new Item("bluedoor", 1, 10 + baseY));

        //powerups

        sixFloorList.Add(new Item("atkgem", 5, 0 + baseY));

        sixFloorList.Add(new Item("defgem", 5, 12 + baseY));

        sixFloorList.Add(new Item("mediumpotion", 5, 4 + baseY));
        sixFloorList.Add(new Item("mediumpotion", 9, 4 + baseY));
        sixFloorList.Add(new Item("mediumpotion", 9, 8 + baseY));

        sixFloorList.Add(new Item("bigpotion", 1, 0 + baseY));
        sixFloorList.Add(new Item("bigpotion", 13, 4 + baseY));

        //Enemies

        sixFloorList.Add(new Item("watcher", 1, 6 + baseY));
        sixFloorList.Add(new Item("watcher", 1, 2 + baseY));
        sixFloorList.Add(new Item("watcher", 3, 0 + baseY));
        sixFloorList.Add(new Item("watcher", 3, 4 + baseY));
        sixFloorList.Add(new Item("watcher", 3, 8 + baseY));
        sixFloorList.Add(new Item("watcher", 5, 2 + baseY));
        sixFloorList.Add(new Item("watcher", 5, 6 + baseY));
        sixFloorList.Add(new Item("watcher", 5, 10 + baseY));
        sixFloorList.Add(new Item("watcher", 7, 0 + baseY));
        sixFloorList.Add(new Item("watcher", 7, 4 + baseY));
        sixFloorList.Add(new Item("watcher", 7, 8 + baseY));
        sixFloorList.Add(new Item("watcher", 7, 12 + baseY));
        sixFloorList.Add(new Item("watcher", 9, 2 + baseY));
        sixFloorList.Add(new Item("watcher", 9, 6 + baseY));
        sixFloorList.Add(new Item("watcher", 9, 10 + baseY));
        sixFloorList.Add(new Item("watcher", 11, 0 + baseY));
        sixFloorList.Add(new Item("watcher", 11, 4 + baseY));
        sixFloorList.Add(new Item("watcher", 11, 8 + baseY));
        sixFloorList.Add(new Item("watcher", 11, 12 + baseY));
        sixFloorList.Add(new Item("watcher", 13, 2 + baseY));
        sixFloorList.Add(new Item("watcher", 13, 6 + baseY));
        sixFloorList.Add(new Item("watcher", 13, 10 + baseY));
    }

    public static List<Item> getList()
    {
        if(sixFloorList.Count == 0)
        generateList();
        return sixFloorList;
    }
}
