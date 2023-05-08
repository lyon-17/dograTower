using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsFloorEight : MonoBehaviour
{
    //All items are going to be 0,0+base
    private static int baseY = 133;
    private static List<Item> eightFloorList = new List<Item>();

    private static void generateList()
    {
        //Keys
        eightFloorList.Add(new Item("yellowkey", 7, 1 + baseY));
        eightFloorList.Add(new Item("yellowkey", 3, 4 + baseY));
        eightFloorList.Add(new Item("yellowkey", 3, 8 + baseY));
        eightFloorList.Add(new Item("yellowkey", 4, 8 + baseY));
        eightFloorList.Add(new Item("yellowkey", 4, 8 + baseY));
        eightFloorList.Add(new Item("yellowkey", 3, 13 + baseY));
        eightFloorList.Add(new Item("yellowkey", 5, 13 + baseY));
        eightFloorList.Add(new Item("yellowkey", 10, 13 + baseY));
        eightFloorList.Add(new Item("yellowkey", 9, 5 + baseY));
        eightFloorList.Add(new Item("yellowkey", 9, 4 + baseY));

        eightFloorList.Add(new Item("greenkey", 2, 6 + baseY));
        eightFloorList.Add(new Item("greenkey", 6, 13 + baseY));

        eightFloorList.Add(new Item("redkey", 8, 1 + baseY));
        eightFloorList.Add(new Item("redkey", 5, 5 + baseY));


        //doors

        eightFloorList.Add(new Item("yellowdoor", 0, 1 + baseY));
        eightFloorList.Add(new Item("yellowdoor", 6, 1 + baseY));
        eightFloorList.Add(new Item("yellowdoor", 13, 1 + baseY));
        eightFloorList.Add(new Item("yellowdoor", 13, 12 + baseY));

        //powerups

        eightFloorList.Add(new Item("smallpotion", 2, 13 + baseY));
        eightFloorList.Add(new Item("smallpotion", 9, 1 + baseY));
        eightFloorList.Add(new Item("smallpotion", 8, 13 + baseY));
        eightFloorList.Add(new Item("smallpotion", 11, 13 + baseY));

        eightFloorList.Add(new Item("mediumpotion", 7, 13 + baseY));
        eightFloorList.Add(new Item("mediumpotion", 11, 8 + baseY));
        eightFloorList.Add(new Item("mediumpotion", 10, 7 + baseY));
        eightFloorList.Add(new Item("mediumpotion", 9, 6 + baseY));
        eightFloorList.Add(new Item("mediumpotion", 4, 5 + baseY));

        eightFloorList.Add(new Item("bigpotion", 5, 7 + baseY));

        //Enemies

        eightFloorList.Add(new Item("vengefulspirit", 1, 6 + baseY));
        eightFloorList.Add(new Item("vengefulspirit", 3, 3 + baseY));
        eightFloorList.Add(new Item("vengefulspirit", 5, 1 + baseY));
        eightFloorList.Add(new Item("vengefulspirit", 4, 7 + baseY));
        eightFloorList.Add(new Item("vengefulspirit", 9, 7 + baseY));
        eightFloorList.Add(new Item("vengefulspirit", 11, 12 + baseY));

        eightFloorList.Add(new Item("wingeddemon", 3, 9 + baseY));
        eightFloorList.Add(new Item("wingeddemon", 7, 12 + baseY));
        eightFloorList.Add(new Item("wingeddemon", 11, 9 + baseY));
        eightFloorList.Add(new Item("wingeddemon", 10, 8 + baseY));
        eightFloorList.Add(new Item("wingeddemon", 10, 1 + baseY));

        eightFloorList.Add(new Item("treant", 2, 12 + baseY));
        eightFloorList.Add(new Item("treant", 4, 4 + baseY));
    }

    public static List<Item> getList()
    {
        if(eightFloorList.Count == 0)
        generateList();
        return eightFloorList;
    }
}
