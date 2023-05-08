using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsFloorTwo : MonoBehaviour
{
    //All items are going to be 0,0+base
    private static int baseY = 19;
    private static List<Item> fiveFloorList = new List<Item>();

    private static void generateList()
    {
        //Keys
        fiveFloorList.Add(new Item("yellowkey", 10, 0+baseY));
        fiveFloorList.Add(new Item("yellowkey", 10, 1+ baseY));
        fiveFloorList.Add(new Item("yellowkey", 6, 12+ baseY));

        fiveFloorList.Add(new Item("greenkey", 13, 0 + baseY));
        fiveFloorList.Add(new Item("greenkey", 7, 12 + baseY));

        fiveFloorList.Add(new Item("redkey", 4, 13 + baseY));

        //doors

        fiveFloorList.Add(new Item("yellowdoor", 0, 7 + baseY));
        fiveFloorList.Add(new Item("yellowdoor", 0, 9 + baseY));
        fiveFloorList.Add(new Item("yellowdoor", 0, 11 + baseY));
        fiveFloorList.Add(new Item("yellowdoor", 2, 7 + baseY));
        fiveFloorList.Add(new Item("yellowdoor", 2, 9 + baseY));
        fiveFloorList.Add(new Item("yellowdoor", 2, 11 + baseY));
        fiveFloorList.Add(new Item("yellowdoor", 4, 7 + baseY));
        fiveFloorList.Add(new Item("yellowdoor", 4, 9 + baseY));
        fiveFloorList.Add(new Item("yellowdoor", 12, 7 + baseY));
        fiveFloorList.Add(new Item("yellowdoor", 12, 10 + baseY));

        fiveFloorList.Add(new Item("greendoor", 3, 3 + baseY));
        fiveFloorList.Add(new Item("greendoor", 7, 5 + baseY));
        fiveFloorList.Add(new Item("greendoor", 4, 11 + baseY));

        fiveFloorList.Add(new Item("reddoor", 10, 7 + baseY));

        //powerups

        fiveFloorList.Add(new Item("atkgem", 2, 12 + baseY));
        fiveFloorList.Add(new Item("atkgem", 2, 13 + baseY));
        fiveFloorList.Add(new Item("atkgem", 4, 12 + baseY));
        fiveFloorList.Add(new Item("atkgem", 12, 13 + baseY));

        fiveFloorList.Add(new Item("defgem", 6, 11 + baseY));
        fiveFloorList.Add(new Item("defgem", 2, 2 + baseY));

        fiveFloorList.Add(new Item("smallpotion", 2, 4 + baseY));
        fiveFloorList.Add(new Item("smallpotion", 0, 8 + baseY));
        fiveFloorList.Add(new Item("smallpotion", 7, 7 + baseY));
        fiveFloorList.Add(new Item("smallpotion", 7, 9 + baseY));
        fiveFloorList.Add(new Item("smallpotion", 6, 8 + baseY));
        fiveFloorList.Add(new Item("smallpotion", 8, 8 + baseY));
        fiveFloorList.Add(new Item("smallpotion", 13, 13 + baseY));

        fiveFloorList.Add(new Item("mediumpotion", 2, 3 + baseY));
        fiveFloorList.Add(new Item("mediumpotion", 0, 10 + baseY));

        fiveFloorList.Add(new Item("bigpotion", 6, 13 + baseY));
        fiveFloorList.Add(new Item("bigpotion", 0, 12 + baseY));

        //Enemies

        fiveFloorList.Add(new Item("smallslime", 0, 1 + baseY));
        fiveFloorList.Add(new Item("smallslime", 3, 0 + baseY));
        fiveFloorList.Add(new Item("smallslime", 4, 2 + baseY));
        fiveFloorList.Add(new Item("smallslime", 10, 2 + baseY));
        fiveFloorList.Add(new Item("smallslime", 10, 3 + baseY));
        fiveFloorList.Add(new Item("smallslime", 13, 2 + baseY));
        fiveFloorList.Add(new Item("smallslime", 13, 3 + baseY));
        fiveFloorList.Add(new Item("smallslime", 2, 8 + baseY));

        fiveFloorList.Add(new Item("brownslime", 1, 4 + baseY));
        fiveFloorList.Add(new Item("brownslime", 10, 10 + baseY));
        fiveFloorList.Add(new Item("brownslime", 13, 10 + baseY));

        fiveFloorList.Add(new Item("tealslime", 13, 7 + baseY));

        fiveFloorList.Add(new Item("terrorbat", 4, 8 + baseY));

        fiveFloorList.Add(new Item("gelatinouscube", 4, 10 + baseY));

        fiveFloorList.Add(new Item("treant", 2, 10 + baseY));

    }

    public static List<Item> getList()
    {
        generateList();
        return fiveFloorList;
    }
}
