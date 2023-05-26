using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{

    public TMP_Text text_score;
    public TMP_Text text_credits;

    private bool _startAnimation = true;

    // Start is called before the first frame update
    void Start()
    {
        //Disable/hide the UI
        CanvasStatsScript.instance.cleanAllStats();
        GameManager.instance.disablePlayerIcons();
        GameManager.instance.disableUIBackground();

        //Score is calculated as
        /*
         * health * 25
         * atk * 5
         * def * 5
         * yk * 200
         * gk * 350
         * rk * 500
         */
        int score =
            GameManager.playerHealth / 25 +
            GameManager.playerAtk * 5 +
            GameManager.playerDef * 5 +
            GameManager.playerYellowKeys * 200 +
            GameManager.playerGreenKeys * 350 +
            GameManager.playerRedKeys * 600;
        text_score.SetText(score.ToString());
        
    }

    /**
     * Move the credits after 2 seconds.
     */
    private void Awake()
    {
        if (_startAnimation)
        {
            _startAnimation = false;
            StartCoroutine(moveCredits());
        }
    }
    IEnumerator moveCredits()
    {
        float currentTime = 19.0f;
        //Animation slowly scroll the text
        float animationTime = 0.1f;
        yield return new WaitForSeconds(2f);
        while (currentTime > 0f)
        {
            Debug.Log(currentTime);
            yield return new WaitForSeconds(animationTime);
            text_credits.transform.position = new Vector3(text_credits.transform.position.x, (text_credits.transform.position.y+0.1f), text_credits.transform.position.z);
            currentTime -= animationTime;
        }
        
    }
}
