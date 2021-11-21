using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultScreen : MonoBehaviour
{
    [Header("Text")]
    public TextMeshProUGUI textAntigen;
    public TextMeshProUGUI textData;
    public TextMeshProUGUI[] textComponent = new TextMeshProUGUI[0];
    public TextMeshProUGUI[] textEventScore = new TextMeshProUGUI[0];
    public TextMeshProUGUI[] textEventScience = new TextMeshProUGUI[0];
    public TextMeshProUGUI textDeathPenalty;
    public TextMeshProUGUI textFinalScience;

    [Header("Objects")]
    public GameObject contentCollectedNull;
    public GameObject contentAntigen;
    public GameObject contentData;
    public GameObject[] contentComponent = new GameObject[0];
    public GameObject contentScienceNull;
    public GameObject[] contentScience = new GameObject[0];
    public GameObject contentPlayerDeath;

    public AudioClip sfxStore;
    public static int scienceEnemiesDefeated;
    public static int scienceComponentsCollected;
    public static int scienceConsumablesCollected;
    public static int unusedConsumables;
    public static float bonusUnusedConsumables;
    public static float pennaltyDeath;
    public static int scienceFinal;



    public void SetupResults()
    {
        GetComponent<LabElement>().OpenElement();

        #region SetupCollected
        int collectedStuff = 0;

        //Collected antigen?
        textAntigen.text = LabManager.expeditionAntigen.ToString();

        if (LabManager.expeditionAntigen > 0)
            contentAntigen.SetActive(true);
        else
            contentAntigen.SetActive(false);

        collectedStuff += LabManager.expeditionAntigen;

        //Collected components?
        for (int i = 0; i < contentComponent.Length; i++)
        {
            textComponent[i].text = LabManager.expeditionComponents[i].ToString();

            if (LabManager.expeditionComponents[i] > 0)
                contentComponent[i].SetActive(true);
            else
                contentComponent[i].SetActive(false);

            collectedStuff += LabManager.expeditionComponents[i];
        }

        //Collected data?
        if (LabManager.expeditionData)
        {
            textData.text = "Encontrados!";
            contentData.SetActive(true);
            collectedStuff++;
        }
        else
        {
            contentData.SetActive(false);
        }

        //If nothing was collected, show null content 
        if(collectedStuff > 0)
            contentCollectedNull.SetActive(false);
        else
            contentCollectedNull.SetActive(true);

        #endregion

        #region SetupScience

        int scienceScored = 0;

        if (ArenaManager.expeditionKills > 0)
        {
            scienceScored++;
            contentScience[0].SetActive(true);
            textEventScore[0].text = "x " + ArenaManager.expeditionKills + " =";
            textEventScience[0].text = scienceEnemiesDefeated + " ciência";
        }
        else
            contentScience[0].SetActive(false);



        if (ArenaManager.expeditionComponents > 0)
        {
            scienceScored++;
            contentScience[1].SetActive(true);
            textEventScore[1].text = "x " + ArenaManager.expeditionComponents + " =";
            textEventScience[1].text = scienceComponentsCollected + " ciência";
        }
        else
            contentScience[1].SetActive(false);



        if (ArenaManager.expeditionConsumables > 0)
        {
            scienceScored++;
            contentScience[2].SetActive(true);
            textEventScore[2].text = "x " + ArenaManager.expeditionConsumables + " =";
            textEventScience[2].text = scienceConsumablesCollected + " ciência";
        }
        else
            contentScience[2].SetActive(false);



        if (unusedConsumables > 0)
        {
            scienceScored++;
            contentScience[3].SetActive(true);
            textEventScore[3].text = "x " + unusedConsumables + " =";
            textEventScience[3].text = "+ " + bonusUnusedConsumables * 100 +"%";
        }
        else
            contentScience[3].SetActive(false);

        if(pennaltyDeath != 0)
        {
            textDeathPenalty.text = "- " + pennaltyDeath * 100 + "%";
            contentPlayerDeath.SetActive(true);
        }
        else
            contentPlayerDeath.SetActive(false);


        if (scienceScored == 0)
            contentScienceNull.SetActive(true);
        else
            contentScienceNull.SetActive(false);

        textFinalScience.text = scienceFinal.ToString();
        #endregion

    }

    public void EndResults()
    {
        GameManager.scriptAudio.PlaySfxSimple(sfxStore);
        GameManager.scriptLab.StoreEverything();
        GetComponent<LabElement>().CloseElement();
        GameManager.scriptLab.clickBlocker.SetActive(false);

        LabMonitor.dirtyFlag = true;
    }
}
