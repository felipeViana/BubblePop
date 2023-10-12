using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonsManager : MonoBehaviour
{
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenTutorial()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");
    }
}
