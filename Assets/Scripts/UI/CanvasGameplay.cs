using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CanvasGameplay : MonoBehaviour
{
    public Animator overlayAnimator;

    private void OnEnable()
    {
        GameManager.scriptCanvas = this;
    }



}
