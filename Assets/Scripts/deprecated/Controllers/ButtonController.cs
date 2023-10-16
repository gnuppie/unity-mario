using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RestartClick()
    {
        GameManagerWeek3.instance.GameRestart();
    }


    public void ReturnToMain()
    {
        Time.timeScale = 1.0f;
        GameManagerWeek3.instance.Lowpass();
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);

    }
}
