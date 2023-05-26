using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

//Manages the system commands (new, load and save) interactions beetwen scenes and script.
public class SystemScript : MonoBehaviour
{
    public Canvas menuCanvas;

    //Hides or show the menu whenever load is clicked
    public static bool showLoad;

    public Vector2 scrollPosition = Vector2.zero;
    private Texture2D _blackTexture;

    public static List<GameSave> savedGames = new List<GameSave>();

    private void Start()
    {
        _blackTexture = Texture2D.blackTexture;
        Color[] pixels = Enumerable.Repeat(Color.black, _blackTexture.width * _blackTexture.height).ToArray();
        _blackTexture.SetPixels(pixels);
        _blackTexture.Apply();

        if (menuCanvas != null)
        {
            menuCanvas.gameObject.SetActive(false);
            showLoad = false;
            
            //Refresh the save list whenever the game is opened again

        }
    }

    void OnGUI()
    {
        //Shows only when load is clicked on, removed when exiting
        if (showLoad)
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(250), GUILayout.Height(200));
            GUILayout.Box("Select Save File");
            GUILayout.Space(10);
            GUIStyle btnStyle = new GUIStyle(GUI.skin.button);
            btnStyle.normal.background = _blackTexture;
            int game = 0;
            SaveLoad.Load();
            foreach (GameSave g in SaveLoad.savedGames)
            {
                string btnText = "Game number: " + game + ". HP" + g.gameData.playerHealth + " atk " + g.gameData.playerAtk + " def " + g.gameData.playerDef;
                if (GUILayout.Button(btnText, btnStyle))
                {
                    if (GameSave.current == null)
                    {
                        
                        GameSave.current = new GameSave();
                        GameSave.current = g;
                        GameSave.current.setLoad(true);
                    }
                    else
                    {
                        GameManager.instance.canvas.SetActive(true);
                        GameSave.current = g;
                        GameManager.getStats();
                        GameManager.refreshData();
                    }
                    //Move on to game...
                    SceneManager.LoadScene("FirstTower");
                }
                game++;
            }
            GUILayout.EndScrollView();
        }
        
    }

    //Restart the entire tower
    public void StartGame()
    {
        //Skips introduction if player select new game after a first time.
        if (GameManager.instance != null)
        {
            SceneManager.LoadScene("FirstTower");
            //Shows the stats if it is disabled.
            if(!GameManager.instance.canvas.activeInHierarchy)
                GameManager.instance.canvas.SetActive(true);
            GameManager.startNew();
        }
        else
        {
            SceneManager.LoadScene("Introduction");
        }
    }   

    //Save the current game
    public static void SaveGame()
    {
        SaveLoad.Save();
    }

    //Load the GUI with the games
    public void LoadGame()
    {
        //Should call OnGUI
        showLoad = true;
    }

    //Load the tutorial tower
    public void startTutorial()
    {
        SceneManager.LoadScene("TutorialTower");
    }

    //Return to the main menu
    public void BackToMenu()
    {
        //remove numbers in the main menu
        if(CanvasStatsScript.instance != null)
        CanvasStatsScript.instance.cleanAllStats();

        //Disable the icons to not be shown in the main menu.
        if (GameManager.instance != null)
        {
            GameManager.instance.disablePlayerIcons();
            GameManager.instance.disableUIBackground();
        }
        else if (TutorialScript.tutorial != null)
            TutorialScript.tutorial.disablePlayerIcons();

        SceneManager.LoadScene("Intro");
    }

    //Load the instruction scene
    public void Instructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    //Ends the game
    public void FinishGame()
    {
        Application.Quit();
    }
}
