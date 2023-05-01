using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGreenDoorScript : MonoBehaviour
{
    private void Update()
    {
        if (TutorialScript.tutorial.playerGreenKeys > 0)
        {
            gameObject.layer = 0;
        }
        else
        {
            gameObject.layer = 6;
        }
    }
}
    
