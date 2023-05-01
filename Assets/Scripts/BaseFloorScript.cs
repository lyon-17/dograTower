using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseFloorScript
{
    /*Contains the different powerups.
     * 0 -> small potion
     * 1 -> medium potion
     * 2 -> big potion
     * 3 -> atk gem
     * 4 -> def gem
     * 5 -> yellow key
     */
    public GameObject[] items;
    /*
     * Contains the different enemies
     * 0 -> bat (35,10,5)
     */

    public GameObject[] enemies;
}
