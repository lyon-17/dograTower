using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreScript : MonoBehaviour
{

    public TMP_Text text_score;
    public TMP_Text text_credits;

    private bool _startAnimation = true;

    private float _currentTime = 50f;

    // Start is called before the first frame update
    void Start()
    {
        //Disable/hide the UI
        CanvasStatsScript.instance.cleanAllStats();
        GameManager.instance.disablePlayerIcons();
        GameManager.instance.disableUIBackground();

        //Score is calculated as
        /*
         * health / 25
         * atk * 5
         * def * 5
         * yk * 200
         * gk * 350
         * rk * 600
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

    private void Update()
    {
        if(_currentTime < 2f)
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("Intro");
            }
    }

    IEnumerator moveCredits()
    {
        //Animation slowly scroll the text
        float animationTime = 0.15f;
        yield return new WaitForSeconds(2f);
        while (_currentTime > 0f)
        {
            yield return new WaitForSeconds(animationTime);
            text_credits.transform.position = new Vector3(text_credits.transform.position.x, (text_credits.transform.position.y+0.1f), text_credits.transform.position.z);
            _currentTime -= animationTime;
        }
        
    }
}
