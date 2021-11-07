using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element_Rocket : LabElement
{
    public Image rocketFill;
    public Dialogue dialogueNeedData;
    public Dialogue dialogueDataOk;
    public Dialogue dialogueCantLaunch;
    public Dialogue dialogueLaunch;

    public override void StartupElement()
    {
        base.StartupElement();

        UpdateRocket();

        if (LabManager.dataStored >= LabManager.dataNeededPerStage[GameManager.currentGameStage])
            GameManager.scriptDialogue.SetupDialogue(dialogueDataOk, DialogueType.oneShot);
        else
            GameManager.scriptDialogue.SetupDialogue(dialogueNeedData, DialogueType.oneShot);
    }

    public void AttemptLaunch()
    {

        if (LabManager.dataStored >= LabManager.dataNeededPerStage[GameManager.currentGameStage])
            GameManager.scriptDialogue.SetupDialogue(dialogueLaunch, DialogueType.oneShot);
        else
            GameManager.scriptDialogue.SetupDialogue(dialogueCantLaunch, DialogueType.oneShot);

    }

    public void UpdateRocket()
    {
        rocketFill.fillAmount = LabManager.vaccineInRocket / LabManager.vaccineTarget[GameManager.currentGameStage];

    }
    



}
