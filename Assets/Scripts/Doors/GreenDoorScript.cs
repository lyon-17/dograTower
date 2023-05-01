using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenDoorScript : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (GameManager.playerGreenKeys > 0)
        {
            gameObject.layer = 0;
        }
        else
        {
            gameObject.layer = 6;
        }
    }
}
