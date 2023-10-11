using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsManager : MonoBehaviour
{
    public void OnRestartButtonPressed()
    {
        GameController.Instance.RestartLevel();
    }

    public void OnContinueButtonPressed()
    {
        GameController.Instance.AdvanceLevel();
    }

}
