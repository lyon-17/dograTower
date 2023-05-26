using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroductionScript : MonoBehaviour
{

    public TMP_Text text_load;

    /**
     * Loading... text invisible at start
     */
    private void Start()
    {
        text_load.faceColor = new Color32(0, 0, 0, 0);
    }
    // Any key will load the main game
    void Update()
    {
        if (Input.anyKeyDown)
        {
            text_load.faceColor = new Color32(255, 255, 255, 255);
            SceneManager.LoadScene("FirstTower");
        }
    }
}
