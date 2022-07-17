using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class GameData : MonoBehaviour
{
    public readonly int countOfPlayableScenes = 3; //3 игровых уровня: от 1 до 3 влючительно. 0 - это главное меню

    private int[] distanceScore; //максимум 3 элемента, т.к. 3 уровня
    private int[] stars;

    private void Start()
    {
        distanceScore = new int[countOfPlayableScenes];
        stars = new int[countOfPlayableScenes];

        if (!PlayerPrefs.HasKey("distance" + GameManager.CurLevel))
        {
            for (int i = 1; i <= countOfPlayableScenes; i++)
                Save("distance", i, 0);
        }

        if (!PlayerPrefs.HasKey("stars" + GameManager.CurLevel))
        {
            for (int i = 1; i <= countOfPlayableScenes; i++)
                Save("stars", i, 0);
        }

        Load();
    }

    public void Save(string name, int numOfScene, int newValue)
    {
        name += numOfScene;
        PlayerPrefs.SetInt(name, newValue);
    }

    private void Load()
    {
        for (int i = 1; i <= countOfPlayableScenes; i++)
        {
            distanceScore[i - 1] = PlayerPrefs.GetInt("distance" + i);
            stars[i - 1] = PlayerPrefs.GetInt("stars" + i);
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
}
