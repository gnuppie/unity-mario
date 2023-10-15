using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;

// later on, teach interface
public class RestartButtonControllerWeek4 : MonoBehaviour, IInteractiveButtonWeek4
{
    // implements the interface
    public PauseButtonController pbc;
    public void ButtonClick()
    {
        // Debug.Log("Onclick restart button");
        GameManagerWeek3.instance.GameRestart();
        pbc.image.sprite = pbc.pauseIcon;
        pbc.isPaused = !pbc.isPaused;
    }


    public void ReturnToMain()
    {
        Time.timeScale = 1.0f;
        GameManagerWeek3.instance.Lowpass();
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);

    }
}
