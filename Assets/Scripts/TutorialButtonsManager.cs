using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButtonsManager : MonoBehaviour
{
    public void BackButtonPressed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
