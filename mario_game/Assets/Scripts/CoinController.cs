using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{

    public Animator coinAnimator;
    public AudioSource coinAudio;
    public QuestionBlockController questionBlockController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private bool hasDinged = false;
    void Update()
    {
        if (questionBlockController.collided && !hasDinged)
        {
            // play the coin sound
            PlayCoinSound();
            hasDinged = true;

            // play the coin animation
            coinAnimator.Play("coin-spawn");
        }
    }
    void PlayCoinSound()
    {
        // play the sound effect
        coinAudio.PlayOneShot(coinAudio.clip);
    }
}
