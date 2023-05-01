using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsFloorTen : MonoBehaviour
{
    //All items are going to be 0,0+base
    private static int baseY = 171;
    private static List<Item> tenFloorList = new List<Item>();

    private static void generateList()
    {
        //Keys

        tenFloorList.Add(new Item("bluekey", 4, 10 + baseY));
        tenFloorList.Add(new Item("bluekey", 10, 10 + baseY));

        //doors

        tenFloorList.Add(new Item("bluedoor", 7, 5 + baseY));
        tenFloorList.Add(new Item("bluedoor", 7, 7 + baseY));

        //powerups

        tenFloorList.Add(new Item("bigpotion", 4, 9 + baseY));
        tenFloorList.Add(new Item("bigpotion", 10, 9 + baseY));

        //Enemies

        tenFloorList.Add(new Item("vengefulspirit", 4, 6 + baseY));
        tenFloorList.Add(new Item("vengefulspirit", 4, 7 + baseY));
        tenFloorList.Add(new Item("vengefulspirit", 4, 8 + baseY));

        tenFloorList.Add(new Item("wingeddemon", 10, 6 + baseY));
        tenFloorList.Add(new Item("wingeddemon", 10, 7 + baseY));
        tenFloorList.Add(new Item("wingeddemon", 10, 8 + baseY));

        tenFloorList.Add(new Item("Dogra", 7, 9 + baseY));
    }

    public static List<Item> getList()
    {
        generateList();
        return tenFloorList;
    }
}
