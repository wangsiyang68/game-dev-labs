using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButtonController : MonoBehaviour, IInteractiveButton
{
    // Start is called before the first frame update
    public string nextSceneName;

    public void ButtonClick()
    {
        Debug.Log("Change scene!");
        SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Single);
    }

}
