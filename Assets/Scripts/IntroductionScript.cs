using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class IntroductionScript : MonoBehaviour
{

    public TMP_Text text_load;

    private void Start()
    {
        text_load.faceColor = new Color32(0, 0, 0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            text_load.faceColor = new Color32(255, 255, 255, 255);
            SceneManager.LoadScene("FirstTower");
        }
    }
}
