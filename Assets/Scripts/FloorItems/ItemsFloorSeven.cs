using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsFloorSeven : MonoBehaviour
{
    //All items are going to be 0,0+base
    private static int baseY = 114;
    private static List<Item> sevenFloorList = new List<Item>();

    private static void generateList()
    {
        //Keys

        sevenFloorList.Add(new Item("greenkey", 4, 4 + baseY));
        sevenFloorList.Add(new Item("greenkey", 9, 9 + baseY));

        //doors

        sevenFloorList.Add(new Item("yellowdoor", 2, 3 + baseY));
        sevenFloorList.Add(new Item("yellowdoor", 4, 3 + baseY));
        sevenFloorList.Add(new Item("yellowdoor", 6, 3 + baseY));
        sevenFloorList.Add(new Item("yellowdoor", 3, 6 + baseY));
        sevenFloorList.Add(new Item("yellowdoor", 5, 6 + baseY));
        sevenFloorList.Add(new Item("yellowdoor", 8, 7 + baseY));
        sevenFloorList.Add(new Item("yellowdoor", 10, 7 + baseY));
        sevenFloorList.Add(new Item("yellowdoor", 7, 10 + baseY));
        sevenFloorList.Add(new Item("yellowdoor", 9, 10 + baseY));
        sevenFloorList.Add(new Item("yellowdoor", 11, 10 + baseY));
        sevenFloorList.Add(new Item("yellowdoor", 8, 11 + baseY));
        sevenFloorList.Add(new Item("yellowdoor", 10, 11 + baseY));

        sevenFloorList.Add(new Item("greendoor", 0, 10 + baseY));
        sevenFloorList.Add(new Item("greendoor", 0, 12 + baseY));

        sevenFloorList.Add(new Item("bluedoor", 13, 3 + baseY));

        //powerups

        sevenFloorList.Add(new Item("smallpotion", 4, 6 + baseY));
        sevenFloorList.Add(new Item("smallpotion", 9, 7 + baseY));
        sevenFloorList.Add(new Item("smallpotion", 8, 8 + baseY));
        sevenFloorList.Add(new Item("smallpotion", 10, 8 + baseY));
        sevenFloorList.Add(new Item("smallpotion", 7, 11 + baseY));
        sevenFloorList.Add(new Item("smallpotion", 11, 11 + baseY));

        sevenFloorList.Add(new Item("mediumpotion", 3, 5 + baseY));
        sevenFloorList.Add(new Item("mediumpotion", 5, 5 + baseY));
        sevenFloorList.Add(new Item("mediumpotion", 7, 9 + baseY));
        sevenFloorList.Add(new Item("mediumpotion", 9, 11 + baseY));
        sevenFloorList.Add(new Item("mediumpotion", 11, 9 + baseY));

        sevenFloorList.Add(new Item("bigpotion", 4, 2 + baseY));

        //Enemies

        sevenFloorList.Add(new Item("vengefulspirit", 9, 6 + baseY));
        sevenFloorList.Add(new Item("vengefulspirit", 8, 10 + baseY));
        sevenFloorList.Add(new Item("vengefulspirit", 10, 10 + baseY));

        sevenFloorList.Add(new Item("wingeddemon", 3, 3 + baseY));
        sevenFloorList.Add(new Item("wingeddemon", 2, 4 + baseY));
        sevenFloorList.Add(new Item("wingeddemon", 5, 3 + baseY));
        sevenFloorList.Add(new Item("wingeddemon", 6, 4 + baseY));
        sevenFloorList.Add(new Item("wingeddemon", 4, 7 + baseY));

    }

    public static List<Item> getList()
    {
        generateList();
        return sevenFloorList;
    }
}
