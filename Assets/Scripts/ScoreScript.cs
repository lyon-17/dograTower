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
        /*CanvasStatsScript.instance.cleanAllStats();
        GameManager.instance.disablePlayerIcons();
        GameManager.instance.disableUIBackground();

        int score =
            GameManager.playerHealth / 25 +
            GameManager.playerAtk * 5 +
            GameManager.playerDef * 5 +
            GameManager.playerYellowKeys * 3 +
            GameManager.playerGreenKeys * 5 +
            GameManager.playerRedKeys * 6;
        text_score.SetText(score.ToString());*/
        
    }
    private void Update()
    {
        
    }
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
