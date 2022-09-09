using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    public void Replay() => gameManager.Replay();

    public void OpenMainMenu() => gameManager.OpenMainMenu();

    public void Continue() => gameManager.Continue();

    public void NextLevel() => gameManager.NextLevel();
}
