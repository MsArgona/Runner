using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LevelButton : MonoBehaviour
{
    [Header("Active Stuff")]
    private Image buttonImage;
    private Button myButton;
    private int starsActive;

    [Header("Level UI")]
    [SerializeField] private Image[] stars;
    [SerializeField] private int level;
    [SerializeField] private TextMeshProUGUI percent;
    [SerializeField] private TextMeshProUGUI luminScore;
    [SerializeField] private Slider levelComplSlider;

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
    }

    void LoadData()
    {
        if (gameData != null)
        {
            starsActive = gameData.GetBestStars(level);
            percent.text = gameData.GetBestDistance(level).ToString();
            luminScore.text = gameData.GetBestLumin(level).ToString() + "/" + gameData.GetTotalLumin(level).ToString();

            levelComplSlider.value = gameData.GetBestDistance(level);
        }
    }

    void ActivateStars()
    {
        for (int i = 0; i < starsActive; i++)
        {
            stars[i].enabled = true;
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(level);
    }
}
