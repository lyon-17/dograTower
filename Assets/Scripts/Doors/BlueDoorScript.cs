using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueDoorScript : MonoBehaviour
{
    void Update()
    {
        if (GameManager.playerBlueKeys > 0)
        {
            gameObject.layer = 0;
        }
        else
        {
            gameObject.layer = 6;
        }
    }
}
