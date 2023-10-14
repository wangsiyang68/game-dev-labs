using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventTool : MonoBehaviour
{ 
    public UnityEvent anim;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TriggerIntEvent()
    {

        anim.Invoke(); // safe to invoke even without callbacks

    }
}