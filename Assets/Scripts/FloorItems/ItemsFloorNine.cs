using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsFloorNine : MonoBehaviour
{
    //All items are going to be 0,0+base
    private static int baseY = 152;
    private static List<Item> nineFloorList = new List<Item>();

    private static void generateList()
    {
        //Keys
        nineFloorList.Add(new Item("yellowkey", 2, 0 + baseY));
        nineFloorList.Add(new Item("yellowkey", 5, 2 + baseY));
        nineFloorList.Add(new Item("yellowkey", 13, 8 + baseY));

        nineFloorList.Add(new Item("greenkey", 13, 6 + baseY));

        nineFloorList.Add(new Item("redkey", 2, 11 + baseY));
        nineFloorList.Add(new Item("redkey", 9, 4 + baseY));

        nineFloorList.Add(new Item("bluekey", 0, 13 + baseY));
        nineFloorList.Add(new Item("bluekey", 9, 8 + baseY));
        nineFloorList.Add(new Item("bluekey", 11, 0 + baseY));

        //doors

        nineFloorList.Add(new Item("yellowdoor", 0, 6 + baseY));
        nineFloorList.Add(new Item("yellowdoor", 0, 8 + baseY));
        nineFloorList.Add(new Item("yellowdoor", 0, 10 + baseY));
        nineFloorList.Add(new Item("yellowdoor", 0, 12 + baseY));
        nineFloorList.Add(new Item("yellowdoor", 2, 6 + baseY));
        nineFloorList.Add(new Item("yellowdoor", 2, 8 + baseY));
        nineFloorList.Add(new Item("yellowdoor", 2, 10 + baseY));
        nineFloorList.Add(new Item("yellowdoor", 4, 6 + baseY));
        nineFloorList.Add(new Item("yellowdoor", 4, 8 + baseY));

        nineFloorList.Add(new Item("greendoor", 9, 7 + baseY));
        nineFloorList.Add(new Item("greendoor", 11, 7 + baseY));
        nineFloorList.Add(new Item("greendoor", 11, 9 + baseY));


        nineFloorList.Add(new Item("reddoor", 9, 0 + baseY));
        nineFloorList.Add(new Item("reddoor", 8, 2 + baseY));
        nineFloorList.Add(new Item("reddoor", 10, 2 + baseY));

        nineFloorList.Add(new Item("bluedoor", 7, 6 + baseY));
        nineFloorList.Add(new Item("bluedoor", 7, 7 + baseY));
        nineFloorList.Add(new Item("bluedoor", 7, 8 + baseY));

        //powerups

        nineFloorList.Add(new Item("smallpotion", 9, 2 + baseY));
        nineFloorList.Add(new Item("smallpotion", 11, 8 + baseY));


        nineFloorList.Add(new Item("mediumpotion", 3, 2 + baseY));

        nineFloorList.Add(new Item("bigpotion", 5, 0 + baseY));
        nineFloorList.Add(new Item("bigpotion", 4, 9 + baseY));

        //Enemies

        nineFloorList.Add(new Item("vengefulspirit", 2, 2 + baseY));

        nineFloorList.Add(new Item("wingeddemon", 13, 9 + baseY));
        nineFloorList.Add(new Item("wingeddemon", 13, 7 + baseY));
        nineFloorList.Add(new Item("wingeddemon", 5, 1 + baseY));

        nineFloorList.Add(new Item("treant", 13, 4 + baseY));
        nineFloorList.Add(new Item("treant", 12, 4 + baseY));
        nineFloorList.Add(new Item("treant", 11, 4 + baseY));
        nineFloorList.Add(new Item("treant", 3, 0 + baseY));
        nineFloorList.Add(new Item("treant", 4, 0 + baseY));
        nineFloorList.Add(new Item("treant", 4, 2 + baseY));

    }

    public static List<Item> getList()
    {
        generateList();
        return nineFloorList;
    }
}
