using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsFloorFour : MonoBehaviour
{
    //All items are going to be 0,0+base
    private static int baseY = 57;
    private static List<Item> fourFloorList = new List<Item>();

    private static void generateList()
    {
        //Keys
        fourFloorList.Add(new Item("yellowkey", 3, 4 + baseY));
        fourFloorList.Add(new Item("yellowkey", 5, 4 + baseY));
        fourFloorList.Add(new Item("yellowkey", 7, 5 + baseY));

        fourFloorList.Add(new Item("redkey", 11, 7 + baseY));

        fourFloorList.Add(new Item("bluekey", 1, 12 + baseY));
        fourFloorList.Add(new Item("bluekey", 12, 12 + baseY));

        //doors

        fourFloorList.Add(new Item("yellowdoor", 12, 5 + baseY));

        fourFloorList.Add(new Item("greendoor", 8, 5 + baseY));
        fourFloorList.Add(new Item("greendoor", 7, 3 + baseY));

        fourFloorList.Add(new Item("reddoor", 4, 1 + baseY));
        fourFloorList.Add(new Item("reddoor", 13, 3 + baseY));

        //powerups

        fourFloorList.Add(new Item("atkgem", 4, 2 + baseY));
        fourFloorList.Add(new Item("atkgem", 13, 11 + baseY));
        fourFloorList.Add(new Item("atkgem", 13, 12 + baseY));
        fourFloorList.Add(new Item("atkgem", 13, 13 + baseY));
        fourFloorList.Add(new Item("atkgem", 12, 13 + baseY));
        fourFloorList.Add(new Item("atkgem", 12, 13 + baseY));
        fourFloorList.Add(new Item("atkgem", 11, 13 + baseY));

        fourFloorList.Add(new Item("defgem", 0, 12 + baseY));
        fourFloorList.Add(new Item("defgem", 0, 13 + baseY));
        fourFloorList.Add(new Item("defgem", 1, 13 + baseY));

        fourFloorList.Add(new Item("smallpotion", 7, 4 + baseY));
        fourFloorList.Add(new Item("smallpotion", 4, 4 + baseY));

        fourFloorList.Add(new Item("mediumpotion", 13, 0 + baseY));
        fourFloorList.Add(new Item("mediumpotion", 0, 0 + baseY));
        fourFloorList.Add(new Item("mediumpotion", 0, 8 + baseY));
        fourFloorList.Add(new Item("mediumpotion", 13, 8 + baseY));

        fourFloorList.Add(new Item("bigpotion", 2, 11 + baseY));
        fourFloorList.Add(new Item("bigpotion", 11, 11 + baseY));


        //Enemies

        fourFloorList.Add(new Item("smallslime", 7, 6 + baseY));

        fourFloorList.Add(new Item("tealslime", 13, 6 + baseY));

        fourFloorList.Add(new Item("terrorbat", 6, 0 + baseY));
        fourFloorList.Add(new Item("terrorbat", 2, 0 + baseY));

        fourFloorList.Add(new Item("gelatinouscube", 1, 4 + baseY));
        fourFloorList.Add(new Item("gelatinouscube", 4, 3 + baseY));
        fourFloorList.Add(new Item("gelatinouscube", 10, 8 + baseY));
        fourFloorList.Add(new Item("gelatinouscube", 5, 8 + baseY));

        fourFloorList.Add(new Item("vengefulspirit", 9, 10 + baseY));
        fourFloorList.Add(new Item("wingeddemon", 3, 10 + baseY));
    }

    public static List<Item> getList()
    {
        if(fourFloorList.Count == 0)
        generateList();
        return fourFloorList;
    }
}
