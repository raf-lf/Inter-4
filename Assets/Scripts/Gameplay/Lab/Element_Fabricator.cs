using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Element_Fabricator : LabElement
{
    [Header("Buttons")]
    public Button antigenButton;
    public Button[] componentButton = new Button[5];

    [Header("Counters")]
    public TextMeshProUGUI antigenReserveQty;
    public TextMeshProUGUI antigenNeededQty;
    public TextMeshProUGUI[] componentReserveQty = new TextMeshProUGUI[4];
    public TextMeshProUGUI[] componentNeededQty = new TextMeshProUGUI[6];

    [Header("Bars")]
    public Image antigenBarFill;

    [Header("Resources")]
    public float currentAntigenTarget;
    public float antigenAdded;
    public float antigenReserve;
    public int[] componentTarget = new int[6];
    public int[] componentAdded = new int[6];
    public int[] componentReserve = new int[6];

    [Header("Vaccine Efficiency")]
    public float mistakePennalty;
    public Dialogue dialogueHasEnoughVaccine;
    public Dialogue dialoguegreatVaccine;
    public Dialogue dialoguegoodVaccine;
    public Dialogue dialoguebadVaccine;
    public Button endBatchButton;
    public Image rocketFill;
    public TextMeshProUGUI rocketFillText;

    [Header("Audio")]
    public AudioClip[] sfxComponentAdd = new AudioClip[0];
    public AudioClip sfxDillutantAdd;
    public AudioClip sfxEndBatch;

    public override void StartupElement()
    {
        base.StartupElement();
        GameManager.scriptLab.SetComponentTargets();

        int totalComponents = 0;

        for (int i = 0; i < componentTarget.Length; i++)
        {
            if (i <= 3)
            {
                componentTarget[i] = LabManager.componentcurrentTarget[i];
                totalComponents += LabManager.componentcurrentTarget[i];
            }
            else if (i == 4)
            {
                componentTarget[i] = LabManager.componentdillutant[GameManager.currentGameStage];
                totalComponents += LabManager.componentdillutant[GameManager.currentGameStage];
            }
            else if (i == 5)
            {
                componentTarget[i] = totalComponents / 5;
            }

        }
        BeginBatch();
    }

    
    public void BeginBatch()
    {
        endBatchButton.interactable = false;

        currentAntigenTarget = LabManager.antigenTarget[GameManager.currentGameStage];

        antigenReserve = LabManager.antigenStored;

        antigenAdded = 0;

        for (int i = 0; i < componentAdded.Length; i++)
        {
            componentAdded[i] = 0;
        }

        for (int i = 0; i <=3; i++)
        {
            componentReserve[i] = LabManager.componentStored[i];
        }

        UpdateValues();
        UpdateRocket();

    }

    public void EndBatch()
    {
        if (LabManager.vaccineInRocket < LabManager.vaccineTarget[GameManager.currentGameStage])
        {
            float mistakeTotal = 0;

            for (int i = 0; i < componentAdded.Length; i++)
            {
                mistakeTotal += Mathf.Abs(componentAdded[i] - componentTarget[i]);

            }

            mistakeTotal *= mistakePennalty;

            float finalAmmount = LabManager.antigenTarget[GameManager.currentGameStage] * (1 - mistakeTotal);

            
            finalAmmount = Mathf.Clamp(finalAmmount, 0, LabManager.vaccineTarget[GameManager.currentGameStage] - LabManager.vaccineInRocket);

            LabManager.vaccineInRocket += (int)finalAmmount;
            LabManager.antigenStored -= (int)antigenAdded;

            for (int i = 0; i < LabManager.componentStored.Length; i++)
            {
                LabManager.componentStored[i] -= componentAdded[i];

            }

            GameManager.scriptAudio.PlaySfx(sfxEndBatch, 1, new Vector2(.8f, 1.2f), GameManager.scriptAudio.sfxSource);

            UpdateRocket();

            BeginBatch();

        }
        else
            GameManager.scriptDialogue.SetupDialogue(dialogueHasEnoughVaccine, DialogueType.oneShot);

    }

    public void UpdateRocket()
    {
        rocketFillText.text = LabManager.vaccineInRocket + "/" + LabManager.vaccineTarget[GameManager.currentGameStage];
        rocketFill.fillAmount = (float)LabManager.vaccineInRocket / (float)LabManager.vaccineTarget[GameManager.currentGameStage];

    }
    public void AddAntigen(float value)
    {
        antigenReserve -= value;
        antigenAdded += value;
        /*
        antigenReserve = Mathf.Clamp(antigenReserve - value, 0, Mathf.Infinity);
        antigenAdded = Mathf.Clamp(antigenReserve + value, 0, currentAntigenTarget);
        */
        UpdateValues();
    }

    public void UseComponent(int componentId)
    {

        AudioClip sfxToPlay = null;

        if (componentId <= 3)
        {
            sfxToPlay = sfxComponentAdd[Random.Range(0, sfxComponentAdd.Length)];
            componentReserve[componentId]--;
        }
        else if (componentId == 4)
        {
            sfxToPlay = sfxDillutantAdd;
            GameManager.scriptAudio.PlaySfxSimple(sfxDillutantAdd);
        }


        GameManager.scriptAudio.PlaySfx(sfxToPlay, 1, new Vector2(.8f, 1.2f), GameManager.scriptAudio.sfxSource);

        componentAdded[componentId]++;

        if (componentId <= 4 && componentAdded[componentId] >= componentTarget[componentId])
            componentButton[componentId].interactable = false;

        UpdateValues();
    }

    public void UpdateValues()
    {
        float antigenNeeded = Mathf.Clamp(currentAntigenTarget - antigenAdded, 0, currentAntigenTarget);

        if (antigenNeeded == 0)
            antigenNeededQty.text = "OK";
        else
            antigenNeededQty.text = ((int)antigenNeeded).ToString();

        antigenReserveQty.text = ((int)antigenReserve).ToString();

        //Can't add antigen if reserve is out or if enough antigen is added
        if (antigenAdded >= currentAntigenTarget || antigenReserve <= 0)
            antigenButton.interactable = false;
        else
            antigenButton.interactable = true;

        //Batch can be ended only if enough antigen is added
        if (LabManager.vaccineInRocket >= LabManager.vaccineTarget[GameManager.currentGameStage])
            endBatchButton.interactable = false;
        else if (antigenAdded >= currentAntigenTarget)
            endBatchButton.interactable = true;
        else
            endBatchButton.interactable = false;

        //Update component reserve values
        for (int i = 0; i < componentReserveQty.Length; i++)
        {
            componentReserveQty[i].text = componentReserve[i].ToString();
        }

        //Update componente buttons. Dillutant is sepparated due to it not having a reserve.
        for (int i = 0; i < componentButton.Length; i++)
        {
            if (i <= 3)
            {
                if (componentAdded[i] >= componentTarget[i] || componentReserve[i] <= 0)
                    componentButton[i].interactable = false;

                else if (componentReserve[i] > 0)
                    componentButton[i].interactable = true;
                else
                    componentButton[i].interactable = false;
            }
            else
            {
                if (componentAdded[i] >= componentTarget[i])
                    componentButton[i].interactable = false;
                else
                    componentButton[i].interactable = true;

            }

        }
        //Counts components so that residues can be calculated
        int totalComponentSum = 0;

        for (int i = 0; i < componentNeededQty.Length; i++)
        {
            if (componentTarget[i] - componentAdded[i] <= 0)
                componentNeededQty[i].text = "Ok";
            else
                componentNeededQty[i].text = (componentTarget[i] - componentAdded[i]).ToString();

            totalComponentSum += componentAdded[i];

        }


        componentAdded[5] = totalComponentSum / 5;

        antigenBarFill.fillAmount = antigenAdded / currentAntigenTarget;

       // CalculateBarSizes();

    }
}
