using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int CurLevel;

    [Header("Scores/UI")]
    [SerializeField] private GameObject[] stars; //всегда 3
    private int gotStars; //всегда макс 3
    [SerializeField] private Text distanceScore;
    private double distance;
    [SerializeField] private Text scoreValue;
    public int Score
    {
        get { return luminScore; }
        set
        {
            luminScore = value;
            StartTextAnim();
        }
    }

    private int luminScore; //кол-во собранных светяшек
    private GameObject[] totalLumin; //кол-во светяшек на сцене
    private int totalLuminCount;
    private Animator scoreTextAnim;

    [Header("Menus")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject levelComplMenu;
    [SerializeField] private GameObject newBestMenu;

    private bool isPause = false;
    private bool isMenuActive = false;

    [Header("Muscic")]
    [SerializeField] private AudioClip bgMusic;
    [SerializeField] private AudioClip winMusic;

    private AudioSource audioSource;
    private PlayerController player;
    private GameData gameData;

    private Transform playerPos;
    private Transform endPos;
    private double onePercent;
    private int curDistance;

    private bool isPlayerDead;

    private void Awake()
    {
        CurLevel = SceneManager.GetActiveScene().buildIndex;

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        Time.timeScale = 1;
        isPlayerDead = false;

        ResetScores();
        ResetMenu();

        FindComponentsAndObjs();
        FindAllLumins();

        //Вычисляем чему равен 1% от общего расстояния
        distance = endPos.position.x - playerPos.transform.position.x;
        onePercent = distance / 100;
    }

    void Update()
    {
        if (endPos == null)
            return;

        CheckingDistance();

        Audio();

        Pause();
    }

    public void PlayerIsDead()
    {
        isPlayerDead = true;

        Summarizing();
    }

    private void ResetScores()
    {
        luminScore = 0;
        Score = 0;
        gotStars = 0;
        curDistance = 0;
    }

    private void ResetMenu()
    {
        pauseMenu.SetActive(false);
        newBestMenu.SetActive(false);
        levelComplMenu.SetActive(false);
    }

    private void FindAllLumins()
    {
        totalLumin = GameObject.FindGameObjectsWithTag("Star");
        totalLuminCount = totalLumin.Length;
        gameData.SetTotalLumin(CurLevel, totalLuminCount);
    }

    private void CountStars()
    {
        int tmp = totalLuminCount / 3; //делим на 3, т.к. всего звезд 3, чтобы знать, сколько стоит 1 звезда

        if (luminScore < tmp) gotStars = 0;
        else if (luminScore >= tmp && luminScore < 2 * tmp) { gotStars = 1; }
        else if (luminScore >= 2 * tmp && luminScore < totalLuminCount) { gotStars = 2; }
        else { gotStars = 3; } //можно получить только собрав все светяшки

        ActivateStars(gotStars);
    }

    private void ActivateStars(int gotStars)
    {
        //от 0 до 3
        for (int i = 0; i < gotStars; i++)
        {
            stars[i].SetActive(true);
        }
    }

    private void WinMenu()
    {
        levelComplMenu.SetActive(true);
        newBestMenu.SetActive(false);

        Summarizing();

        isMenuActive = true;
        audioSource.Pause();
        Time.timeScale = 0;
    }

    private void ShowNewBestMenu()
    {
        levelComplMenu.SetActive(false);
        newBestMenu.SetActive(true);

        isMenuActive = true;
        audioSource.Pause();
        Time.timeScale = 0;
    }

    private void CheckingDistance()
    {
        distance = endPos.position.x - playerPos.transform.position.x;
        curDistance = 100 - (int)(distance / onePercent);
        distanceScore.text = curDistance.ToString() + "%";

        if (playerPos.transform.position.x >= endPos.transform.position.x)
        {
            CountStars();
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
        if (Input.GetKeyDown(KeyCode.Escape) && !isPause && !isMenuActive)
        {
            isPause = true;
            pauseMenu.SetActive(true);

            audioSource.Pause();
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPause)
        {
            Continue();
        }
    }

    public void Replay()
    {
        Summarizing();

        // Score = 0;

        SceneManager.LoadScene(CurLevel);
    }

    //подведение итогов прохождения уровня
    private void Summarizing()
    {
        //если игрок собрал больше люминов, чем раньше, то пересчитываем кол-во звезд за уровень
        if (AreNewLuminScoreBest())
        {
            CountStars();
            SaveBestLuminAndStarScore();
        }

        if (AreNewDistanceBest())
        {
            SaveBestDistance();

            if (isPlayerDead) //если игрок прошел дальше, но не дошел до конца уровня, то сообщаем ему, что он побил свой рекорд
            {
                ShowNewBestMenu();
            }
            else  //иначе сразу начинаем заново уровень
            {
                SceneManager.LoadScene(CurLevel);
            }
        }
    }

    //Если прошел дальше, то это новый лучший результат
    private bool AreNewDistanceBest()
    {
        if (curDistance > gameData.GetBestDistance(CurLevel))
            return true;

        return false;
    }

    private bool AreNewLuminScoreBest()
    {
        if (luminScore > gameData.GetBestLumin(CurLevel))
            return true;

        return false;
    }

    private void SaveBestLuminAndStarScore()
    {
        if (gameData != null)
        {
            gameData.SetBestStars(CurLevel, gotStars);
            gameData.SetBestLumin(CurLevel, luminScore);
        }
    }

    private void SaveBestDistance()
    {
        if (gameData != null)
            gameData.SetBestDistance(CurLevel, curDistance);
    }

    public void OpenMainMenu()
    {
        Summarizing();

        SceneManager.LoadScene(0);
    }

    public void Continue()
    {
        isPause = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        audioSource.UnPause();
    }

    public void NextLevel()
    {
        Summarizing();

        if (CurLevel++ > gameData.CountOfPlayableScenes)
        { //если нет дальше уровня, то возвращаться в главное меню
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(CurLevel++);
        }
    }

    public void OpenLevel(int numLevel)
    {
        SceneManager.LoadScene(numLevel);
    }

    private void StartTextAnim()
    {
        if (scoreTextAnim != null)
        {
            scoreValue.text = luminScore.ToString();
            scoreTextAnim.SetTrigger("GotPoint");
        }
    }

    //Находим все необходимые компоненты и объекты на сцене
    private void FindComponentsAndObjs()
    {
        gameData = FindObjectOfType<GameData>();
        scoreTextAnim = scoreValue.GetComponent<Animator>();

        player = GameObject.Find("Player").GetComponent<PlayerController>();
        playerPos = player.transform;
        endPos = GameObject.FindGameObjectWithTag("End").transform;
    }
}
