using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Event_LoadScene : EventBase
{
    public string sceneName;

    public override void RunEvent()
    {
        base.RunEvent();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
