using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyManagerW5 : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        // other instructions
        //GameManager.instance.flatten.AddListener(flattenChild); //TODO
        //GameManager.instance.gameRestart.AddListener(GameRestart);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameRestart()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<EnemyMovementW5>().GameRestart();
        }
    }
}
