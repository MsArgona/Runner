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
    [SerializeField]
    private GameObject confirmPanel;

    private GameData gameData;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        myButton = GetComponent<Button>();
    }

    private void Start()
    {
        gameData = FindObjectOfType<GameData>();
        LoadData();
        ActivateStars();
        ShowLevel();
        //DecideSprite();
    }

    void LoadData()
    {
        if (gameData != null)
        {
            //starsActive = gameData.saveData.stars[level - 1];  вернуть
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

    public void ConfirmPanel(int level)
    {
        confirmPanel.GetComponent<ConfirmPanel>().level = level;
        confirmPanel.SetActive(true);
    }
}
