using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsList : MonoBehaviour
{
    private static List<Item> _FloorItemList = new List<Item>();

    /**
     * Generate the entire list of items.
     */
    private static void generateList()
    {
        //Keys
        _FloorItemList.Add(new Item("yellowkey", 3, 1));
        _FloorItemList.Add(new Item("yellowkey", 1, 6));
        _FloorItemList.Add(new Item("yellowkey", 2, 6));
        _FloorItemList.Add(new Item("yellowkey", 13, 1));
        _FloorItemList.Add(new Item("yellowkey", 12, 10));

        _FloorItemList.Add(new Item("greenkey", 3, 6));
        _FloorItemList.Add(new Item("greenkey", 9, 6));

        _FloorItemList.Add(new Item("redkey", 0, 8));

        _FloorItemList.Add(new Item("bluekey", 9, 9));

        //Doors

        _FloorItemList.Add(new Item("yellowdoor", 5, 3));
        _FloorItemList.Add(new Item("yellowdoor", 1, 13));
        _FloorItemList.Add(new Item("yellowdoor", 3, 13));
        _FloorItemList.Add(new Item("yellowdoor", 6, 13));
        _FloorItemList.Add(new Item("yellowdoor", 8, 13));
        _FloorItemList.Add(new Item("yellowdoor", 11, 3));
        _FloorItemList.Add(new Item("yellowdoor", 11, 1));

        _FloorItemList.Add(new Item("greendoor", 3, 8));
        _FloorItemList.Add(new Item("greendoor", 9, 3));
        _FloorItemList.Add(new Item("greendoor", 13, 6));

        _FloorItemList.Add(new Item("reddoor", 6, 4));

        _FloorItemList.Add(new Item("bluedoor", 7, 3));

        //Powerups

        _FloorItemList.Add(new Item("atkgem", 12, 11));
        _FloorItemList.Add(new Item("atkgem", 9, 2));

        _FloorItemList.Add(new Item("smallpotion", 8, 6));
        _FloorItemList.Add(new Item("smallpotion", 10, 6));
        _FloorItemList.Add(new Item("smallpotion", 9, 7));
        _FloorItemList.Add(new Item("smallpotion", 9, 1));
        _FloorItemList.Add(new Item("smallpotion", 11, 2));
        _FloorItemList.Add(new Item("smallpotion", 0, 10));
        _FloorItemList.Add(new Item("smallpotion", 0, 9));

        _FloorItemList.Add(new Item("mediumpotion", 13, 2));
        _FloorItemList.Add(new Item("mediumpotion", 12, 9));
        _FloorItemList.Add(new Item("mediumpotion", 12, 12));

        //Enemies

        _FloorItemList.Add(new Item("smallslime", 4, 4));
        _FloorItemList.Add(new Item("smallslime", 2, 4));

        _FloorItemList.Add(new Item("terrorbat", 7, 9));
        _FloorItemList.Add(new Item("terrorbat", 7, 10));

        _FloorItemList.Add(new Item("tealslime", 10, 0));
        _FloorItemList.Add(new Item("tealslime", 12, 0));
        _FloorItemList.Add(new Item("tealslime", 2, 7));

        _FloorItemList.Add(new Item("brownslime", 12, 6));

        _FloorItemList.Add(new Item("watcher", 9, 5));
    }

    public static List<Item> getList()
    {
        if (_FloorItemList.Count == 0)
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

            _FloorItemList.AddRange(listFloorTwo);
            _FloorItemList.AddRange(listFloorThree);
            _FloorItemList.AddRange(listFloorFour);
            _FloorItemList.AddRange(listFloorFive);
            _FloorItemList.AddRange(listFloorSix);
            _FloorItemList.AddRange(listFloorSeven);
            _FloorItemList.AddRange(listFloorEight);
            _FloorItemList.AddRange(listFloorNine);
            _FloorItemList.AddRange(listFloorTen);
        }

        return _FloorItemList;
    }
}
