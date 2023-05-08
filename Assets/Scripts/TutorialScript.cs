using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class TutorialScript : MonoBehaviour
{
    //Player params
    private Rigidbody2D Rigidbody2D;
    public static TutorialScript tutorial = null;

    //If the object is disabled isn't found by Find... to bypass that, I add an extra parent.
    private GameObject enemyIcons;
    private Transform gEnemy;

    private GameObject playerIcons;
    private Transform gPlayer;

    private float currentTime;
    private bool canMove = true;

    //disable/enable movement while menu is open
    [HideInInspector]
    public bool disable = false;

    //Check if the battle is active, stops movement and do battle until it finished
    public bool battleActive = false;

    //Check if there's an encounter
    private bool battleEncounter = false;

    //Check to stop in stairs and then enabled them after move
    public bool movedFloors = false;
    public bool canMoveFloors = true;

    /*Movement speed*/
    public float moveSpeed = 5f;
    //Destination point
    public Transform movePoint;
    //Layer that blocks the player
    public LayerMask blockingLayer;
    //Menu canvas//
    public Canvas menuCanvas;

    public float waitingTime = 5f;

    //Base player stats

    public int playerHealth;
    public int playerAtk;
    public int playerDef;
    public int playerYellowKeys;
    public int playerGreenKeys;
    public int playerRedKeys;
    public int playerBlueKeys;

    private int playerDmg;
    private int enemyDmg;

    [HideInInspector]
    public Collider2D enemyCollider;
    private string enemyName;
    [HideInInspector]
    public int enemyHealth;
    [HideInInspector]
    public int enemyAtk;
    [HideInInspector]
    public int enemyDef;

    public TMP_Text text_hp;
    public TMP_Text text_atk;
    public TMP_Text text_def;
    public TMP_Text text_yk;
    public TMP_Text text_gk;
    public TMP_Text text_rk;
    public TMP_Text text_bk;
    public TMP_Text text_eName;
    public TMP_Text text_hp_enemy;
    public TMP_Text text_atk_enemy;
    public TMP_Text text_def_enemy;
    public TMP_Text text_status;

    private void Start()
    {
        tutorial = this;

        enemyIcons = GameObject.FindGameObjectWithTag("Icons");
        gEnemy = enemyIcons.transform.GetChild(0);

        playerIcons = GameObject.FindGameObjectWithTag("IconsPlayer");
        gPlayer = playerIcons.transform.GetChild(0);

        //Enable icons (exit to the menu shows them)
        gPlayer.gameObject.SetActive(true);
        gEnemy.gameObject.SetActive(false);
        //Initialize the gameSave to save
        playerHealth = 100;
        playerAtk = 12;
        playerDef = 2;
        playerYellowKeys = 0;
        playerGreenKeys = 0;
        playerRedKeys = 0;
        playerBlueKeys = 0;
        int[] playerStats = { playerHealth, playerAtk, playerDef, playerYellowKeys, playerGreenKeys, playerRedKeys, playerBlueKeys };
        //Call to the stats script to refresh the text with the player stats
        CanvasStatsScript.instance.updatePlayerStats(playerStats);

        //Player component
        Rigidbody2D = GetComponent<Rigidbody2D>();
        movePoint.parent = null;
    }

    //Deactivate/activate enemy icons.
    public void activateIcons()
    {
        gEnemy.gameObject.SetActive(true);
    }
    public void disactivateIcons()
    {
        gEnemy.gameObject.SetActive(false);
    }
    public void disablePlayerIcons()
    {
        gPlayer.gameObject.SetActive(false);
    }

    void Update()
    {
        text_status.transform.position = new Vector3(Rigidbody2D.transform.position.x + 2f, Rigidbody2D.transform.position.y + 1.5f, Rigidbody2D.transform.position.z);
        if(playerHealth <= 0)
        {
            CanvasStatsScript.instance.cleanAllStats();
            disablePlayerIcons();
            disactivateIcons();
            SceneManager.LoadScene("GameOver");
        }
        if (!disable)
        {
            //Battle attempt, should be a round every X time.
            if (battleActive && battleEncounter && Time.time > currentTime + 0.85)
            {
                battleActive = initBattle();
                if (battleActive)
                    StartCoroutine(statusValue(2, enemyDmg, false, true, true, enemyCollider));
                StartCoroutine(statusValue(2, playerDmg, false, true, false, enemyCollider));

                currentTime = Time.time;
            }
            //restore movement and allows new encounters when battle is finished
            if (!battleActive)
            {
                canMove = true;
                battleEncounter = false;
                
            }
            if (canMove && canMoveFloors)
            {
                Move();
            }
            //Wait a small timve (400ms) before trying to move again
            if (!canMove && !battleEncounter && Time.time > currentTime + 0.4f)
            {
                canMove = true;
                currentTime = Time.time;
            }
            if (Rigidbody2D.transform.position.x == 5 && Rigidbody2D.transform.position.y == 30)
            {
                CanvasStatsScript.instance.cleanAllStats();
                disablePlayerIcons();
                SceneManager.LoadScene("Intro");
            }
                
        }
        if (disable && Input.GetKeyDown(KeyCode.Escape))
        {
            menuCanvas.gameObject.SetActive(false);
            disable = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !disable)
        {
            //open menu
            menuCanvas.gameObject.SetActive(true);
            disable = true;
        }
    }

    //Move the character
    private void Move()
    {
        /*Final position of the main character will move towards X position at constant speed.
            /Allow diagonal movement (to remove it would be adding a elseif for y movement
            */
        Rigidbody2D.position = Vector3.MoveTowards(Rigidbody2D.position, movePoint.position, moveSpeed * Time.deltaTime);

        //Check if distance is minimum, to move again
        if (Vector3.Distance(Rigidbody2D.position, movePoint.position) <= Mathf.Epsilon)
        {
            //Either horizontal movement
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                //Create a circle ahead where player is moving, if the circle don't overlap with the blocking layer character will move
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, blockingLayer))
                {
                    //Move the character in X coord.
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }
            }
            //For Y movement
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, blockingLayer))
            {
                if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
            }
        }
        //TutorialMsgs();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TerrorBat")
        {
            if (!battleEncounter)
            {
                enemyName = "Weak Terror bat";
                enemyHealth = 10;
                enemyAtk = 5;
                enemyDef = 5;
                enemyCollider = collision;
            }
            battleEncounter = true;
            battleActive = true;
            canMove = false;
        }
        if (collision.tag == "Gelatinous")
        {
            if (!battleEncounter)
            {
                enemyName = "Weak Gel cube";
                enemyHealth = 30;
                enemyAtk = 20;
                enemyDef = 3;
                enemyCollider = collision;
            }
            battleEncounter = true;
            battleActive = true;
            canMove = false;
        }
        if (collision.tag == "Treant")
        {
            if (!battleEncounter)
            {
                enemyName = "Weak Treant";
                enemyHealth = 30;
                enemyAtk = 30;
                enemyDef = 6;
                enemyCollider = collision;
            }
            battleEncounter = true;
            battleActive = true;
            canMove = false;
        }
        if (collision.tag == "BigPotion")
        {
            potionTrigger(200, collision);
        }
        if (collision.tag == "MediumPotion")
        {
            potionTrigger(75, collision);
        }
        if (collision.tag == "SmallPotion")
        {
            potionTrigger(25, collision);
        }
        if (collision.tag == "RedGem")
        {
            playerAtk++;
            CanvasStatsScript.instance.updateStat("atk", playerAtk);
            StartCoroutine(statusValue(1, 1, true));
            collision.gameObject.SetActive(false);
        }
        if (collision.tag == "BlueGem")
        {
            playerDef++;
            CanvasStatsScript.instance.updateStat("def", playerDef);
            StartCoroutine(statusValue(0, 1, true));
            collision.gameObject.SetActive(false);
        }
        if (collision.tag == "YellowKey")
        {
            keyTrigger("yellow", collision);
        }
        if (collision.tag == "GreenKey")
        {
            keyTrigger("green", collision);
        }
        if (collision.tag == "RedKey")
        {
            keyTrigger("red", collision);
        }
        if (collision.tag == "BlueKey")
        {
            keyTrigger("blue", collision);
        }
        if (collision.tag == "YellowDoor")
        {
            if (playerYellowKeys > 0)
            {
                collision.gameObject.SetActive(false);
                playerYellowKeys--;
                StartCoroutine(statusValue(3, 1, false));
                CanvasStatsScript.instance.updateStat("yk", playerYellowKeys);
            }
        }
        if (collision.tag == "GreenDoor")
        {
            if (playerGreenKeys > 0)
            {
                collision.gameObject.SetActive(false);
                playerGreenKeys--;
                StartCoroutine(statusValue(4, 1, false));
                CanvasStatsScript.instance.updateStat("gk", playerGreenKeys);
            }
        }
        if (collision.tag == "RedDoor")
        {
            if (playerRedKeys > 0)
            {
                collision.gameObject.SetActive(false);
                playerRedKeys--;
                StartCoroutine(statusValue(5, 1, false));
                CanvasStatsScript.instance.updateStat("rk", playerRedKeys);
            }
        }
        if (collision.tag == "BlueDoor")
        {
            if (playerBlueKeys > 0)
            {
                collision.gameObject.SetActive(false);
                playerBlueKeys--;
                StartCoroutine(statusValue(6, 1, false));
                CanvasStatsScript.instance.updateStat("bk", playerBlueKeys);
            }
        }
        if (collision.tag == "Finish")
        {
            CanvasStatsScript.instance.cleanAllStats();
            disablePlayerIcons();
            SceneManager.LoadScene("Intro");
        }
        if (collision.tag == "StairsUp" && !movedFloors)
        {
            canMoveFloors = false;
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + 19, Camera.main.transform.position.z);
            enemyIcons.transform.position = new Vector3(enemyIcons.transform.position.x, enemyIcons.transform.position.y, Camera.main.transform.position.z);
            Rigidbody2D.position = new Vector3(collision.transform.position.x, collision.transform.position.y + 19, transform.position.z);
            movePoint.position = new Vector3(collision.transform.position.x, collision.transform.position.y + 19, transform.position.z);
            movedFloors = true;
            StartCoroutine(moveFloor());
        }
        if (collision.tag == "StairsDown" && !movedFloors)
        {
            canMoveFloors = false;
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 19, Camera.main.transform.position.z);
            Rigidbody2D.position = new Vector3(collision.transform.position.x, collision.transform.position.y - 19, transform.position.z);
            movePoint.position = new Vector3(collision.transform.position.x, collision.transform.position.y - 19, transform.position.z);
            enemyIcons.transform.position = new Vector3(enemyIcons.transform.position.x, enemyIcons.transform.position.y, Camera.main.transform.position.z);
            movedFloors = true;
            StartCoroutine(moveFloor());
        }

    }

    private void potionTrigger(int health, Collider2D potionCollider)
    {
        playerHealth += health;
        CanvasStatsScript.instance.updateStat("hp", playerHealth);
        StartCoroutine(statusValue(2, health, true));
        potionCollider.gameObject.SetActive(false);
    }

    private void keyTrigger(string keyType, Collider2D keyCollider)
    {
        switch (keyType)
        {
            case "yellow":
                playerYellowKeys++;
                CanvasStatsScript.instance.updateStat("yk", playerYellowKeys);
                StartCoroutine(statusValue(3, 1, true));
                break;
            case "green":
                playerGreenKeys++;
                CanvasStatsScript.instance.updateStat("gk", playerGreenKeys);
                StartCoroutine(statusValue(4, 1, true));
                break;
            case "red":
                playerRedKeys++;
                CanvasStatsScript.instance.updateStat("rk", playerRedKeys);
                StartCoroutine(statusValue(5, 1, true));
                break;
            case "blue":
                playerBlueKeys++;
                CanvasStatsScript.instance.updateStat("bk", playerBlueKeys);
                StartCoroutine(statusValue(6, 1, true));
                break;

        }
        keyCollider.gameObject.SetActive(false);
    }

    public bool initBattle()
    {
        //To not update the stats.
        activateIcons();
        battleActive = true;
        string[] enemyStats = { enemyName, enemyHealth.ToString(), enemyAtk.ToString(), enemyDef.ToString() };
        CanvasStatsScript.instance.updateEnemyStats(enemyStats);

        playerDmg = playerAtk - enemyDef;
        enemyDmg = enemyAtk - playerDef;
        enemyHealth -= playerDmg;
        CanvasStatsScript.instance.updateStat("elife", enemyHealth);
        if (enemyHealth <= 0)
        {
            enemyCollider.gameObject.SetActive(false);
            disactivateIcons();
            CanvasStatsScript.instance.cleanStats();
            battleActive = false;
            return false;

        }
        playerHealth -= enemyDmg;
        CanvasStatsScript.instance.updateStat("hp", playerHealth);
        return true;
    }

    public int getPlayerPositionX()
    {
        return (int) Rigidbody2D.transform.position.x;
    }
    public int getPlayerPositionY()
    {
        return (int)Rigidbody2D.transform.position.y;
    }

    IEnumerator moveFloor()
    {
        //float currentTime = 1.0f;
        //float animationTime = 0.1f;
        //Remove to add a animation

        /*canvasTransitionPanel.GetComponent<Image>().material.color = new Color(1.0f, 1.0f, 1.0f, currentTime);
        while (currentTime > 0f)
        {
            yield return new WaitForSeconds(animationTime);
            currentTime -= animationTime;
            canvasTransitionPanel.GetComponent<Image>().material.color = new Color(1.0f, 1.0f, 1.0f, currentTime);
        }
        canvasTransitionText.SetText("");*/
        yield return new WaitForSeconds(1f);

        movedFloors = false;
        canMoveFloors = true;
    }

    IEnumerator statusValue(int icon, int number, bool inc, bool combat = false, bool player = false, Collider2D collision = null)
    {
        float currentTime = 0.5f;
        float animationTime = 0.1f;
        float alpha = 1f;
        //Create a new text for each object
        GameObject text = new GameObject();
        GameObject text_value = new GameObject();
        enumeratorComponents(text);
        enumeratorComponents(text_value);
        text.transform.position = new Vector3(text_status.transform.position.x + 7, text_status.transform.position.y - 2, text_status.transform.position.z);
        text_value.transform.position = new Vector3(text_status.transform.position.x + 7.5f, text_status.transform.position.y - 2, text_status.transform.position.z);
        text.GetComponent<TextMeshPro>().fontSize = 12;
        text_value.GetComponent<TextMeshPro>().fontSize = 12;

        float newY = text_status.transform.position.y - 2;
        //Combat life animation
        if (combat)
        {
            currentTime = 0.3f;
            animationTime = 0.05f;
            GameObject text_e = new GameObject();
            GameObject text_e_value = new GameObject();
            enumeratorComponents(text_e);
            enumeratorComponents(text_e_value);
            text.transform.position = new Vector3(text_status.transform.position.x + 7.5f, text_status.transform.position.y - 3, text_status.transform.position.z);
            text_value.transform.position = new Vector3(text_status.transform.position.x + 7.7f, text_status.transform.position.y - 3, text_status.transform.position.z);
            text_e.transform.position = new Vector3(collision.transform.position.x + 10, collision.transform.position.y - 1, collision.transform.position.z);
            text_e_value.transform.position = new Vector3(collision.transform.position.x + 10.2f, collision.transform.position.y - 1, collision.transform.position.z);
            text.GetComponent<TextMeshPro>().fontSize = 7;
            text_value.GetComponent<TextMeshPro>().fontSize = 7;
            text_e.GetComponent<TextMeshPro>().fontSize = 7;
            text_e_value.GetComponent<TextMeshPro>().fontSize = 7;

            newY = text_status.transform.position.y - 3;
            float newEnemyY = collision.transform.position.y - 1;
            //Color is always red
            if (player)
            {
                text.GetComponent<TextMeshPro>().text = "<sprite=\"status_icons\" index=" + icon + ">";
                text_value.GetComponent<TextMeshPro>().color = new Color(1f, 0.325f, 0.325f);
                text_value.GetComponent<TextMeshPro>().text = "- " + number.ToString();
            }
            else
            {
                text_e.GetComponent<TextMeshPro>().text = "<sprite=\"status_icons\" index=" + icon + ">";
                text_e_value.GetComponent<TextMeshPro>().color = new Color(1f, 0.325f, 0.325f);
                text_e_value.GetComponent<TextMeshPro>().text = "- " + number.ToString();
            }
            while (currentTime > 0f)
            {
                yield return new WaitForSeconds(currentTime);

                if (player)
                {
                    newY += 0.1f;
                    text.transform.position = new Vector3(text.transform.position.x, newY, text.transform.position.z);
                    text_value.transform.position = new Vector3(text_value.transform.position.x, newY, text_value.transform.position.z);
                    text.GetComponent<TextMeshPro>().alpha = alpha;
                    text_value.GetComponent<TextMeshPro>().alpha = alpha;
                    alpha -= 0.2f;
                    currentTime -= animationTime;
                }
                else
                {
                    newEnemyY += 0.1f;
                    text_e.transform.position = new Vector3(text_e.transform.position.x, newEnemyY, text_e.transform.position.z);
                    text_e_value.transform.position = new Vector3(text_e_value.transform.position.x, newEnemyY, text_e_value.transform.position.z);
                    text_e.GetComponent<TextMeshPro>().alpha = alpha;
                    text_e_value.GetComponent<TextMeshPro>().alpha = alpha;
                    alpha -= 0.2f;
                    currentTime -= animationTime;
                }

            }
            text.SetActive(false);
            text_value.SetActive(false);
            text_e.SetActive(false);
            text_e_value.SetActive(false);

            yield return new WaitForSeconds(0.8f);
        }
        else
        {

            text.GetComponent<TextMeshPro>().text = "<sprite=\"status_icons\" index=" + icon + ">";

            //Picking items animation
            if (inc)
            {
                text_value.GetComponent<TextMeshPro>().color = new Color(0.168f, 0.737f, 0.196f);
                text_value.GetComponent<TextMeshPro>().text = "+ " + number.ToString();
            }
            else
            {
                text_value.GetComponent<TextMeshPro>().color = new Color(0.774f, 0.054f, 0.061f);
                text_value.GetComponent<TextMeshPro>().text = "- " + number.ToString();
            }
            while (currentTime > 0f)
            {
                yield return new WaitForSeconds(currentTime);
                newY += 0.1f;

                text.transform.position = new Vector3(text.transform.position.x, newY, text.transform.position.z);
                text_value.transform.position = new Vector3(text_value.transform.position.x, newY, text_value.transform.position.z);
                text.GetComponent<TextMeshPro>().alpha = alpha;
                text_value.GetComponent<TextMeshPro>().alpha = alpha;
                alpha -= 0.2f;
                currentTime -= animationTime;
            }
            text.SetActive(false);
            text_value.SetActive(false);

            yield return new WaitForSeconds(0.8f);
        }
    }

    private void enumeratorComponents(GameObject obj)
    {
        obj.AddComponent<RectTransform>();
        obj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.5f);
        obj.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0.5f);
        obj.AddComponent<TextMeshPro>();
    }

}
