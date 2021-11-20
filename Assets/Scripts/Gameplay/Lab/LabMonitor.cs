using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LabMonitor : MonoBehaviour
{
    public AudioClip sfxGoalCompleted;

    [Header("Phase Text")]
    public TextMeshProUGUI topicCurrentPhase;
    public string[] phaseNames = new string[6];
    public Color[] phaseColors = new Color[6];

    [Header("Topics")]
    public TextMeshProUGUI topicMaterials;
    public TextMeshProUGUI topicVaccine;
    public TextMeshProUGUI topicData;
    public TextMeshProUGUI topicRocket;

    [Header("Flags")]
    public static bool okData;
    public static bool okVaccine;
    public static bool dirtyFlag;


    private void Start()
    {
        dirtyFlag = true;
        GameManager.scriptLab.monitor = this;
        SetPhase();
        LaunchRocket(false);
    }

    private void Update()
    {
        if(dirtyFlag)
        {
            dirtyFlag = false;
            UpdateData();
            UpdateVaccine();
        }

    }

    public void ResetFlags()
    {
        okData = false;
        okVaccine = false;
    }

    public void SetPhase()
    {
        topicCurrentPhase.text = phaseNames[GameManager.currentGameStage];
        topicCurrentPhase.color = phaseColors[GameManager.currentGameStage];

    }
    public void UpdateData()
    {
        if (LabManager.dataNeededPerStage[GameManager.currentGameStage] <= 0)
            topicData.gameObject.SetActive(false);
        else if (LabManager.dataStored >= LabManager.dataNeededPerStage[GameManager.currentGameStage])
        {
            topicData.color = Color.gray;

            if (!okData)
            {
                okData = true;
                GameManager.scriptAudio.PlaySfxSimple(sfxGoalCompleted);
            }
        }
        else
            topicData.color = Color.white;


    }
    public void UpdateVaccine()
    {
        if (LabManager.vaccineInRocket >= LabManager.vaccineTarget[GameManager.currentGameStage])
        {
            topicMaterials.color = Color.gray;
            topicVaccine.color = Color.gray;

                if (!okVaccine)
                {
                    okVaccine = true;
                    GameManager.scriptAudio.PlaySfxSimple(sfxGoalCompleted);
                }
            }
        else
        {
            topicMaterials.color = Color.white;
            topicVaccine.color = Color.white;
        }

    }

    public void LaunchRocket(bool launch)
    {
        if(launch)
        {
            topicRocket.color = Color.gray;
            GameManager.scriptAudio.PlaySfxSimple(sfxGoalCompleted);
        }
        else
            topicRocket.color = Color.white;
    }

}
