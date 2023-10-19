using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinPowerup : BasePowerup
{
    protected override void Start()
    {
        base.Start(); // call base class Start()
        this.type = PowerupType.Coin;
    }

    public AudioSource coinAudio;
    void PlayCoinSound()
    {
        // play the sound effect
        coinAudio.PlayOneShot(coinAudio.clip);
    }

    // Defining the abstract classes
    public UnityEvent<IPowerup> PowerupCollected;
    public override void SpawnPowerup()
    {
        PowerupCollected.Invoke(this);
    }
    public override void ApplyPowerup(MonoBehaviour i)
    {
        PlayerMovementW5 pm = (PlayerMovementW5)i;
        pm.incrementScore.Invoke(1);
    }

}
