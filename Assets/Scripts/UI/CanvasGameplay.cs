using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGameplay : MonoBehaviour
{
    public Animator overlayAnimator;

    private void OnEnable()
    {
        GameManager.scriptCanvas = this;
    }


}
