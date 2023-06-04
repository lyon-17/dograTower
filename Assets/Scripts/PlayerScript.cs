using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D Rigidbody2D;

    /*
     * Contains various sound effects
     * 0 -> potion
     * 1 -> atk/def gem
     * 2 -> key
     * 3 -> key used
     * 4 -> Stairs up
     * 5 -> Stairs down
     * 6 -> Abyss
     */

    public AudioClip[] SoundsEffect;
    public AudioClip[] playerMoveEffects;

    private float _currentTime;
    //Disable/enable movement if a combat is happening
    private bool _canMove = true;
    private int _moveEffect = 0;

    //disable/enable movement while menu is open
    [HideInInspector]
    public bool disable = false;

    //Check if the battle is active, stops movement and do battle until it finished
    private bool _battleActive = false;

    //Check if there's an encounter
    private bool _battleEncounter = false;

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

    //Canvas for floor transition (Just a black screen)
    public GameObject canvasTransition;
    public TMP_Text canvasTransitionText;
    private int _floor = 1;
    private bool _setFloor = true;

    //0 def, 1 atk, 2 life, 3 yk, 4 gk, 5 rk, 6 bk
    public TMP_Text text_status;

    public float waitingTime = 5f;


    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        movePoint.parent = null;
        canvasTransition.SetActive(false);

        /**
         * Set the camera to the current floor
         */
        if(Rigidbody2D.position.y > 19)
        {
            int times = (int)Rigidbody2D.position.y / 19;
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + (19 * times), Camera.main.transform.position.z);
            GameManager.instance.moveUIBackground(times,true);
        }
    }

    void Update()
    {
        if (_setFloor)
        {
            int times = (int)Rigidbody2D.position.y / 19;
            _floor = times + 1;
            GameSave.current.setFloor(_floor);
            _setFloor = false;
        }
        text_status.transform.position = new Vector3(Rigidbody2D.transform.position.x+2f, Rigidbody2D.transform.position.y+1.5f, Rigidbody2D.transform.position.z);
        if (!disable)
        {
            if (_canMove && canMoveFloors)
            {
                Move();
            }
            //Wait a small timve (400ms) before trying to move again
            if (!_canMove && !_battleEncounter && Time.time > _currentTime + 0.4f)
            {
                _canMove = true;
                _currentTime = Time.time;
            }
            //Battle attempt, a round every 0.85 seconds.
            if (_battleActive && _battleEncounter && Time.time > _currentTime + 0.85)
            {
                _battleActive = GameManager.instance.initBattle();
                //Player receive dmg only when battle is active
                if (_battleActive)
                    StartCoroutine(statusValue(2, GameManager.instance.enemyDmg, false, true, true, GameManager.instance.enemyCollider));
                StartCoroutine(statusValue(2, GameManager.instance.playerDmg, false, true, false, GameManager.instance.enemyCollider));
                _currentTime = Time.time;
            }
            //restore movement and allows new encounters when battle is finished
            if (!_battleActive)
            {
                _canMove = true;
                _battleEncounter = false;
            }
        }
        if (disable && Input.GetKeyDown(KeyCode.Escape))
        {
            menuCanvas.gameObject.SetActive(false);
            disable = false;
            SystemScript.showLoad = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !disable && !_battleActive)
        {
            //open menu
            menuCanvas.gameObject.SetActive(true);
            disable = true;
        }
    }

    //Wait 1 second before player can move again
    IEnumerator moveFloor()
    {
        
        yield return new WaitForSeconds(1f);
        movedFloors = false;
        canMoveFloors = true;
        canvasTransition.SetActive(false);
    }

    //Stop move, that's is. Move the point to the closest player cell.
    private void stopMove()
    {
        movePoint.position = new Vector3(Mathf.Floor(Rigidbody2D.position.x), Mathf.Floor(Rigidbody2D.position.y), 0);
        Rigidbody2D.position = movePoint.position;
    }

    //Move the character
    private void Move()
    {
        /*Final position of the main character will move towards X position at constant speed. */
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
                    Camera.main.GetComponent<AudioSource>().PlayOneShot(playerMoveEffects[_moveEffect],0.4f);
                    //Walking sounds
                    if (_moveEffect == 0)
                        _moveEffect = 1;
                    else
                        _moveEffect = 0;
                }
            }
            //For Y movement
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, blockingLayer))
            {
                if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                    Camera.main.GetComponent<AudioSource>().PlayOneShot(playerMoveEffects[_moveEffect],0.4f);
                    if (_moveEffect == 0)
                        _moveEffect = 1;
                    else
                        _moveEffect = 0;
                }
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "BigPotion")
        {
            potionTrigger(200, collision);
        }
        if(collision.tag == "MediumPotion")
        {
            potionTrigger(75, collision);
        }
        if(collision.tag == "SmallPotion")
        {
            potionTrigger(25, collision);
        }
        if (collision.tag == "AtkGem")
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(SoundsEffect[1],0.5f);
            GameManager.playerAtk++;
            CanvasStatsScript.instance.updateStat("atk", GameManager.playerAtk);
            collision.gameObject.SetActive(false);
            StartCoroutine(statusValue(1, 1, true));
        }
        if (collision.tag == "DefGem")
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(SoundsEffect[1],0.5f);
            GameManager.playerDef++;
            CanvasStatsScript.instance.updateStat("def", GameManager.playerDef);
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
            if (GameManager.playerYellowKeys > 0)
            {
                Camera.main.GetComponent<AudioSource>().PlayOneShot(SoundsEffect[3],0.5f);
                collision.gameObject.SetActive(false);
                GameManager.playerYellowKeys--;
                CanvasStatsScript.instance.updateStat("yk", GameManager.playerYellowKeys);
                StartCoroutine(statusValue(3, 1, false));
            }
        }
        if (collision.tag == "GreenDoor")
        {
            if (GameManager.playerGreenKeys > 0)
            {
                Camera.main.GetComponent<AudioSource>().PlayOneShot(SoundsEffect[3], 0.4f);
                collision.gameObject.SetActive(false);
                GameManager.playerGreenKeys--;
                CanvasStatsScript.instance.updateStat("gk", GameManager.playerGreenKeys);
                StartCoroutine(statusValue(4, 1, false));
            }
        }
        if (collision.tag == "RedDoor")
        {
            if (GameManager.playerRedKeys > 0)
            {
                Camera.main.GetComponent<AudioSource>().PlayOneShot(SoundsEffect[3], 0.4f);
                collision.gameObject.SetActive(false);
                GameManager.playerRedKeys--;
                CanvasStatsScript.instance.updateStat("rk", GameManager.playerRedKeys);
                StartCoroutine(statusValue(5, 1, false));
            }
        }
        if (collision.tag == "BlueDoor")
        {
            if (GameManager.playerBlueKeys > 0)
            {
                Camera.main.GetComponent<AudioSource>().PlayOneShot(SoundsEffect[3], 0.4f);
                collision.gameObject.SetActive(false);
                GameManager.playerBlueKeys--;
                CanvasStatsScript.instance.updateStat("bk", GameManager.playerBlueKeys);
                StartCoroutine(statusValue(6, 1, false));
            }
        }
        if (collision.tag == "SmallSlime")
        {
            enemyTrigger("smallSlime","Small Slime", collision);
        }
        if (collision.tag == "BrownSlime")
        {
            enemyTrigger("brownSlime","Brown Slime", collision);
        }
        if (collision.tag == "TealSlime")
        {
            enemyTrigger("tealSlime","Teal Slime", collision);
        }
        if (collision.tag == "TerrorBat")
        {
            enemyTrigger("terrorbat","Terror Bat", collision);
        }
        if(collision.tag == "Gelatinous")
        {
            enemyTrigger("gelatinouscube","Gelatinous Cube", collision);
        }
        if (collision.tag == "Treant")
        {
            enemyTrigger("treant","Treant", collision);
        }
        if (collision.tag == "Watcher")
        {
            enemyTrigger("watcher","Watcher", collision);
        }
        if (collision.tag == "VengefulSpirit")
        {
            enemyTrigger("vengefulSpirit","Vengeful Spirit", collision);
        }
        if (collision.tag == "WingedDemon")
        {
            enemyTrigger("wingedDemon","Winged Demon", collision);
        }
        if (collision.tag == "Dogra")
        {
            enemyTrigger("Dogra","Dogra, the Abyssal Worm", collision);
        }

        if (collision.tag == "StairsUp" && !movedFloors)
        {
            //Remove comment to make a small animation whenever a floor go up or down
            
            canvasTransition.SetActive(true);
            _floor++;
            canvasTransitionText.text = "Floor " + _floor;
            GameManager.instance.setFloor(_floor);

            Camera.main.GetComponent<AudioSource>().PlayOneShot(SoundsEffect[4], 0.4f);
            canMoveFloors = false;
            GameManager.instance.moveUIBackground(1,false);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + 19, Camera.main.transform.position.z);
            Rigidbody2D.position = new Vector3(collision.transform.position.x, collision.transform.position.y + 19, transform.position.z);
            movePoint.position = new Vector3(collision.transform.position.x, collision.transform.position.y + 19, transform.position.z);
            movedFloors = true;
            StartCoroutine(moveFloor());
        }
        if(collision.tag == "StairsDown" && !movedFloors)
        {
            
            canvasTransition.SetActive(true);
            _floor--;
            canvasTransitionText.text = "Floor " + _floor;
            GameManager.instance.setFloor(_floor);
            
            Camera.main.GetComponent<AudioSource>().PlayOneShot(SoundsEffect[5], 0.4f);
            canMoveFloors = false;
            GameManager.instance.moveUIBackground(-1,false);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - 19, Camera.main.transform.position.z);
            Rigidbody2D.position = new Vector3(collision.transform.position.x, collision.transform.position.y - 19, transform.position.z);
            movePoint.position = new Vector3(collision.transform.position.x, collision.transform.position.y - 19, transform.position.z);
            movedFloors = true;
            StartCoroutine(moveFloor());
        }
        if(collision.tag == "Vortex")
        {
            Camera.main.GetComponent<AudioSource>().PlayOneShot(SoundsEffect[6], 0.4f);
            canMoveFloors = false;
            GameManager.instance.moveUIBackground(1,false);
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + 19, Camera.main.transform.position.z);
            Rigidbody2D.position = new Vector3(collision.transform.position.x, collision.transform.position.y + 8, transform.position.z);
            movePoint.position = new Vector3(collision.transform.position.x, collision.transform.position.y + 8, transform.position.z);
            movedFloors = true;
            GameManager.instance.setFloor(10);
            StartCoroutine(moveFloor());
        }

    }

    private void enemyTrigger(string enemyName,string showName, Collider2D enemyCollider)
    {
        if (!_battleEncounter)
        {
            int[] stats = EnemyStatList.enemyListing[enemyName];
            
            GameManager.instance.enemyName = showName;
            GameManager.instance.enemyHealth = stats[0];
            GameManager.instance.enemyAtk = stats[1];
            GameManager.instance.enemyDef = stats[2];

            if(GameManager.playerAtk - stats[2] <= 0)
            {
                stopMove();
                string[] eStats = { showName, stats[0].ToString(), stats[1].ToString(), stats[2].ToString(), "Can't be hurt" };
                CanvasStatsScript.instance.updateStats(eStats);
                return;
            }
        }
        _battleEncounter = true;
        _battleActive = true;
        _canMove = false;
        GameManager.instance.enemyCollider = enemyCollider;
    }

    private void potionTrigger(int health, Collider2D potionCollider)
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(SoundsEffect[0], 0.4f);
        GameManager.playerHealth += health;
        StartCoroutine(statusValue(2, health, true));
        CanvasStatsScript.instance.updateStat("hp", GameManager.playerHealth);
        potionCollider.gameObject.SetActive(false);
    }

    private void keyTrigger(string keyType, Collider2D keyCollider)
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(SoundsEffect[2], 0.4f);
        switch (keyType)
        {
            case "yellow":
                GameManager.playerYellowKeys++;
                CanvasStatsScript.instance.updateStat("yk", GameManager.playerYellowKeys);
                StartCoroutine(statusValue(3, 1, true));
                break;
            case "green":
                GameManager.playerGreenKeys++;
                CanvasStatsScript.instance.updateStat("gk", GameManager.playerGreenKeys);
                StartCoroutine(statusValue(4, 1, true));
                break;
            case "red":
                GameManager.playerRedKeys++;
                CanvasStatsScript.instance.updateStat("rk", GameManager.playerRedKeys);
                StartCoroutine(statusValue(5, 1, true));
                break;
            case "blue":
                GameManager.playerBlueKeys++;
                CanvasStatsScript.instance.updateStat("bk", GameManager.playerBlueKeys);
                StartCoroutine(statusValue(6, 1, true));
                break;

        }
        keyCollider.gameObject.SetActive(false);
    }

    //Create a small animation whenever something is picked up or removed or combat triggers.
    /**
     * icon -> icon to show
     * number -> number to show
     * inc -> incremental or no incremental
     * combat -> if is on combat or no
     * player -> if effect is onto the player or no
     * collision -> to get collision coords.
     */
    IEnumerator statusValue(int icon, int number, bool inc, bool combat = false, bool player = false, Collider2D collision = null)
    {
        float _currentTime = 0.5f;
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
            _currentTime = 0.3f;
            animationTime = 0.05f;
            GameObject text_e = new GameObject();
            GameObject text_e_value = new GameObject();
            enumeratorComponents(text_e);
            enumeratorComponents(text_e_value);
            text.transform.position = new Vector3(text_status.transform.position.x + 7.5f, text_status.transform.position.y - 3, text_status.transform.position.z);
            text_value.transform.position = new Vector3(text_status.transform.position.x + 7.7f, text_status.transform.position.y - 3, text_status.transform.position.z);
            text_e.transform.position = new Vector3(collision.transform.position.x + 10, collision.transform.position.y-1 , collision.transform.position.z);
            text_e_value.transform.position = new Vector3(collision.transform.position.x + 10.2f, collision.transform.position.y-1 , collision.transform.position.z);
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
            while (_currentTime > 0f)
            {
                yield return new WaitForSeconds(_currentTime);

                if (player)
                {
                    newY += 0.1f;
                    text.transform.position = new Vector3(text.transform.position.x, newY, text.transform.position.z);
                    text_value.transform.position = new Vector3(text_value.transform.position.x, newY, text_value.transform.position.z);
                    text.GetComponent<TextMeshPro>().alpha = alpha;
                    text_value.GetComponent<TextMeshPro>().alpha = alpha;
                    alpha -= 0.2f;
                    _currentTime -= animationTime;
                }
                else
                {
                    newEnemyY += 0.1f;
                    text_e.transform.position = new Vector3(text_e.transform.position.x, newEnemyY, text_e.transform.position.z);
                    text_e_value.transform.position = new Vector3(text_e_value.transform.position.x, newEnemyY, text_e_value.transform.position.z);
                    text_e.GetComponent<TextMeshPro>().alpha = alpha;
                    text_e_value.GetComponent<TextMeshPro>().alpha = alpha;
                    alpha -= 0.2f;
                    _currentTime -= animationTime;
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
            while (_currentTime > 0f)
            {
                yield return new WaitForSeconds(_currentTime);
                newY += 0.1f;

                text.transform.position = new Vector3(text.transform.position.x, newY, text.transform.position.z);
                text_value.transform.position = new Vector3(text_value.transform.position.x, newY, text_value.transform.position.z);
                text.GetComponent<TextMeshPro>().alpha = alpha;
                text_value.GetComponent<TextMeshPro>().alpha = alpha;
                alpha -= 0.2f;
                _currentTime -= animationTime;
            }
            text.SetActive(false);
            text_value.SetActive(false);

            yield return new WaitForSeconds(0.8f);
        }
    }
    /**
     * Attach components into the scene
     */
    private void enumeratorComponents(GameObject obj)
    {
        obj.AddComponent<RectTransform>();
        obj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.4f);
        obj.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0.4f);
        obj.AddComponent<TextMeshPro>();
    }
}
