using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class GameData : MonoBehaviour
{
    public readonly int CountOfPlayableScenes = 3; //3 игровых уровня: от 1 до 3 влючительно. 0 - это главное меню

    private int[] distanceScore; //максимум 3 элемента, т.к. 3 уровня
    private int[] stars; //кол-во полученных звезд (всего 3)
    private int[] totalLumin; //общее кол-во сверяшек на поле
    private int[] luminScore; //кол-во собранных светяшек

    private int curLevel;

    private void Awake()
    {
        distanceScore = new int[CountOfPlayableScenes];
        stars = new int[CountOfPlayableScenes];
        totalLumin = new int[CountOfPlayableScenes];
        luminScore = new int[CountOfPlayableScenes];

        CheckDatas();
       // ResetAllDatas();

        Load();
    }
    private void Start()
    {
        curLevel = GameManager.CurLevel;
    }

    //УДАЛИТЬ ЭТОТ МЕТОД
    public void DELETE()
    {
        ResetAllDatas();
    }

    public void Save(string name, int numOfScene, int newValue)
    {
        name += numOfScene;
        PlayerPrefs.SetInt(name, newValue);
    }

    private void CheckDatas()
    {
        for (int i = 1; i <= CountOfPlayableScenes; i++)
        {
            if (!PlayerPrefs.HasKey("distance" + i))
                Save("distance", i, 0);
            if (!PlayerPrefs.HasKey("stars" + i))
                Save("stars", i, 0);
            if (!PlayerPrefs.HasKey("totalLumin" + i))
                Save("totalLumin", i, 0);
            if (!PlayerPrefs.HasKey("luminScore" + i))
                Save("luminScore", i, 0);
        }
    }

    private void Load()
    {
        for (int i = 1; i <= CountOfPlayableScenes; i++)
        {
            distanceScore[i - 1] = PlayerPrefs.GetInt("distance" + i);
            stars[i - 1] = PlayerPrefs.GetInt("stars" + i);
            totalLumin[i - 1] = PlayerPrefs.GetInt("totalLumin" + i);
            luminScore[i - 1] = PlayerPrefs.GetInt("luminScore" + i);
        }
    }
    //Сбросить все рекорды
    private void ResetAllDatas()
    {
        for (int i = 1; i <= CountOfPlayableScenes; i++)
        {
            Save("distance", i, 0);
            Save("stars", i, 0);
            Save("totalLumin", i, 0);
            Save("luminScore", i, 0);
        }
    }

    public int GetBestDistance(int numScene)
    {
        return distanceScore[numScene - 1];
    }

    public void SetBestDistance(int numScene, int newBestDistance)
    {
        distanceScore[numScene - 1] = newBestDistance;
        Save("distance", numScene, newBestDistance);
    }

    public int GetBestStars(int numScene)
    {
        return stars[numScene - 1];
    }

    public void SetBestStars(int numScene, int newBestStars)
    {
        stars[numScene - 1] = newBestStars;
        Save("stars", numScene, newBestStars);
    }

    public int GetBestLumin(int numScene)
    {
        return luminScore[numScene - 1];
    }

    public void SetBestLumin(int numScene, int newBestLumin)
    {
        luminScore[numScene - 1] = newBestLumin;
        Save("luminScore", numScene, newBestLumin);
    }

    public int GetTotalLumin(int numScene)
    {
        return totalLumin[numScene - 1];
    }

    public void SetTotalLumin(int numScene, int newTotalLumin)
    {
        totalLumin[numScene - 1] = newTotalLumin;
        Save("totalLumin", numScene, newTotalLumin);
    }
}
