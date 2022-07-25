using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Positions")]
    public Transform PlayerPos;
    public Transform EndPos;
    private double onePercent;
    private int curDistance;

    [Header("Scores/UI")]
    [SerializeField] private GameObject[] Stars; //всегда 3
    [SerializeField] private GameObject WinText;
    [SerializeField] private GameObject NewBestText;
    public Text DistanceScore;
    private double distance;
    public Text ScoreValue;
    public int Score
    {
        get { return starScore; }
        set
        {
            starScore = value;
            StartTextAnim();
        }
    }
    private int starScore; //кол-во собранных светяшек
    private GameObject[] totalStars; //кол-во светяшек на сцене
    private int totalStarsCount;
    private Animator scoreTextAnim;

    [Header("Menus")]
    public GameObject GameMenu;
    public GameObject PauseMenu;

    private bool isPause = false;
    private bool isGameMenu = false;

    [Header("Muscic")]
    public AudioClip BgMusic;
    public AudioClip winMusic;

    private AudioSource audioSource;
    private PlayerController player;
    private GameData gameData;

    public static int CurLevel;

    private void Awake()
    {
        CurLevel = SceneManager.GetActiveScene().buildIndex;

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        gameData = FindObjectOfType<GameData>();
        Time.timeScale = 1;

        if (EndPos != null)
        {
            GameMenu.SetActive(false);
            PauseMenu.SetActive(false);

            distance = EndPos.position.x - PlayerPos.transform.position.x;
            onePercent = distance / 100;
            curDistance = 0;

            totalStars = GameObject.FindGameObjectsWithTag("Star");
            totalStarsCount = totalStars.Length;

            player = GameObject.Find("Player").GetComponent<PlayerController>();
            scoreTextAnim = ScoreValue.GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (EndPos == null)
            return;

        CheckingDistance();

        Audio();

        Pause();
    }

    public void WinMenu()
    {
        int tmp = totalStarsCount / 3; //делим на 3, т.к. всего звезд 3, чтобы знать, сколько стоит 1 звезда
        int activeStars = 0;

        if (starScore < tmp) activeStars = 0;
        else if (starScore >= tmp && starScore < 2 * tmp) { activeStars = 1; }
        else if (starScore >= 2 * tmp && starScore < totalStarsCount) { activeStars = 2; }
        else { activeStars = 3; } //можно получить только собрав все светяшки

        for (int i = 0; i < activeStars; i++)
        {
            Stars[i].SetActive(true);
        }

        //тут разделить для победа и получен новый лучший результат
        WinText.SetActive(true);
        NewBestText.SetActive(false);

        isGameMenu = true;
        GameMenu.SetActive(true);
        audioSource.Pause();
        Time.timeScale = 0;
    }

    private void CheckingDistance()
    {
        distance = EndPos.position.x - PlayerPos.transform.position.x;
        curDistance = 100 - (int)(distance / onePercent);
        DistanceScore.text = curDistance.ToString() + "%";

        if (PlayerPos.transform.position.x >= EndPos.transform.position.x)
        {
            WinMenu();
        }
    }

    private void Audio()
    {
        //ближе к концу заглушить музыку
        if (distance < 5 && audioSource.volume > 0)
        {
            audioSource.volume -= 0.03f;
        }
    }

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPause && !isGameMenu)
        {
            isPause = true;
            PauseMenu.SetActive(true);

            audioSource.Pause();
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPause)
        {
            Continue();
        }
    }

    public void PlayerIsDead()
    {
        Summarizing();
    }

    public void Replay()
    {
        if (AreNewScoresBest())
            SaveDatas();

        Score = 0;

        //audioSource.Stop();
        //audioSource.Play();
        SceneManager.LoadScene(CurLevel);
    }

    //подведение итогов прохождения уровня
    private void Summarizing()
    {
        //если новый результат лучший, то показать это
        if (AreNewScoresBest())
        {
            SaveDatas();
            WinMenu();   
        } //иначе сразу начинаем заново уровень
        else
        {
            SceneManager.LoadScene(CurLevel);
        }
    }

    private bool AreNewScoresBest()
    {
        //Debug.Log("curDistance: " + curDistance);
        //Debug.Log("gameData: " + gameData.GetBestDistance(CurLevel));

        if (curDistance > gameData.GetBestDistance(CurLevel))
            return true;

        return false;
    }

    private void SaveDatas()
    {
        if (gameData != null)
        {
            if (gameData.GetBestDistance(CurLevel) < curDistance)
                gameData.SetBestDistance(CurLevel, curDistance);
            if (gameData.GetBestStars(CurLevel) < starScore)
                gameData.SetBestStars(CurLevel, starScore);
        }
    }

    public void OpenMainMenu()
    {
        SaveDatas();

        SceneManager.LoadScene(0);
    }

    public void Continue()
    {
        isPause = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        audioSource.UnPause();
    }

    public void OpenLevel(int numLevel)
    {
        SceneManager.LoadScene(numLevel);
    }

    private void StartTextAnim()
    {
        if (scoreTextAnim != null)
        {
            ScoreValue.text = starScore.ToString();
            scoreTextAnim.SetTrigger("GotPoint");
        }
    }
}
