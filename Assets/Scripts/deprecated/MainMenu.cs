using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject highscoreText;
    public IntVariable gameScore;
    // Start is called before the first frame update
    void Start()
    {
        SetHighscore();
    }

    public void GoToLoadScene()
    {
        SceneManager.LoadSceneAsync("LoadingScreen", LoadSceneMode.Single);
    }

    void SetHighscore()
    {
        highscoreText.GetComponent<TextMeshProUGUI>().text = "TOP: " + gameScore.previousHighestValue.ToString("D6");
    }

    public void ResetHighscore()
    {
        // reset selection after highscore is pressed
        GameObject eventSystem = GameObject.Find("EventSystem");
        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        gameScore.ResetHighestValue();
        // set highscore
        SetHighscore();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
