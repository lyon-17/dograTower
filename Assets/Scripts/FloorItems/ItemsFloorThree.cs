using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsFloorThree : MonoBehaviour
{
    //All items are going to be 0,0+base
    private static int baseY = 38;
    private static List<Item> fiveFloorList = new List<Item>();

    private static void generateList()
    {
        //Keys
        fiveFloorList.Add(new Item("yellowkey", 0, 3 + baseY));
        fiveFloorList.Add(new Item("yellowkey", 7, 12 + baseY));
        fiveFloorList.Add(new Item("yellowkey", 10, 12 + baseY));
        fiveFloorList.Add(new Item("yellowkey", 10, 13 + baseY));
        fiveFloorList.Add(new Item("yellowkey", 13, 12 + baseY));
        fiveFloorList.Add(new Item("yellowkey", 13, 13 + baseY));

        fiveFloorList.Add(new Item("redkey", 5, 3 + baseY));

        //doors

        fiveFloorList.Add(new Item("yellowdoor", 6, 3 + baseY));
        fiveFloorList.Add(new Item("yellowdoor", 1, 6 + baseY));
        fiveFloorList.Add(new Item("yellowdoor", 1, 8 + baseY));
        fiveFloorList.Add(new Item("yellowdoor", 3, 8 + baseY));
        fiveFloorList.Add(new Item("yellowdoor", 5, 8 + baseY));

        fiveFloorList.Add(new Item("reddoor", 10, 0 + baseY));
        fiveFloorList.Add(new Item("reddoor", 1, 10 + baseY));

        //powerups

        fiveFloorList.Add(new Item("atkgem", 9, 0 + baseY));
        fiveFloorList.Add(new Item("atkgem", 0, 8 + baseY));

        fiveFloorList.Add(new Item("defgem", 11, 0 + baseY));
        fiveFloorList.Add(new Item("defgem", 0, 5 + baseY));

        fiveFloorList.Add(new Item("smallpotion", 0, 2 + baseY));
        fiveFloorList.Add(new Item("smallpotion", 9, 3 + baseY));
        fiveFloorList.Add(new Item("smallpotion", 11, 3 + baseY));
        fiveFloorList.Add(new Item("smallpotion", 4, 6 + baseY));
        fiveFloorList.Add(new Item("smallpotion", 11, 8 + baseY));
        fiveFloorList.Add(new Item("smallpotion", 12, 8 + baseY));

        fiveFloorList.Add(new Item("mediumpotion", 4, 3 + baseY));
        fiveFloorList.Add(new Item("mediumpotion", 0, 6 + baseY));

        //Enemies

        fiveFloorList.Add(new Item("smallslime", 3, 0 + baseY));
        fiveFloorList.Add(new Item("smallslime", 1, 0 + baseY));
        fiveFloorList.Add(new Item("smallslime", 0, 1 + baseY));
        fiveFloorList.Add(new Item("smallslime", 13, 11 + baseY));

        fiveFloorList.Add(new Item("brownslime", 7, 2 + baseY));
        fiveFloorList.Add(new Item("brownslime", 13, 2 + baseY));

        fiveFloorList.Add(new Item("tealslime", 5, 2 + baseY));
        fiveFloorList.Add(new Item("tealslime", 13, 10 + baseY));


        fiveFloorList.Add(new Item("terrorbat", 10, 10 + baseY));
        fiveFloorList.Add(new Item("terrorbat", 6, 5 + baseY));
        fiveFloorList.Add(new Item("terrorbat", 7, 6 + baseY));
        fiveFloorList.Add(new Item("terrorbat", 8, 5 + baseY));

        fiveFloorList.Add(new Item("watcher", 4, 11 + baseY));
        fiveFloorList.Add(new Item("watcher", 6, 11 + baseY));
    }

    public static List<Item> getList()
    {
        generateList();
        return fiveFloorList;
    }
}
