using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    // events
    public UnityEvent gameStart;
    public UnityEvent gameRestart;
    public UnityEvent<int> scoreChange;
    public UnityEvent gameOver;
    public UnityEvent<string> flatten;
    public UnityEvent<float> stompSfx;

    private int score = 0; // we don't want this to show up in the inspector
    // Start is called before the first frame update
    void Start()
    {
        gameStart.Invoke();
        Time.timeScale = 1.0f;
    }

    public void GameRestart()
    {
        // reset score
        score = 0;
        SetScore(score);
        gameRestart.Invoke();
        Time.timeScale = 1.0f;
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
        Time.timeScale = 0.0f;
        gameOver.Invoke();
    }

    public void ScoreByStomp(string name)
    {
        IncreaseScore(1);
        flatten.Invoke(name);
    }

    public void SetStompSfxPitch(float velocity)
    {
        stompSfx.Invoke(velocity);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
