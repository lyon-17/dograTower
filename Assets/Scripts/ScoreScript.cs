using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{

    public TMP_Text text_score;

    // Start is called before the first frame update
    void Start()
    {
        //Disable/hide the UI
        CanvasStatsScript.instance.cleanAllStats();
        GameManager.instance.disablePlayerIcons();
        GameManager.instance.disableUIBackground();

        int score =
            GameManager.playerHealth / 25 +
            GameManager.playerAtk * 5 +
            GameManager.playerDef * 5 +
            GameManager.playerYellowKeys * 3 +
            GameManager.playerGreenKeys * 5 +
            GameManager.playerRedKeys * 6;
        text_score.SetText(score.ToString());
    }

}
