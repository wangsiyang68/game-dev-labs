using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StompOnGoomba : MonoBehaviour
{

    public UnityEvent stomp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Stomp()
    {
        stomp.Invoke();
    }
}
