using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject canvas;
    public AudioClip[] combatSounds;
    public AudioClip Hurt;
    /*
     * Contains the entire list of the items
     * 0 -> small potion
     * 1 -> medium potion
     * 2 -> big potion
     * 3 -> red gem
     * 4 -> blue gem
     * 5 -> yellow key
     * 6 -> yellow door
     * 7 -> Green key
     * 8 -> Green door
     * 9 -> red key
     * 10 -> red door
     * 11 -> blue key
     * 12 -> blue door
     */
    public GameObject[] items;
    /*
     * Contains the entire list of possible enemies
     * 0 -> small slime
     * 1 -> brown slime
     * 2 -> teal slime
     * 3 -> terror bat
     * 4 -> gelatinous cube
     * 5 -> treant
     * 6 -> watcher
     * 7 -> vengeful spirit
     * 8 -> winged demon
     * 9 -> Dogra
     */
    public GameObject[] enemies;

    //Stores the current active objects from the scene.
    private static List<GameObject> _objs = new List<GameObject>();

    //Stores the entire item list. Which is saved and loaded
    private static List<Item> _managerItemList = new List<Item>();

    //Stores player and movepoint
    private static GameObject _player, _movePoint;

    //To disable/enable icons beetwen scenes.
    private GameObject enemyIcons, playerIcons,uiBackground;
    //Disable/enable backgrounds
    private Transform gEnemy, gPlayer, gBackground;

    //Need to load stats from the file
    private static bool _loadStats = false;

    //Refresh the current scene as "creating" a new one
    private static bool _refresh = false;
    //To set the player position to the beggining
    private static bool _newGame = false;
    //Loading come from menu. GameManager isn't created so need to store the load data, load the game and then put the data into the game
    private static bool _loadFromMenu = false;

    //player coords in the save
    private static int _loadedX;
    private static int _loadedY;

    //Origin position
    private int _originX = 1;
    private int _originY = 0;

    //First floor
    private int _floor = 1;

    //Check if player have been defeated
    private static bool _defeated = false;

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

    //Enemy collider to the battle
    [HideInInspector]
    public Collider2D enemyCollider;
    //Enemy stats
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
    //Check if the battle is active
    public bool battleActive = false;
    
    void Awake()
    {
        
        //Update the icons in the UI
        enemyIcons = GameObject.FindGameObjectWithTag("Icons");
        gEnemy = enemyIcons.transform.GetChild(0);

        playerIcons = GameObject.FindGameObjectWithTag("IconsPlayer");
        gPlayer = playerIcons.transform.GetChild(0);

        uiBackground = GameObject.FindGameObjectWithTag("CanvasBackground");
        gBackground = uiBackground.transform.GetChild(0);

        gEnemy.gameObject.SetActive(false);
        gPlayer.gameObject.SetActive(true);
        gBackground.gameObject.SetActive(true);
        //Keeps the first canvas (holds the stats text) created
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        if (GameSave.current != null)
        {
            if (GameSave.current.getLoad())
            {
                getStats();
                _loadStats = true;
            }
        }

        //Need to refresh the scene
        if (_refresh)
        {
            //Everytime game is loaded, a new canvas is created so is must be destroyed, same with backgrounds
            GameObject[] canvasList = GameObject.FindGameObjectsWithTag("Canvas");
            foreach ( GameObject canva in canvasList)
            {
                if (!canva.Equals(canvas))
                {
                    Destroy(canva);
                }
            }

            GameObject[] uiBackgrounds = GameObject.FindGameObjectsWithTag("CanvasBackground");
            foreach (GameObject uiBack in uiBackgrounds)
            {
                if (!uiBack.Equals(uiBackground))
                {
                    Destroy(uiBack);
                }
            }

            uiBackground.transform.position = new Vector3(uiBackground.transform.position.x, 0, uiBackground.transform.position.z);

            loadItems();
            int[] playerStats = { playerHealth, playerAtk, playerDef, playerYellowKeys, playerGreenKeys,playerRedKeys,playerBlueKeys };
            //Call to the stats script to refresh the text with the player stats
            CanvasStatsScript.instance.updatePlayerStats(playerStats);
            _player = GameObject.FindGameObjectWithTag("Player");
            _movePoint = GameObject.FindGameObjectWithTag("PlayerMovePoint");
            //Load from the title screen uses stored X and Y because player object don't exist before it.
            if (_loadFromMenu)
            {
                _player.transform.position = new Vector3(_loadedX, _loadedY, 0);
                _movePoint.transform.position = new Vector3(_loadedX, _loadedY, 0);
                GameSave.current.gameData.playerX = (int)_player.transform.position.x;
                GameSave.current.gameData.playerY = (int)_player.transform.position.y;
                _loadFromMenu = false;
            }
            //Loading game move position to the saved game position
            if (!_newGame)
            {
                _movePoint.transform.position = new Vector3(GameSave.current.gameData.playerX, GameSave.current.gameData.playerY, 0);
                _player.GetComponent<Rigidbody2D>().position = new Vector3(GameSave.current.gameData.playerX, GameSave.current.gameData.playerY, 0);
            }
            //destination is the start if there's the new game
            else
            {
                _movePoint.transform.position = new Vector3(_originX, _originY, 0);
                _player.GetComponent<Rigidbody2D>().position = new Vector3(_originX, _originY, 0);
            }
            _newGame = false;
            _refresh = false;
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
        DontDestroyOnLoad(uiBackground);
    }
    /**
     * Start a new game from the beggining
     */
    public static void startNew()
    {
        //Defeated turned into false whenever a new game is started
        _defeated = false;
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
        //Generates a new list and update the scene adding them
        _managerItemList = ItemsList.getList();
        GameSave.current.itemList = _managerItemList;
        _refresh = true;
        _newGame = true;
    }

    private void Start()
    {
        //Initialize the enemy dictionary
        EnemyStatList.loadDictionary();
        //Initialize the gameSave to save
        if (!_loadStats)
        {
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
            GameSave.current.itemList = new List<Item>();
            _managerItemList = ItemsList.getList();
        }
        int[] playerStats = { playerHealth, playerAtk, playerDef, playerYellowKeys, playerGreenKeys, playerRedKeys, playerBlueKeys };
        //Call to the stats script to refresh the text with the player stats
        CanvasStatsScript.instance.updatePlayerStats(playerStats);
            //Add items to the local list
            loadItems();
            _player = GameObject.FindGameObjectWithTag("Player");
        
    }

    //Load items from the local list and turns to object added into the scene
    private void loadItems()
    {
        foreach (Item i in _managerItemList)
        {
            //Powerups
            if (i.getName().Contains("smallpot"))
            {
                GameObject obj = Instantiate(items[0], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                _objs.Add(obj);
            }
            if (i.getName().Contains("mediumpot"))
            {
                GameObject obj = Instantiate(items[1], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                _objs.Add(obj);
            }
            if (i.getName().Contains("bigpot"))
            {
                GameObject obj = Instantiate(items[2], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                _objs.Add(obj);
            }
            if (i.getName().Contains("atkgem"))
            {
                GameObject obj = Instantiate(items[3], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                _objs.Add(obj);
            }
            if (i.getName().Contains("defgem"))
            {
                GameObject obj = Instantiate(items[4], new Vector3(i.getX(),i.getY(),0), Quaternion.identity);
                _objs.Add(obj);
            }
            //Keys and doors
            if (i.getName().Contains("yellowkey"))
            {
                GameObject obj = Instantiate(items[5], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                _objs.Add(obj);
            }
            if (i.getName().Contains("yellowdoor"))
            {
                GameObject obj = Instantiate(items[6], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                _objs.Add(obj);
            }
            if (i.getName().Contains("greenkey"))
            {
                GameObject obj = Instantiate(items[7], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                _objs.Add(obj);
            }
            if (i.getName().Contains("greendoor"))
            {
                GameObject obj = Instantiate(items[8], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                _objs.Add(obj);
            }
            if (i.getName().Contains("redkey"))
            {
                GameObject obj = Instantiate(items[9], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                _objs.Add(obj);
            }
            if (i.getName().Contains("reddoor"))
            {
                GameObject obj = Instantiate(items[10], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                _objs.Add(obj);
            }
            if (i.getName().Contains("bluekey"))
            {
                GameObject obj = Instantiate(items[11], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                _objs.Add(obj);
            }
            if (i.getName().Contains("bluedoor"))
            {
                GameObject obj = Instantiate(items[12], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                _objs.Add(obj);
            }
            //Enemies
            if (i.getName().Contains("smallslime"))
            {
                GameObject obj = Instantiate(enemies[0], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                _objs.Add(obj);
            }
            if (i.getName().Contains("brown"))
            {
                GameObject obj = Instantiate(enemies[1], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                _objs.Add(obj);
            }
            if (i.getName().Contains("teal"))
            {
                GameObject obj = Instantiate(enemies[2], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                _objs.Add(obj);
            }
            if (i.getName().Contains("terror"))
            {
                GameObject obj = Instantiate(enemies[3], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                _objs.Add(obj);
            }
            if (i.getName().Contains("gelatinous"))
            {
                GameObject obj = Instantiate(enemies[4], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                _objs.Add(obj);
            }
            if (i.getName().Contains("treant"))
            {
                GameObject obj = Instantiate(enemies[5], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                _objs.Add(obj);
            }
            if (i.getName().Contains("watcher"))
            {
                GameObject obj = Instantiate(enemies[6], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                _objs.Add(obj);
            }
            if (i.getName().Contains("vengeful"))
            {
                GameObject obj = Instantiate(enemies[7], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                _objs.Add(obj);
            }
            if (i.getName().Contains("winged"))
            {
                GameObject obj = Instantiate(enemies[8], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                _objs.Add(obj);
            }
            if (i.getName().Contains("Dogra"))
            {
                GameObject obj = Instantiate(enemies[9], new Vector3(i.getX(), i.getY(), 0), Quaternion.identity);
                _objs.Add(obj);
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
        if (_player != null)
            _player.transform.position = new Vector3(GameSave.current.gameData.playerX, GameSave.current.gameData.playerY, 0);
        else
            _loadFromMenu = true;
        //Items in the scene
        _managerItemList = GameSave.current.itemList;
        
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
        GameSave.current.gameData.playerX = (int) _player.transform.position.x;
        GameSave.current.gameData.playerY = (int) _player.transform.position.y;
        //All the active objects are saved as items
        GameSave.current.itemList = new List<Item>();

        string[] objectNames = {"atkgem","defgem","smallpotion","mediumpotion","bigpotion",
                                "yellowkey","greenkey","redkey","bluekey","yellowdoor","greendoor","reddoor","bluedoor",
                                "smallslime","brownslime","tealslime","terrorbat","gelatinous","watcher","treant","vengefulspirit","wingeddemon","dogra"};
        foreach (GameObject obj in _objs)
        {
            //Check that object exist in the current scene
            if (obj != null)
            {
                //Check that object is active, if inactive it isn't saved
                if (obj.activeInHierarchy == true)
                {
                    string objectName = obj.tag.ToLower();
                    if (objectNames.Contains(objectName))
                        GameSave.current.itemList.Add(new Item(objectName, (int)obj.transform.position.x, (int)obj.transform.position.y));
                }
            }
        }
        _managerItemList = GameSave.current.itemList;
    }

    //Creates a new save to generate a new save when needed
    public static void refreshData()
    {
        //Stores player position from the save. To load from the title screen
        if(_loadFromMenu)
        {
            _loadedX = GameSave.current.gameData.playerX;
            _loadedY = GameSave.current.gameData.playerY;
        }
        if (_loadStats)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _movePoint = GameObject.FindGameObjectWithTag("PlayerMovePoint");
            _movePoint.transform.position = new Vector3(GameSave.current.gameData.playerX, GameSave.current.gameData.playerY, 0);
            _player.GetComponent<Rigidbody2D>().position = new Vector3(GameSave.current.gameData.playerX, GameSave.current.gameData.playerY, 0);
            int times = GameSave.current.gameData.playerY / 19;
            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + (19 * times), Camera.main.transform.position.z);
            instance.moveUIBackground(times, true);
            GameSave.current.setFloor(times);
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
        if (_player != null)
        {
            GameSave.current.gameData.playerX = (int)_player.transform.position.x;
            GameSave.current.gameData.playerY = (int)_player.transform.position.y;
        }
        //A new list with the items in the scene
        GameSave.current.itemList = new List<Item>();
        GameSave.current.itemList = _managerItemList;
        //Update the scene adding the objects loaded into the scene
        _refresh = true;
    }

    public bool initBattle()
    {
        //To not update the stats.
        battleActive = true;
        activateIcons();
        string[] enemyStats = { enemyName, enemyHealth.ToString(), enemyAtk.ToString(), enemyDef.ToString() };
        CanvasStatsScript.instance.updateEnemyStats(enemyStats);
        Camera.main.GetComponent<AudioSource>().PlayOneShot(combatSounds[Random.Range(0, combatSounds.Length)],0.4f);
        playerDmg = playerAtk - enemyDef;
        enemyDmg = enemyAtk - playerDef;
        if (enemyDmg < 0)
            enemyDmg = 0;
        enemyHealth -= playerDmg;

        CanvasStatsScript.instance.updateStat("elife", enemyHealth);
        if (enemyHealth <= 0)
        {
            enemyCollider.gameObject.SetActive(false);
            disactivateIcons();
            CanvasStatsScript.instance.cleanStats();
            //Credits when Dogra is defeated
            if (enemyName.Contains("Dogra"))
            {
                SceneManager.LoadScene("Credits");
            }
            battleActive = false;
            return false;
        }
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Hurt,0.7f);
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
    //Deactivate/activate player icons
    public void disablePlayerIcons()
    {
        gPlayer.gameObject.SetActive(false);
    }
    public void disableUIBackground()
    {
        gBackground.gameObject.SetActive(false);
    }
    //Move the UI background
    public void moveUIBackground(int times, bool load)
    {
        float originY = uiBackground.transform.position.y;
        if (load)
            originY = 0;
        uiBackground.transform.position = new Vector3(uiBackground.transform.position.x, originY + (19 * times), uiBackground.transform.position.z);
    }

    //Set and get floor if different from the current one
    public void setFloor(int floor)
    {
        _floor = floor;
    }

    public int getFloor()
    {
        return _floor;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0 && !_defeated)
        {
            CanvasStatsScript.instance.cleanAllStats();
            disablePlayerIcons();
            disactivateIcons();
            disableUIBackground();
            _defeated = true;
            SceneManager.LoadScene("GameOver");
        }
        if (_loadStats)
        {
            refreshData();
            
            _loadStats = false;
        }
    }
}
