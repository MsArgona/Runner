using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDotManager : MonoBehaviour
{
    [SerializeField] LevelDot[] levelDots;

    private int curIdnex;

    private void Start()
    {
        levelDots[0].Active = true;

        for (int i = 1; i < levelDots.Length; i++)
        {
            levelDots[i].Active = false;
        }
    }

    public void MoveNext()
    {
        if (curIdnex < levelDots.Length - 1)
        {
            levelDots[curIdnex].Active = false;
            curIdnex++;
            levelDots[curIdnex].Active = true;
        }
        else
        {
            levelDots[curIdnex].Active = false;
            curIdnex = 0;
            levelDots[curIdnex].Active = true;
        }
    }

    public void MoveBack()
    {
        if (curIdnex > 0)
        {
            levelDots[curIdnex].Active = false;
            curIdnex--;
            levelDots[curIdnex].Active = true;
        }
        else
        {
            levelDots[curIdnex].Active = false;
            curIdnex = levelDots.Length - 1;
            levelDots[curIdnex].Active = true;
        }         
    }
}
