using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    private Vector3[] scoreTextPosition = {
        new Vector3(-215, 80, 0),
        new Vector3(0, 10, 0)
        };
    private Vector3[] restartButtonPosition = {
        new Vector3(-120, 100, 0),
        new Vector3(0, -45, 0)
    };

    public GameObject scoreText;
    public Transform restartButton;
    public GameObject highscoreText;
    public GameObject gameOverCanvas;
    public IntVariable gameScore;
    public GameObject playPauseButton;

    // Start is called before the first frame update
    void Awake()
    {
        // other instructions
        // subscribe to events
        GameManager.instance.gameStart.AddListener(GameStart);
        GameManager.instance.gameOver.AddListener(GameOver);
        GameManager.instance.gameRestart.AddListener(GameStart);
        GameManager.instance.scoreChange.AddListener(SetScore);
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameStart()
    {
        // hide gameover Canvas
        Debug.Log("HUD GAME START CALLED");
        gameOverCanvas.SetActive(false);
        scoreText.transform.localPosition = scoreTextPosition[0];
        restartButton.localPosition = restartButtonPosition[0];
        playPauseButton.SetActive(true);
    }

    public void SetScore(int score)
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
    }


    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        scoreText.transform.localPosition = scoreTextPosition[1];
        restartButton.localPosition = restartButtonPosition[1];
        // set highscore
        highscoreText.GetComponent<TextMeshProUGUI>().text = "HIGHSCORE : " + gameScore.previousHighestValue.ToString("D6");
        // show
        highscoreText.SetActive(true);
        playPauseButton.SetActive(false);
    }
}
