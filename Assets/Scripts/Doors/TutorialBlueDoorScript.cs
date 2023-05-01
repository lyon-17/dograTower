using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBlueDoorScript : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (TutorialScript.tutorial.playerBlueKeys > 0)
        {
            gameObject.layer = 0;
        }
        else
        {
            gameObject.layer = 6;
        }
    }
}
