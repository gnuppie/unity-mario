using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    private Vector3[] scoreTextPosition = {
        new Vector3(-663.0f, 472.0f, 0.0f),
        new Vector3(0.0f, 0.0f, 0.0f)
        };
    private Vector3[] restartBtnPosition = {
        new Vector3(899.0f, 485.0f, 0.0f),
        new Vector3(0.0f, -100.0f, 0.0f)
    };

    public GameObject scoreText;
    public Transform restartBtn;

    public GameObject gameOverScreen;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameStart()
    {
        // hide gameover panel
        gameOverScreen.SetActive(false);
        scoreText.transform.localPosition = scoreTextPosition[0];
        restartBtn.localPosition = restartBtnPosition[0];
    }

    public void SetScore(int score)
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
    }


    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        scoreText.transform.localPosition = scoreTextPosition[1];
        restartBtn.localPosition = restartBtnPosition[1];
    }
}
