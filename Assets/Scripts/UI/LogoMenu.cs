using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogoMenu : MonoBehaviour
{
    [Header("Components")]
    public Animator animOverlay;
    public GameObject menuObject;
    public GameObject creditObject;
    public RectTransform creditContent;

    public AudioClip sfxOk;
    public AudioClip sfxNope;
    public AudioClip sfxLeave;

    [Header("Credits")]
    public float startY;
    public float endY;

    public float scrollSpeed;
    public float fastModifier;

    public bool playing;
    public bool fast;
    public bool reverse;

    private void Awake()
    {
        SaveSystem.LoadPreferences();
        
    }

    public void ClickStartGame()
    {
        StartCoroutine(StartGame());

        GameManager.scriptAudio.PlaySfxSimple(sfxOk);
    }

    private IEnumerator StartGame()
    {
        SaveSystem.LoadGame();

        GameManager.scriptAudio.FadeBgm(0, .05f);
        animOverlay.SetBool("blackOut", true);
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("cutscenes", LoadSceneMode.Single);

    }

    public void ClickExitGame()
    {
        StartCoroutine(ExitGame());

        GameManager.scriptAudio.PlaySfxSimple(sfxLeave);
    }

    private IEnumerator ExitGame()
    {
        GameManager.scriptAudio.FadeBgm(0, .05f);
        animOverlay.SetBool("blackOut", true);
        yield return new WaitForSeconds(2);

        Application.Quit();

    }

    public void ClickEraseData()
    {
        SaveSystem.EraseData();
        GameManager.scriptAudio.PlaySfxSimple(sfxLeave);
    }

    public void OpenCloseCredits(bool open)
    {

        creditContent.anchoredPosition = new Vector2(0, startY);

        if(open)
            GameManager.scriptAudio.PlaySfxSimple(sfxOk);
        else
            GameManager.scriptAudio.PlaySfxSimple(sfxNope);

        menuObject.SetActive(!open);
        creditObject.SetActive(open);

        playing = open;

    }

    public void CreditsRoll()
    {
        if(playing)
        {
            float currentSpeed = scrollSpeed;

            if(fast)
                currentSpeed *= fastModifier;
            if(reverse)
                currentSpeed = Mathf.Abs(currentSpeed) * -1;

            if(currentSpeed >0)
            {
                if(creditContent.anchoredPosition.y <= endY)
                    creditContent.anchoredPosition += new Vector2(0, currentSpeed);
            }
            else
            {
                if (creditContent.anchoredPosition.y >= startY)
                    creditContent.anchoredPosition += new Vector2(0, currentSpeed);
            }
        }

    }

    public void ClickPlay()
    {
        GameManager.scriptAudio.PlaySfxSimple(sfxOk);
        playing = !playing;
    }
    public void ClickFast()
    {
        GameManager.scriptAudio.PlaySfxSimple(sfxOk);
        fast = !fast;
    }
    public void ClickReverse()
    {
        GameManager.scriptAudio.PlaySfxSimple(sfxOk);
        reverse = !reverse;
    }

    private void Update()
    {
        CreditsRoll();
    }

}
