using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsFloorFive : MonoBehaviour
{
    //All items are going to be 0,0+base
    private static int baseY = 76;
    private static List<Item> fiveFloorList = new List<Item>();

    private static void generateList()
    {
        //Keys
        fiveFloorList.Add(new Item("yellowkey", 1, 0 + baseY));
        fiveFloorList.Add(new Item("yellowkey", 6, 3 + baseY));
        fiveFloorList.Add(new Item("yellowkey", 10, 12 + baseY));
        fiveFloorList.Add(new Item("yellowkey", 11, 12 + baseY));
        fiveFloorList.Add(new Item("yellowkey", 12, 12 + baseY));

        fiveFloorList.Add(new Item("greenkey", 7, 3 + baseY));

        fiveFloorList.Add(new Item("redkey", 0, 0 + baseY));

        //doors

        fiveFloorList.Add(new Item("yellowdoor", 2, 0 + baseY));
        fiveFloorList.Add(new Item("yellowdoor", 6, 0 + baseY));
        fiveFloorList.Add(new Item("yellowdoor", 9, 2 + baseY));
        fiveFloorList.Add(new Item("yellowdoor", 11, 2 + baseY));
        fiveFloorList.Add(new Item("yellowdoor", 13, 3 + baseY));
        fiveFloorList.Add(new Item("yellowdoor", 13, 5 + baseY));
        fiveFloorList.Add(new Item("yellowdoor", 10, 6 + baseY));
        fiveFloorList.Add(new Item("yellowdoor", 10, 8 + baseY));

        fiveFloorList.Add(new Item("greendoor", 4, 0 + baseY));
        fiveFloorList.Add(new Item("greendoor", 6, 10 + baseY));
        fiveFloorList.Add(new Item("greendoor", 9, 12 + baseY));

        fiveFloorList.Add(new Item("reddoor", 0, 3 + baseY));
        fiveFloorList.Add(new Item("reddoor", 4, 9 + baseY));
        fiveFloorList.Add(new Item("reddoor", 4, 11 + baseY));

        //powerups

        fiveFloorList.Add(new Item("atkgem", 0, 2 + baseY));

        fiveFloorList.Add(new Item("defgem", 0, 4 + baseY));

        fiveFloorList.Add(new Item("smallpotion", 12, 2 + baseY));
        fiveFloorList.Add(new Item("smallpotion", 3, 0 + baseY));
        fiveFloorList.Add(new Item("smallpotion", 5, 0 + baseY));
        fiveFloorList.Add(new Item("smallpotion", 4, 6 + baseY));

        fiveFloorList.Add(new Item("mediumpotion", 5, 6 + baseY));
        fiveFloorList.Add(new Item("mediumpotion", 6, 6 + baseY));
        fiveFloorList.Add(new Item("mediumpotion", 13, 1 + baseY));

        fiveFloorList.Add(new Item("bigpotion", 8, 12 + baseY));

        //Enemies

        fiveFloorList.Add(new Item("smallslime", 2, 5 + baseY));
        fiveFloorList.Add(new Item("smallslime", 2, 6 + baseY));
        fiveFloorList.Add(new Item("smallslime", 2, 7 + baseY));

        fiveFloorList.Add(new Item("brownslime", 5, 5 + baseY));

        fiveFloorList.Add(new Item("tealslime", 0, 5 + baseY));
        fiveFloorList.Add(new Item("tealslime", 0, 7 + baseY));
        fiveFloorList.Add(new Item("tealslime", 5, 8 + baseY));
        fiveFloorList.Add(new Item("tealslime", 8, 7 + baseY));

        fiveFloorList.Add(new Item("gelatinouscube", 11, 9 + baseY));
        fiveFloorList.Add(new Item("gelatinouscube", 13, 10 + baseY));
        fiveFloorList.Add(new Item("gelatinouscube", 13, 8 + baseY));
        fiveFloorList.Add(new Item("gelatinouscube", 13, 7 + baseY));
    }

    public static List<Item> getList()
    {
        generateList();
        return fiveFloorList;
    }
}
