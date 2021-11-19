using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Element_Hatch : LabElement
{
    public AudioClip sfxHatchOpen;
    public AudioClip sfxHatchClose;
    public AudioClip sfxExpeditionGo;

    public override void OpenElement()
    {
        if (!elementActive)
            GameManager.scriptAudio.PlaySfxSimple(sfxHatchOpen);

        base.OpenElement();

    }
    public override void CloseElement()
    {
        if (elementActive)
            GameManager.scriptAudio.PlaySfxSimple(sfxHatchClose);

        base.CloseElement();
    }

    public void StartExpedition()
    {
        StartCoroutine(ExpeditionSequence());
    }

    IEnumerator ExpeditionSequence()
    {
        GameManager.scriptAudio.PlaySfxSimple(sfxExpeditionGo);
        GameManager.scriptLab.animOverlay.SetBool("blackOut", true);
        GameManager.scriptAudio.FadeBgm(0, .05f);

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("arena", LoadSceneMode.Single);

    }
}
