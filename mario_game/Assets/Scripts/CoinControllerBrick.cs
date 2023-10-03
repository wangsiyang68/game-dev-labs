using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinControllerBrick : MonoBehaviour
{

    public Animator coinAnimator;
    public AudioSource coinAudio;
    public BlockController blockController;
    public GameObject coin;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private bool hasDinged = false;
    void Update()
    {
        if (blockController.collided && !hasDinged)
        {
            // play the coin sound
            PlayCoinSound();
            hasDinged = true;

            // play the coin animation
            coinAnimator.Play("coin-spawn");

            //destroy the coin
            Destroy(coin, 1.1f);
        }
    }
    void PlayCoinSound()
    {
        // play the sound effect
        coinAudio.PlayOneShot(coinAudio.clip);
    }
}
