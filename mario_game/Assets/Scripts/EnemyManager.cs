using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        // other instructions
        GameManager.instance.flatten.AddListener(flattenChild);
        GameManager.instance.gameRestart.AddListener(GameRestart);
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
            child.GetComponent<EnemyMovement>().GameRestart();
        }
    }

    public void flattenChild(string name)
    {
        Debug.Log("my name is " + name);
        EnemyMovement childEnemyMovement = transform.Find(name).GetComponent<EnemyMovement>();
        childEnemyMovement.flatten();
    }
}
