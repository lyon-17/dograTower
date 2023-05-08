using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsFloorThree : MonoBehaviour
{
    //All items are going to be 0,0+base
    private static int baseY = 38;
    private static List<Item> threeFloorList = new List<Item>();

    private static void generateList()
    {
        //Keys
        threeFloorList.Add(new Item("yellowkey", 1, 3 + baseY));
        threeFloorList.Add(new Item("yellowkey", 7, 12 + baseY));
        threeFloorList.Add(new Item("yellowkey", 10, 12 + baseY));
        threeFloorList.Add(new Item("yellowkey", 10, 13 + baseY));
        threeFloorList.Add(new Item("yellowkey", 13, 12 + baseY));
        threeFloorList.Add(new Item("yellowkey", 13, 13 + baseY));

        threeFloorList.Add(new Item("redkey", 5, 3 + baseY));

        //doors

        threeFloorList.Add(new Item("yellowdoor", 6, 3 + baseY));
        threeFloorList.Add(new Item("yellowdoor", 1, 6 + baseY));
        threeFloorList.Add(new Item("yellowdoor", 1, 8 + baseY));
        threeFloorList.Add(new Item("yellowdoor", 3, 8 + baseY));
        threeFloorList.Add(new Item("yellowdoor", 5, 8 + baseY));

        threeFloorList.Add(new Item("reddoor", 10, 0 + baseY));
        threeFloorList.Add(new Item("reddoor", 1, 10 + baseY));

        //powerups

        threeFloorList.Add(new Item("atkgem", 9, 0 + baseY));
        threeFloorList.Add(new Item("atkgem", 0, 8 + baseY));

        threeFloorList.Add(new Item("defgem", 11, 0 + baseY));
        threeFloorList.Add(new Item("defgem", 0, 5 + baseY));

        threeFloorList.Add(new Item("smallpotion", 0, 2 + baseY));
        threeFloorList.Add(new Item("smallpotion", 9, 3 + baseY));
        threeFloorList.Add(new Item("smallpotion", 11, 3 + baseY));
        threeFloorList.Add(new Item("smallpotion", 4, 6 + baseY));
        threeFloorList.Add(new Item("smallpotion", 11, 8 + baseY));
        threeFloorList.Add(new Item("smallpotion", 12, 8 + baseY));

        threeFloorList.Add(new Item("mediumpotion", 4, 3 + baseY));
        threeFloorList.Add(new Item("mediumpotion", 0, 6 + baseY));

        //Enemies

        threeFloorList.Add(new Item("smallslime", 3, 0 + baseY));
        threeFloorList.Add(new Item("smallslime", 1, 0 + baseY));
        threeFloorList.Add(new Item("smallslime", 0, 1 + baseY));
        threeFloorList.Add(new Item("smallslime", 13, 11 + baseY));

        threeFloorList.Add(new Item("brownslime", 7, 2 + baseY));
        threeFloorList.Add(new Item("brownslime", 13, 2 + baseY));

        threeFloorList.Add(new Item("tealslime", 5, 2 + baseY));

        threeFloorList.Add(new Item("terrorbat", 10, 10 + baseY));
        threeFloorList.Add(new Item("terrorbat", 6, 5 + baseY));
        threeFloorList.Add(new Item("terrorbat", 7, 6 + baseY));
        threeFloorList.Add(new Item("terrorbat", 8, 5 + baseY));

        threeFloorList.Add(new Item("watcher", 4, 11 + baseY));
        threeFloorList.Add(new Item("watcher", 6, 11 + baseY));
    }

    public static List<Item> getList()
    {
        if(threeFloorList.Count == 0)
        generateList();
        return threeFloorList;
    }
}
