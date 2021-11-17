using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Element_Rocket : LabElement
{
    [Header("Dialogues")]
    public Dialogue dialogueNeedData;
    public Dialogue dialogueDataOk;
    public Dialogue dialogueCantLaunch;
    public Dialogue dialogueLaunch;


    [Header("Components")]
    public Image rocketFill;
    public Animator animRocket;
    public GameObject buttonDataMissing;
    public GameObject buttonDataOk;



    public override void StartupElement()
    {
        base.StartupElement();

        UpdateRocket();

        if (LabManager.dataStored >= LabManager.dataNeededPerStage[GameManager.currentGameStage])
        {
            GameManager.scriptDialogue.SetupDialogue(dialogueDataOk, DialogueType.oneShot);
            buttonDataMissing.SetActive(false);
            buttonDataOk.SetActive(true);
        }
        else
        {
            GameManager.scriptDialogue.SetupDialogue(dialogueNeedData, DialogueType.oneShot);
            buttonDataMissing.SetActive(true);
            buttonDataOk.SetActive(false);
        }
    }

    public void AttemptLaunch()
    {

        if (LabManager.dataStored >= LabManager.dataNeededPerStage[GameManager.currentGameStage])
        {
            GameManager.scriptAudio.PlaySfxSimple(GameManager.scriptLab.sfxClickOk);
            GameManager.scriptDialogue.SetupDialogue(dialogueLaunch, DialogueType.oneShot);
            StartCoroutine(LaunchSequence());

        }
        else
        {
            GameManager.scriptAudio.PlaySfxSimple(GameManager.scriptLab.sfxClickNo);
            GameManager.scriptDialogue.SetupDialogue(dialogueCantLaunch, DialogueType.oneShot);
        }

    }

    IEnumerator LaunchSequence()
    {
        PhaseEnd();
        GameManager.scriptLab.clickBlocker.SetActive(true);

        animRocket.SetTrigger("launch");
        yield return new WaitForSeconds(5);
        GameManager.scriptLab.animOverlay.SetBool("blackOut", true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("cutscenes", LoadSceneMode.Single);
    }

    public void PhaseEnd()
    {
        LabManager.vaccineInRocket = 0;

        if (GameManager.currentGameStage < 5)
            GameManager.currentGameStage++;

    }
    public void UpdateRocket()
    {
        rocketFill.fillAmount = LabManager.vaccineInRocket / LabManager.vaccineTarget[GameManager.currentGameStage];

    }
    



}
