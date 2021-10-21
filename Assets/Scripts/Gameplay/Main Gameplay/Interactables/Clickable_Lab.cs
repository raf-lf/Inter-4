using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable_Lab : Clickable
{
    [Header("Lab")]
    public GameObject confirmationWindow;

    public override void Click()
    {
        MenuOpenClose(true);
       // Debug.Log("Open");
        base.Click();
    }

    public void MenuOpenClose(bool on)
    {
        confirmationWindow.SetActive(on);

    }

    public void EndExpedition()
    {
        GameManager.scriptArena.EndExpedition(false);

    }
    
    public override void PlayerNearby(bool on)
    {
        base.PlayerNearby(on);
        MenuOpenClose(false);
      //  Debug.Log("Close");
    }

}
