using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerupManager : MonoBehaviour
{
    public UnityEvent<IPowerup> PowerupAffectsPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void FilterAndCast(IPowerup powerup)
    {
        //Coin = 0,
        //MagicMushroom = 1,
        //OneUpMushroom = 2,
        //StarMan = 3
        Debug.Log("Filtering and Casting...");
        switch ((int) powerup.powerupType)
        {
            case 0:
                PowerupAffectsPlayer.Invoke(powerup);
                break;
            case 1:
                //GameManagerW5.instance.powerupAffectsPlayer.Invoke(powerup);
                PowerupAffectsPlayer.Invoke(powerup);
                break;
            case 2:
                break;
            case 3:
                break;


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
