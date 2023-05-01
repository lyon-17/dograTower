using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Allows to open doors when player have more than one key
public class YellowDoorScript : MonoBehaviour
{

    
    void Update()
    {
        if(GameManager.playerYellowKeys > 0)
        {
            gameObject.layer = 0;
        }
        else
        {
            gameObject.layer = 6;
        }

    }
}
