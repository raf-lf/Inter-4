using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Element_Hatch : LabElement
{
    public void StartExpedition()
    {
        StartCoroutine(ExpeditionSequence());
    }

    IEnumerator ExpeditionSequence()
    {
        GameManager.scriptLab.animOverlay.SetBool("blackOut", true);
        GameManager.scriptAudio.FadeBgm(0, .05f);

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("arena", LoadSceneMode.Single);

    }
}
