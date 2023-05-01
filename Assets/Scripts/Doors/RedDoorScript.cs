using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDoorScript : MonoBehaviour
{

    void Update()
    {
        if (GameManager.playerRedKeys > 0)
        {
            gameObject.layer = 0;
        }
        else
        {
            gameObject.layer = 6;
        }
    }
}
