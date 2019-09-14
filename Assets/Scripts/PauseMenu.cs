using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject pauseMenu;
    public GameObject fullPanelPause;
    public Button pausebtn;
    public Sprite pauseIco;
    public Sprite resumeIco;

    public void Resumebtn()
    {
        pausebtn.image.overrideSprite = pauseIco;
        pauseMenu.SetActive(false);
        fullPanelPause.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

   public void Pausebtn()
    {
        if (!GameIsPaused)
        {
            pausebtn.image.overrideSprite = resumeIco;
            pauseMenu.SetActive(true);
            fullPanelPause.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
        else if (GameIsPaused)
        {
            Resumebtn();
        }
    }

    public void Menubtn()
    {


    }

    public void QuitGame()
    {
        Application.Quit();
    }



    //------------backbtn---------------
    public string loadScene;
    private bool lockMode = false;

    public void Backbtn()
    {
        if (!lockMode)
        {
            lockMode = true;
            Application.LoadLevel(loadScene);
        }

    }

}
