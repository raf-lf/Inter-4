using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    [Header ("Components")]
    public Image fadeScreen;
    public GameObject menuObject;
    public AudioClip sfxClickOk;
    public AudioClip sfxClickCancel;


    [Header("Audio")]
    public GameObject[] soundsObjects = new GameObject[4];
    public GameObject[] musicObjects = new GameObject[4];

    [Header("Exit")]
    public GameObject exitConfirmation;

    private void Awake()
    {
        fadeScreen = GetComponent<Image>();
    }

    private void Start()
    {
        ToggleSounds(GameManager.soundsOn);
        ToggleMusic(GameManager.musicOn);
    }

    public void MenuToggle()
    {
        MenuOpenClose(!menuObject.activeInHierarchy);
    }

    public void MenuOpenClose(bool open)
    {
        ExitConfirmation(false);

        fadeScreen.enabled = open;
        menuObject.SetActive(open);


        if (open)
        {
            Time.timeScale = 0;
            GameManager.scriptAudio.PlaySfxSimple(sfxClickOk);
        }
        else
        {
            Time.timeScale = 1;
            GameManager.scriptAudio.PlaySfxSimple(sfxClickCancel);
        }


    }

    public void ExitConfirmation(bool on)
    {
        exitConfirmation.SetActive(on);

        if (on)
            GameManager.scriptAudio.PlaySfxSimple(sfxClickOk);
        else
            GameManager.scriptAudio.PlaySfxSimple(sfxClickCancel);
    }

    public void Exit()
    {
        StartCoroutine(ExitGame());
        GameManager.scriptAudio.PlaySfxSimple(sfxClickOk);
    }

    IEnumerator ExitGame()
    {
        if (SceneManager.GetActiveScene().name == "lab")
            GameManager.scriptLab.animOverlay.SetBool("blackOut", true);

        else if (SceneManager.GetActiveScene().name == "arena")
            GameManager.scriptCanvas.overlayAnimator.SetBool("blackOut", true);

        GameManager.scriptAudio.FadeBgm(0, .05f);
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("logo", LoadSceneMode.Single);
    }

    public void ToggleSounds(bool on)
    {
        if (!on)
            GameManager.scriptAudio.PlaySfxSimple(sfxClickCancel);

        if (on)
            AudioManager.volumeSfxModifier = 1;
        else
            AudioManager.volumeSfxModifier = 0;

        if (on)
            GameManager.scriptAudio.PlaySfxSimple(sfxClickOk);

        soundsObjects[0].SetActive(on);
        soundsObjects[1].SetActive(on);
        soundsObjects[2].SetActive(!on);
        soundsObjects[3].SetActive(!on);

        GameManager.soundsOn = on;


    }

    public void ToggleMusic(bool on)
    {
        if (on)
            GameManager.scriptAudio.PlaySfxSimple(sfxClickOk);
        else
            GameManager.scriptAudio.PlaySfxSimple(sfxClickCancel);


        if (on)
            AudioManager.volumeBgmModifier = 1;
        else
            AudioManager.volumeBgmModifier = 0;

        musicObjects[0].SetActive(on);
        musicObjects[1].SetActive(on);
        musicObjects[2].SetActive(!on);
        musicObjects[3].SetActive(!on);

        GameManager.musicOn = on;

    }

}
