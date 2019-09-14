using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishedMenu : MonoBehaviour {

    public string loadScene;
    private bool lockMode = false;
  //  public Button level1, level2, level3, level4, level5, level6;


    public void finished(GameObject finishedLevel , GameObject fullPanelPause)
    {
        
        finishedLevel.SetActive(true);
        fullPanelPause.SetActive(true);
        Time.timeScale = 0f;
      
    }


    public void BtnNextLevel()
    {

        if (NextLevel.levelRetat == 2)
        {
            NextLevel.levelRetat = 4;
            //level2.interactable = true;
            loadscene();
        }
        else if (NextLevel.levelRetat == 4)
        {
            NextLevel.levelRetat = 6;
           // level3.interactable = true;
            loadscene();
        }
        else if (NextLevel.levelRetat == 6)
        {
            NextLevel.levelRetat = 8;
           // level4.interactable = true;
            loadscene();
        }
        else if (NextLevel.levelRetat == 8)
        {
            NextLevel.levelRetat = 10;
           // level5.interactable = true;
            loadscene();
        }
        else if (NextLevel.levelRetat == 20)
        {
            NextLevel.levelRetat = 20;
           // level6.interactable = true;
            loadscene();
        }
        

    }

    public void loadscene()
    {
        if (!lockMode)
        {
            lockMode = true;
            Application.LoadLevel(loadScene);
        }
    }
}
