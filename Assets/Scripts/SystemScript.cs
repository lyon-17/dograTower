using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

//Manages the system commands (new, load and save) interactions beetwen scenes and script.
public class SystemScript : MonoBehaviour
{
    public Canvas menuCanvas;

    public static bool showLoad;

    public Vector2 scrollPosition = Vector2.zero;

    public static List<GameSave> savedGames = new List<GameSave>();

    private void Start()
    {
        if (menuCanvas != null)
        {
            menuCanvas.gameObject.SetActive(false);
            showLoad = false;
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
            int game = 0;

            foreach (GameSave g in SaveLoad.savedGames)
            {
                if (GUILayout.Button("Game number: " + game + ". HP" + g.gameData.playerHealth + " atk " + g.gameData.playerAtk + " def " + g.gameData.playerDef))
                {
                    if (GameSave.current == null)
                        GameSave.current = new GameSave();
                    GameManager.instance.canvas.SetActive(true);
                    SceneManager.LoadScene(1);
                    GameSave.current = g;
                    GameManager.getStats();
                    GameManager.refreshData();
                    //Move on to game...

                }
                game++;
            }
            GUILayout.EndScrollView();
        }
        
    }

    //Restart the entire tower
    public void StartGame()
    {
        SceneManager.LoadScene("FirstTower");
        if (GameManager.instance != null)
        {
            //Shows the stats if it is disabled.
            if(!GameManager.instance.canvas.activeInHierarchy)
                GameManager.instance.canvas.SetActive(true);
            GameManager.startNew();
        }
    }   

    public static void SaveGame()
    {
        SaveLoad.Save();
    }

    public void LoadGame()
    {
        //Should call OnGUI
        showLoad = true;
    }

    public void startTutorial()
    {
        SceneManager.LoadScene("TutorialTower");
    }

    public void BackToMenu()
    {
        //remove numbers in the main menu
        CanvasStatsScript.instance.cleanAllStats();

        //Disable the icons to not be shown in the main menu.
        if (GameManager.instance != null)
            GameManager.instance.disablePlayerIcons();
        else if(TutorialScript.tutorial != null)
            TutorialScript.tutorial.disablePlayerIcons();

        SceneManager.LoadScene("Intro");
    }

    //Finaliza el juego
    public void FinishGame()
    {
        Application.Quit();
    }
}
