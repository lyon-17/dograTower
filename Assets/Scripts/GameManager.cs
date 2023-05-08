using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject canvas;
    /*
     * Contains the entire list of the items
     * 0 -> small potion
     * 1 -> medium potion
     * 2 -> big potion
     * 3 -> red gem
     * 4 -> blue gem
     * 5 -> yellow key
     * 6 -> yellow door (WIP)
     */
    public GameObject[] items;
    /*
     * Contains the entire list of possible enemies
     * 0 -> terror bat
     * 1 -> gelatinous cube
     */
    public GameObject[] enemies;

    //Stores the current active objects from the scene.
    private static List<GameObject> objs = new List<GameObject>();

    //Stores the entire item list. Which is saved and loaded
    private static List<Item> managerItemList = new List<Item>();

    private static GameObject player, movePoint;

    //To disable/enable icons beetwen scenes.
    private GameObject enemyIcons, playerIcons;
    private Transform gEnemy, gPlayer;

    //Refresh the current scene as "creating" a new one
    private static bool refresh = false;
    //To set the player position to the beggining
    private static bool newGame = false;
    private static bool loadFromMenu = false;

    private static int loadedX;
    private static int loadedY;

    private int originX = 1;
    private int originY = 0;

    //Base player stats

    public static int playerHealth;
    public static int playerAtk;
    public static int playerDef;
    public static int playerYellowKeys;
    public static int playerGreenKeys;
    public static int playerRedKeys;
    public static int playerBlueKeys;

    //For animation purposes
    public int playerDmg;
    public int enemyDmg;

    [HideInInspector]
    public Collider2D enemyCollider;
    [HideInInspector]
    public string enemyName;
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
    public TMP_Text text_name_enemy;
    public TMP_Text text_hp_enemy;
    public TMP_Text text_atk_enemy;
    public TMP_Text text_def_enemy;
    

    public bool battleActive = false;
    private float currentTime;
    //Current floor, testing for audio (remove later)
    public int floor = 0;
    void Awake()
    {
        //Update the icons in the UI
        enemyIcons = GameObject.FindGameObjectWithTag("Icons");
        gEnemy = enemyIcons.transform.GetChild(0);

        playerIcons = GameObject.FindGameObjectWithTag("IconsPlayer");
        gPlayer = playerIcons.transform.GetChild(0);

        gEnemy.gameObject.SetActive(false);
        gPlayer.gameObject.SetActive(true);
        //Keeps the first canvas (holds the stats text) created
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        //Need to refresh the scene
        if (refresh)
        {
            //Everytime game is loaded, a new canvas is created so is must be destroyed
            GameObject[] canvasList = GameObject.FindGameObjectsWithTag("Canvas");
            foreach ( GameObject canva in canvasList)
            {
                if (!canva.Equals(canvas))
                {
                    Destroy(canva);
                }
            }

            loadItems();
            int[] playerStats = { playerHealth, playerAtk, playerDef, playerYellowKeys, playerGreenKeys,playerRedKeys,playerBlueKeys };
            //Call to the stats script to refresh the text with the player stats
            CanvasStatsScript.instance.updatePlayerStats(playerStats);
            player = GameObject.FindGameObjectWithTag("Player");
            movePoint = GameObject.FindGameObjectWithTag("PlayerMovePoint");
            //Load from the title screen uses stored X and Y because player object don't exist before it.
            if (loadFromMenu)
            {
                player.transform.position = new Vector3(loadedX, loadedY, 0);
                movePoint.transform.position = new Vector3(loadedX, loadedY, 0);
                GameSave.current.gameData.playerX = (int)player.transform.position.x;
                GameSave.current.gameData.playerY = (int)player.transform.position.y;
                loadFromMenu = false;
            }
            //Loading game move position to the saved game position
            if (!newGame)
            {
                movePoint.transform.position = new Vector3(GameSave.current.gameData.playerX, GameSave.current.gameData.playerY, 0);
                player.GetComponent<Rigidbody2D>().position = new Vector3(GameSave.current.gameData.playerX, GameSave.current.gameData.playerY, 0);
            }
            //destination is the start if there's the new game
            else
            {
                movePoint.transform.position = new Vector3(originX, originY, 0);
                player.GetComponent<Rigidbody2D>().position = new Vector3(originX, originY, 0);
            }
            newGame = false;
            refresh = false;
        }
        //Create a new instance when loaded
        if (instance == null)
            instance = this;

        //If instance is already loaded, destroy the new overlap instance
        else if (instance != null)
            Destroy(gameObject);
            
        //Don't destroy the gameManager and first canvas when changing scenes
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(canvas);
    }

    public static void startNew()
    {
        //Create a new gameSave
        GameSave.current = new GameSave();
        //Create new player values
        playerHealth = GameSave.current.gameData.playerHealth;
        playerAtk = GameSave.current.gameData.playerAtk;
        playerDef = GameSave.current.gameData.playerDef;
        playerYellowKeys = GameSave.current.gameData.playerYellowKeys;
        playerGreenKeys = GameSave.current.gameData.playerGreenKeys;
        playerRedKeys = GameSave.current.gameData.playerRedKeys;
        playerBlueKeys = GameSave.current.gameData.playerBlueKeys;
        //managerItemList.Clear();
        //GameSave.current.gameData.itemList.Clear();
        managerItemList = ItemsList.createNewList();
        GameSave.current.gameData.itemList = managerItemList;
        //Update the scene adding the objects loaded into the scene
        refresh = true;
        newGame = true;
    }

    private void Start()
    {
        EnemyStatList.loadDictionary();
        //mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //Initialize the gameSave to save
        GameSave.current = new GameSave();
        playerHealth = GameSave.current.gameData.playerHealth;
        playerAtk = GameSave.current.gameData.playerAtk;
        playerDef = GameSave.current.gameData.playerDef;
        playerYellowKeys = GameSave.current.gameData.playerYellowKeys;
        playerGreenKeys = GameSave.current.gameData.playerGreenKeys;
        playerRedKeys = GameSave.current.gameData.playerRedKeys;
        playerBlueKeys = GameSave.current.gameData.playerBlueKeys;
        text_hp.text = ": " + playerHealth;
        text_atk.text = ": " + playerAtk;
        text_def.text = ": " + playerDef;
        text_yk.text = ": " + playerYellowKeys;
        text_gk.text = ": " + playerGreenKeys;
        text_rk.text = ": " + playerRedKeys;
        text_bk.text = ": " + playerBlueKeys;
        GameSave.current.gameData.itemList = new List<Item>();
        int[] playerStats = { playerHealth, playerAtk, playerDef, playerYellowKeys, playerGreenKeys, playerRedKeys, playerBlueKeys };
        //Call to the stats script to refresh the text with the player stats
        CanvasStatsScript.instance.updatePlayerStats(playerStats);
        managerItemList = ItemsList.createNewList();
        //Add items to the local list
        loadItems();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    //Load items from the local list and turns to object added into the scene
    private void loadItems()
    {
        foreach (Item i in managerItemList)
        {
            //Powerups
            if (i.getName().Contains("smallpot"))
            {
                GameObject obj = Instantiate(items[0], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                objs.Add(obj);
            }
            if (i.getName().Contains("mediumpot"))
            {
                GameObject obj = Instantiate(items[1], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                objs.Add(obj);
            }
            if (i.getName().Contains("bigpot"))
            {
                GameObject obj = Instantiate(items[2], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                objs.Add(obj);
            }
            if (i.getName().Contains("atkgem"))
            {
                GameObject obj = Instantiate(items[3], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                objs.Add(obj);
            }
            if (i.getName().Contains("defgem"))
            {
                GameObject obj = Instantiate(items[4], new Vector3(i.getX(),i.getY(),0), Quaternion.identity);
                objs.Add(obj);
            }
            //Keys and doors
            if (i.getName().Contains("yellowkey"))
            {
                GameObject obj = Instantiate(items[5], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                objs.Add(obj);
            }
            if (i.getName().Contains("yellowdoor"))
            {
                GameObject obj = Instantiate(items[6], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                objs.Add(obj);
            }
            if (i.getName().Contains("greenkey"))
            {
                GameObject obj = Instantiate(items[7], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                objs.Add(obj);
            }
            if (i.getName().Contains("greendoor"))
            {
                GameObject obj = Instantiate(items[8], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                objs.Add(obj);
            }
            if (i.getName().Contains("redkey"))
            {
                GameObject obj = Instantiate(items[9], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                objs.Add(obj);
            }
            if (i.getName().Contains("reddoor"))
            {
                GameObject obj = Instantiate(items[10], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                objs.Add(obj);
            }
            if (i.getName().Contains("bluekey"))
            {
                GameObject obj = Instantiate(items[11], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                objs.Add(obj);
            }
            if (i.getName().Contains("bluedoor"))
            {
                GameObject obj = Instantiate(items[12], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                objs.Add(obj);
            }
            //Enemies
            if (i.getName().Contains("smallslime"))
            {
                GameObject obj = Instantiate(enemies[0], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                objs.Add(obj);
            }
            if (i.getName().Contains("brown"))
            {
                GameObject obj = Instantiate(enemies[1], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                objs.Add(obj);
            }
            if (i.getName().Contains("teal"))
            {
                GameObject obj = Instantiate(enemies[2], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                objs.Add(obj);
            }
            if (i.getName().Contains("terror"))
            {
                GameObject obj = Instantiate(enemies[3], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                objs.Add(obj);
            }
            if (i.getName().Contains("gelatinous"))
            {
                GameObject obj = Instantiate(enemies[4], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                objs.Add(obj);
            }
            if (i.getName().Contains("treant"))
            {
                GameObject obj = Instantiate(enemies[5], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                objs.Add(obj);
            }
            if (i.getName().Contains("watcher"))
            {
                GameObject obj = Instantiate(enemies[6], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                objs.Add(obj);
            }
            if (i.getName().Contains("vengeful"))
            {
                GameObject obj = Instantiate(enemies[7], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                objs.Add(obj);
            }
            if (i.getName().Contains("winged"))
            {
                GameObject obj = Instantiate(enemies[8], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                objs.Add(obj);
            }
            if (i.getName().Contains("Dogra"))
            {
                GameObject obj = Instantiate(enemies[9], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                objs.Add(obj);
            }
        }
    }

    //Info from the gameSave and apply it to the manager
    public static void getStats()
    {
        //player stats
        playerHealth = GameSave.current.gameData.playerHealth;
        playerAtk = GameSave.current.gameData.playerAtk;
        playerDef = GameSave.current.gameData.playerDef;
        playerYellowKeys = GameSave.current.gameData.playerYellowKeys;
        playerGreenKeys = GameSave.current.gameData.playerGreenKeys;
        playerRedKeys = GameSave.current.gameData.playerRedKeys;
        playerBlueKeys = GameSave.current.gameData.playerBlueKeys;
        //Player position
        if (player != null)
            player.transform.position = new Vector3(GameSave.current.gameData.playerX, GameSave.current.gameData.playerY, 0);
        else
            loadFromMenu = true;
        //Items in the scene
        managerItemList = GameSave.current.gameData.itemList;
    }

    //Info from manager and updates the gameSave
    public static void updGameData()
    {
        //plyaer stats
        GameSave.current.gameData.playerHealth = playerHealth;
        GameSave.current.gameData.playerAtk = playerAtk;
        GameSave.current.gameData.playerDef = playerDef;
        GameSave.current.gameData.playerYellowKeys = playerYellowKeys;
        GameSave.current.gameData.playerGreenKeys = playerGreenKeys;
        GameSave.current.gameData.playerRedKeys = playerRedKeys;
        GameSave.current.gameData.playerBlueKeys = playerBlueKeys;

        //Player position saved as x and y
        GameSave.current.gameData.playerX = (int) player.transform.position.x;
        GameSave.current.gameData.playerY = (int) player.transform.position.y;
        //All the active objects are saved as items
        GameSave.current.gameData.itemList = new List<Item>();
        

        string[] objectNames = {"atkgem","defgem","smallpotion","mediumpotion","bigpotion",
                                "yellowkey","greenkey","redkey","bluekey","yellowdoor","greendoor","reddoor","bluedoor",
                                "smallslime","brownslime","tealslime","terrorbat","gelatinouscube","watcher","treant","vengefulspirit","wingeddemon"};

        foreach (GameObject obj in objs)
        {
            //Check that object exist in the current scene
            if (obj != null)
            {
                //Check that object is active, if inactive it isn't saved
                if (obj.activeInHierarchy == true)
                {
                    string objectName = obj.tag.ToLower();
                    if (objectNames.Contains(objectName))
                    {
                        GameSave.current.gameData.itemList.Add(new Item(objectName, (int)obj.transform.position.x, (int)obj.transform.position.y));
                    }
                }
            }
        }
        managerItemList = GameSave.current.gameData.itemList;
    }

    //Creates a new save to generate a new save when needed
    public static void refreshData()
    {
        //Stores player position from the save. To load from the title screen
        if(loadFromMenu)
        {
            loadedX = GameSave.current.gameData.playerX;
            loadedY = GameSave.current.gameData.playerY;
        }
        //Create a new gameSave
        GameSave.current = new GameSave();
        //Stores the player values from the current scene
        GameSave.current.gameData.playerHealth = playerHealth;
        GameSave.current.gameData.playerAtk = playerAtk;
        GameSave.current.gameData.playerDef = playerDef;
        GameSave.current.gameData.playerYellowKeys = playerYellowKeys;
        GameSave.current.gameData.playerGreenKeys = playerGreenKeys;
        GameSave.current.gameData.playerRedKeys = playerRedKeys;
        GameSave.current.gameData.playerBlueKeys = playerBlueKeys;
        //If loaded inside the tower.
        if (player != null)
        {
            GameSave.current.gameData.playerX = (int)player.transform.position.x;
            GameSave.current.gameData.playerY = (int)player.transform.position.y;
        }
        //A new list with the items in the scene
        GameSave.current.gameData.itemList = new List<Item>();
        GameSave.current.gameData.itemList = managerItemList;
        //Update the scene adding the objects loaded into the scene
        refresh = true;
    }

    public bool initBattle()
    {
        //To not update the stats.
        battleActive = true;
        activateIcons();
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

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0)
        {
            CanvasStatsScript.instance.cleanAllStats();
            disablePlayerIcons();
            disactivateIcons();
            SceneManager.LoadScene("GameOver");
        }
        //temp, remove when needed
        if (floor != 10)
        {
            int y = (int)player.GetComponent<Rigidbody2D>().position.y;
            floor = y / 19;
        }
    }
}
