using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class GameManagerWeek3 : Singleton<GameManagerWeek3>
{
    // events
    public UnityEvent gameStart;
    public UnityEvent gameRestart;
    public UnityEvent<int> scoreChange;
    public UnityEvent gameOver;

    private int score = 0;

    void Start()
    {
        gameStart.Invoke();
        Time.timeScale = 1.0f;
        // subscribe to scene manager scene change
        SceneManager.activeSceneChanged += SceneSetup;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SceneSetup(Scene current, Scene next)
    {
        gameStart.Invoke();
        SetScore(score);
    }


    public void GameRestart()
    {
        // reset score
        score = 0;
        SetScore(score);
        gameRestart.Invoke();
        Time.timeScale = 1.0f;
        Lowpass();
    }

    public void IncreaseScore(int increment)
    {
        score += increment;
        SetScore(score);
    }

    public void SetScore(int score)
    {
        scoreChange.Invoke(score);
    }


    public void GameOver()
    {
        gameOver.Invoke();
        Time.timeScale = 0.0f;
        Lowpass();
    }

    public AudioMixerSnapshot unpaused;
    public AudioMixerSnapshot paused;

    void Lowpass()
    {
        if (Time.timeScale == 0)
        {
            paused.TransitionTo(.01f);
        }
        else
        {
            unpaused.TransitionTo(.01f);
        }
    }
}