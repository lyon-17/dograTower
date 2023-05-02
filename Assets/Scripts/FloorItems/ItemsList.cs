using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsList : MonoBehaviour
{
    private static List<Item> FloorItemList = new List<Item>();

    private static void generateList()
    {
        //Keys
        FloorItemList.Add(new Item("yellowkey", 3, 1));
        FloorItemList.Add(new Item("yellowkey", 1, 6));
        FloorItemList.Add(new Item("yellowkey", 2, 6));
        FloorItemList.Add(new Item("yellowkey", 13, 1));
        FloorItemList.Add(new Item("yellowkey", 12, 10));

        FloorItemList.Add(new Item("greenkey", 3, 6));
        FloorItemList.Add(new Item("greenkey", 9, 6));

        FloorItemList.Add(new Item("redkey", 0, 8));

        FloorItemList.Add(new Item("bluekey", 9, 9));

        //Doors

        FloorItemList.Add(new Item("yellowdoor", 5, 3));
        FloorItemList.Add(new Item("yellowdoor", 1, 13));
        FloorItemList.Add(new Item("yellowdoor", 3, 13));
        FloorItemList.Add(new Item("yellowdoor", 6, 13));
        FloorItemList.Add(new Item("yellowdoor", 8, 13));
        FloorItemList.Add(new Item("yellowdoor", 11, 3));
        FloorItemList.Add(new Item("yellowdoor", 11, 1));

        FloorItemList.Add(new Item("greendoor", 3, 8));
        FloorItemList.Add(new Item("greendoor", 9, 3));
        FloorItemList.Add(new Item("greendoor", 13, 6));

        FloorItemList.Add(new Item("reddoor", 6, 4));

        FloorItemList.Add(new Item("bluedoor", 7, 3));

        //Powerups

        FloorItemList.Add(new Item("atkgem", 12, 11));
        FloorItemList.Add(new Item("atkgem", 9, 2));

        FloorItemList.Add(new Item("smallpotion", 8, 6));
        FloorItemList.Add(new Item("smallpotion", 10, 6));
        FloorItemList.Add(new Item("smallpotion", 9, 7));
        FloorItemList.Add(new Item("smallpotion", 9, 1));
        FloorItemList.Add(new Item("smallpotion", 11, 2));
        FloorItemList.Add(new Item("smallpotion", 0, 10));
        FloorItemList.Add(new Item("smallpotion", 0, 9));

        FloorItemList.Add(new Item("mediumpotion", 13, 2));
        FloorItemList.Add(new Item("mediumpotion", 12, 9));
        FloorItemList.Add(new Item("mediumpotion", 12, 12));

        //Enemies

        FloorItemList.Add(new Item("smallslime", 4, 4));
        FloorItemList.Add(new Item("smallslime", 2, 4));

        FloorItemList.Add(new Item("terrorbat", 7, 9));
        FloorItemList.Add(new Item("terrorbat", 7, 10));

        FloorItemList.Add(new Item("tealslime", 10, 0));
        FloorItemList.Add(new Item("tealslime", 12, 0));
        FloorItemList.Add(new Item("tealslime", 2, 7));

        FloorItemList.Add(new Item("brownslime", 12, 6));

        FloorItemList.Add(new Item("watcher", 9, 5));
    }

    public static void generateItemList()
    {
        generateList();
        List<Item> listFloorTwo = ItemsFloorTwo.getList();
        List<Item> listFloorThree = ItemsFloorThree.getList();
        List<Item> listFloorFour = ItemsFloorFour.getList();
        List<Item> listFloorFive = ItemsFloorFive.getList();
        List<Item> listFloorSix = ItemsFloorSix.getList();
        List<Item> listFloorSeven = ItemsFloorSeven.getList();
        List<Item> listFloorEight = ItemsFloorEight.getList();
        List<Item> listFloorNine = ItemsFloorNine.getList();
        List<Item> listFloorTen = ItemsFloorTen.getList();

        FloorItemList.AddRange(listFloorTwo);
        FloorItemList.AddRange(listFloorThree);
        FloorItemList.AddRange(listFloorFour);
        FloorItemList.AddRange(listFloorFive);
        FloorItemList.AddRange(listFloorSix);
        FloorItemList.AddRange(listFloorSeven);
        FloorItemList.AddRange(listFloorEight);
        FloorItemList.AddRange(listFloorNine);
        FloorItemList.AddRange(listFloorTen);
    }

    public static List<Item> createNewList()
    {
        if (FloorItemList.Count == 0)
        {
            generateItemList();
        }
        return FloorItemList;
    }

}
