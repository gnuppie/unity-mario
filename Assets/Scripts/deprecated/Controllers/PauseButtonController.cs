
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseButtonControllerWeek4 : MonoBehaviour, IInteractiveButtonWeek4
{
    [System.NonSerialized]
    public bool isPaused = false;
    public Sprite pauseIcon;
    public Sprite playIcon;

    [System.NonSerialized]
    public Image image;
    public HUDManager hud;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonClick()
    {
        Time.timeScale = isPaused ? 1.0f : 0.0f;
        isPaused = !isPaused;
        if (isPaused)
        {
            image.sprite = playIcon;
        }
        else
        {
            image.sprite = pauseIcon;
        }
        GameManagerWeek3.instance.Lowpass();
        hud.PausePlay();
    }


    public void ReturnToMain()
    {
        Time.timeScale = 1.0f;
        GameManagerWeek3.instance.Lowpass();
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);

    }
}
