using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfirmPanel : MonoBehaviour
{
    [Header("Level information")]
    [SerializeField]
    private string levelToLoad;
    public int level;
    private GameData gameData;
    private int distanceScore;

    [Header("UI Stuff")]
    [SerializeField]
    public Image[] stars;
    public Text distanceText;
    private int starsActive;

    void OnEnable()
    {
        gameData = FindObjectOfType<GameData>();
        LoadData();
        ActivateStars();
        SetText();
    }

    void LoadData()
    {
        if (gameData != null)
        {
            //starsActive = gameData.saveData.stars[level - 1];   // level - 1 
            //distanceScore = gameData.saveData.distanceScore[level - 1];// level - 1 
        }
    }

    void ActivateStars()
    {
        for (int i = 0; i < starsActive; i++)
        {
            stars[i].enabled = true;
        }
    }

    void SetText()
    {
        distanceText.text = "" + distanceScore;
    }

    public void Cancel()
    {
        this.gameObject.SetActive(false);
    }

    public void Play()
    {
        PlayerPrefs.SetInt("CurrentLevel", level - 1); //т.к. массив начинается с 0       
        SceneManager.LoadScene(levelToLoad);
    }
}
