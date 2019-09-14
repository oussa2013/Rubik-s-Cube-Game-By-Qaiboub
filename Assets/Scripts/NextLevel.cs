using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour {

    public static int levelRetat;

    public string loadScene ;
    private bool lockMode = false;
    public Button level2, level3, level4, level5, level6;
    public Text  lev2, lev3, lev4, lev5, lev6;
    public Image  img2 , img3 , img4, img5, img6;
    public Sprite opendLevelIco;
    public Sprite lockIco;

    int  lvl2, lvl3, lvl4, lvl5, lvl6;

    private void Start()
    {
        if(PlayerPrefs.HasKey("lvl2"))
        lvl2 = PlayerPrefs.GetInt("lvl2");
        if (PlayerPrefs.HasKey("lvl3"))
        lvl3 = PlayerPrefs.GetInt("lvl3");
        if (PlayerPrefs.HasKey("lvl4"))
        lvl4 = PlayerPrefs.GetInt("lvl4");
        if (PlayerPrefs.HasKey("lvl5"))
        lvl5 = PlayerPrefs.GetInt("lvl5");
        if (PlayerPrefs.HasKey("lvl6"))
        lvl6 = PlayerPrefs.GetInt("lvl6");
    }


    private void Update()
    {
        if (lvl2 == 1)
        {
            level2.interactable = true;
            level2.image.overrideSprite = opendLevelIco;
            img2.overrideSprite = lockIco;
            lev2.enabled = true;
        }
           
        if (lvl3 == 1)
        {
            level3.interactable = true;
            level3.image.overrideSprite = opendLevelIco;
            img3.overrideSprite = lockIco;
            lev3.enabled = true;
        }

        if (lvl4 == 1)
        {
            level4.interactable = true;
            level4.image.overrideSprite = opendLevelIco;
            img4.overrideSprite = lockIco;
            lev4.enabled = true;
        }
        if (lvl5 == 1)
        {
            level5.interactable = true;
            level5.image.overrideSprite = opendLevelIco;
            img5.overrideSprite = lockIco;
            lev5.enabled = true;
        }
        if (lvl6 == 1)
        {
            level6.interactable = true;
            level6.image.overrideSprite = opendLevelIco;
            img6.overrideSprite = lockIco;
            lev6.enabled = true;
        }
    }

    public void BtnLevel1()
    {
        levelRetat = 2;
        nextLevel();
    }
    public void BtnLevel2()
    {
        levelRetat = 4;
        nextLevel();
    }

    public void BtnLevel3()
    {
        levelRetat = 6;
        nextLevel();
    }
    public void BtnLevel4()
    {
        levelRetat = 8;
        nextLevel();
    }

    public void BtnLevel5()
    {
        levelRetat = 10;
        nextLevel();
    }
    public void BtnLevel6()
    {
        levelRetat = 20;
        nextLevel();
    }

    public void nextLevel()
    {
        if (!lockMode)
        {
            lockMode = true;
            Application.LoadLevel(loadScene);
        }

    }
}
