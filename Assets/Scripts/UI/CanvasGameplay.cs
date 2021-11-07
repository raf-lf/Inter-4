using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CanvasGameplay : MonoBehaviour
{
    public Animator overlayAnimator;
    public Animator recallUnable;
    public Animator recallConfirmation;

    private void OnEnable()
    {
        GameManager.scriptCanvas = this;
    }
    public void RecallWindowClose()
    {
        recallConfirmation.SetBool("active", false);
        Time.timeScale = 1;
    }
    public void RecallReturnLab()
    {
        Time.timeScale = 1;
        GameManager.scriptArena.EndExpedition(false);
    }


}
