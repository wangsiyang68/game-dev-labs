using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RestartButtonControllerW5 : MonoBehaviour, IInteractiveButton
{
    // implements the interface

    public UnityEvent gameRestart;
    public void ButtonClick()
    {
        gameRestart.Invoke();
    }

}
