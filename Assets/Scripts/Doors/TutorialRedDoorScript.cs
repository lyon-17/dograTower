using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRedDoorScript : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (TutorialScript.tutorial.playerRedKeys > 0)
        {
            gameObject.layer = 0;
        }
        else
        {
            gameObject.layer = 6;
        }
    }
}
