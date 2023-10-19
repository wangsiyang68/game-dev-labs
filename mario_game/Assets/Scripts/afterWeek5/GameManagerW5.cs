using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManagerW5 : MonoBehaviour
{
    // events
    //public UnityEvent gameStart;
    //public UnityEvent gameRestart;
    //public UnityEvent<int> scoreChange;
    //public UnityEvent gameOver;
    //public UnityEvent<string> flatten;
    //public UnityEvent<float> stompSfx;
    //public IntVariable gameScore;

    public IntVariable gameScore;

    // private int score = 0; // we don't want this to show up in the inspector
    // Start is called before the first frame update
    public UnityEvent gameStart;
    public UnityEvent updateScore;
    // public UnityEvent<BasePowerup> powerupAffectsPlayer;
    void Start()
    {
        gameStart.Invoke();
        Time.timeScale = 1.0f;
        gameScore.Value = 0;
        // subscribe to scene manager scene change
        // SceneManager.activeSceneChanged += SceneSetup;
    }
    public void SceneSetup(Scene current, Scene next)
    {
        gameStart.Invoke();
        //SetScore(gameScore.Value);
    }

    public void GameRestart()
    {
        // reset score in HUD Manager
        // since need to reset score before displaying the score
        Time.timeScale = 1.0f;
    }

    public void IncreaseScore(int increment)
    {
        gameScore.ApplyChange(increment);
        updateScore.Invoke();
    }

    //public void SetScore(int score)
    //{
    //    scoreChange.Invoke(score);
    //}

    public void GameOver()
    {
        Time.timeScale = 0.0f;
    }

    //public void ScoreByStomp(string name)
    //{
    //    IncreaseScore(1);
    //    flatten.Invoke(name);
    //}

    //public void SetStompSfxPitch(float velocity)
    //{
    //    stompSfx.Invoke(velocity);
    //}
    //// Update is called once per frame
    //void Update()
    //{

    //}
}
