using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewBestMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] stars; //всегда 3
    [SerializeField] private TextMeshProUGUI progess;
    [SerializeField] private TextMeshProUGUI score;

    private void Awake()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(false);
        }
    }

    public void SetActive(int newProgress, int newScore, int maxScore, int newStars)
    {
        progess.text = newProgress.ToString() + "%";
        score.text = newScore.ToString() + "/" + maxScore.ToString();
        ActivateStars(newStars);
    }

    private void ActivateStars(int newStars)
    {
        for (int i = 0; i < newStars; i++)
        {
            stars[i].SetActive(true);
        }
    }
}
