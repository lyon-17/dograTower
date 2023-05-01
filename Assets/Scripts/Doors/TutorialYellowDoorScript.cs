using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialYellowDoorScript : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (TutorialScript.tutorial.playerYellowKeys > 0)
        {
            gameObject.layer = 0;
        }
        else
        {
            gameObject.layer = 6;
        }
    }
}
