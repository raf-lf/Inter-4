using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CutsceneClicker : MonoBehaviour, IPointerClickHandler
{
    public bool canSkip = true;
    private CutsceneManager managerCutscene;

    private void Awake()
    {
        managerCutscene = FindObjectOfType<CutsceneManager>();  
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (canSkip)
            managerCutscene.CheckNextFrame();
    }
}
