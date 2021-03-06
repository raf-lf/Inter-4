using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Element_Rocket : LabElement
{
    [Header("Dialogues")]
    public Dialogue dialogueCanLaunch;
    public Dialogue dialogueCantLaunch;
    public Dialogue dialogueLaunch;

    [Header("Components")]
    public Image rocketFill;
    public Animator animRocket;
    public GameObject buttonOk;
    public GameObject buttonNope;
    public TextMeshProUGUI textDataNeeded;

    [Header("Prerequisites")]
    public GameObject textVaccineOk;
    public GameObject textVaccineNope;
    public GameObject textDataOk;
    public GameObject textDataNope;


    public override void StartupElement()
    {
        base.StartupElement();

        UpdateRocket();
        
        textDataNeeded.text = LabManager.dataStored + " / " + LabManager.dataNeededPerStage[GameManager.currentGameStage];

        bool vaccineOk = false;

        if (LabManager.vaccineInRocket >= LabManager.vaccineTarget[GameManager.currentGameStage])
            vaccineOk = true;
        else
            vaccineOk = false;
        
        textVaccineOk.SetActive(vaccineOk);
        textVaccineNope.SetActive(!vaccineOk);


        bool dataOk = false;

        if (LabManager.dataStored >= LabManager.dataNeededPerStage[GameManager.currentGameStage])
            dataOk = true;
        else
            dataOk = false;

        textDataOk.SetActive(dataOk);
        textDataNope.SetActive(!dataOk);


        bool canLaunch = false;

        if (vaccineOk && dataOk)
        {
            canLaunch = true;
            GameManager.scriptDialogue.SetupDialogue(dialogueCanLaunch, DialogueType.oneShot);
        }
        else
        {
            canLaunch = false;
            GameManager.scriptDialogue.SetupDialogue(dialogueCantLaunch, DialogueType.oneShot);
        }

        buttonOk.SetActive(canLaunch);
        buttonNope.SetActive(!canLaunch);
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

        GameManager.scriptLab.monitor.LaunchRocket(true);

        animRocket.SetTrigger("launch");
        yield return new WaitForSeconds(5);
        GameManager.scriptLab.animOverlay.SetBool("blackOut", true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("cutscenes", LoadSceneMode.Single);
        GameManager.scriptLab.monitor.ResetFlags();
    }

    public void PhaseEnd()
    {
        LabManager.vaccineInRocket = 0;
        LabManager.dataStored -= LabManager.dataNeededPerStage[GameManager.currentGameStage];

        GameManager.briefingPlayed = false;

        if (GameManager.currentGameStage < 5)
            GameManager.currentGameStage++;

        SaveSystem.SaveGame();

    }
    public void UpdateRocket()
    {
        rocketFill.fillAmount = (float)LabManager.vaccineInRocket / (float)LabManager.vaccineTarget[GameManager.currentGameStage];

    }
    



}
