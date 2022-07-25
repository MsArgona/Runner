using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [Header("Active Stuff")]
    //[SerializeField]
    //private bool isActive;
   // [SerializeField]
   // private Sprite activeSprite;
   // [SerializeField]
   // private Sprite lockedSprite;
    private Image buttonImage;
    private Button myButton;
    private int starsActive;

    [Header("Level UI")]
    [SerializeField]
    private Image[] stars;
    [SerializeField]
    private Text levelText;
    [SerializeField]
    private int level;

    private GameData gameData;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        myButton = GetComponent<Button>();
    }

    private void Start()
    {
        gameData = FindObjectOfType<GameData>();
        ShowLevel();
        LoadData();
        ActivateStars();
       
        //DecideSprite();
    }

    void LoadData()
    {
        if (gameData != null)
        {          
            starsActive = gameData.GetBestStars(level);
        }
    }

    void ActivateStars()
    {
        for (int i = 0; i < starsActive; i++)
        {
             stars[i].enabled = true; 
        }    
    }

    //void DecideSprite()
    //{
    //    if (isActive)
    //    {
    //        buttonImage.sprite = activeSprite;
    //        myButton.enabled = true;
    //        levelText.enabled = true;
    //    }
    //    else
    //    {
    //        buttonImage.sprite = lockedSprite;
    //        myButton.enabled = false;
    //        levelText.enabled = false;
    //    }

    //}

    public void LoadLevel()
    {
        SceneManager.LoadScene(level);
    }

    void ShowLevel()
    {
        levelText.text = "" + level;
    }
}
