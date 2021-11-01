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
    public TextMeshProUGUI[] componentReserveQty = new TextMeshProUGUI[4];
    public TextMeshProUGUI antigenNeededQty;
    public TextMeshProUGUI[] componentAddedQty = new TextMeshProUGUI[6];

    [Header("Bars")]
    public Image antigenBarFill;
    public RectTransform targetMixTotal;
    public RectTransform addedMixTotal;
    public Image[] targetMixComponentBar = new Image[6];
    public Image[] currentMixComponentBar = new Image[6];

    [Header("Resources")]
    public int[] antigenTarget = {100, 500, 1500, 3000, 3000};
    public int currentAntigenTarget;
    public int antigenAdded;
    public int antigenReserve;
    public int[] componentTarget = new int[6];
    public int[] componentAdded = new int[6];
    public int[] componentReserve = new int[6];

    [Header("Vaccine Efficiency")]
    public float mistakePennalty;

    public override void StartupElement()
    {
        base.StartupElement();

        UpdateValues();
        BeginBatch();
    }

    
    public void BeginBatch()
    {
        currentAntigenTarget = antigenTarget[GameManager.currentGameStage];

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

    }

    public void EndBatch()
    {
        float mistakeTotal = 0;

        for (int i = 0; i < componentAdded.Length; i++)
        {
            mistakeTotal += Mathf.Abs(componentAdded[i] - componentTarget[i]);

        }

        mistakeTotal *= mistakePennalty;

    }

    public void AddAntigen(int value)
    {
        antigenReserve -= value;
        antigenAdded += value;
        UpdateValues();
    }

    public void UseComponent(int componentId)
    {
        if (componentId <=3) 
            componentReserve[componentId]--;
        
        componentAdded[componentId]++;

        UpdateValues();
    }

    public void CalculateBarSizes()
    {
        float barSize = 600;
        float targetMixSum = 0;

        for (int i = 0; i < componentTarget.Length; i++)
        {
            targetMixSum += componentTarget[i];
        }

        float sizeFactor = barSize / targetMixSum;

        for (int i = 0; i < componentTarget.Length; i++)
        {
            targetMixComponentBar[i].rectTransform.sizeDelta = new Vector2(targetMixComponentBar[i].rectTransform.sizeDelta.x * sizeFactor, targetMixComponentBar[i].rectTransform.sizeDelta.y);
        }


    }
    public void UpdateValues()
    {
        int antigenNeeded = Mathf.Clamp(currentAntigenTarget - antigenAdded, 0, currentAntigenTarget);

        if (antigenNeeded == 0)
            antigenNeededQty.text = "OK";
        else
            antigenNeededQty.text = antigenNeeded.ToString();

        antigenReserveQty.text = antigenReserve.ToString();

        if (antigenReserve == 0 || antigenAdded == currentAntigenTarget)
            antigenButton.interactable = false;
        else if (antigenReserve > 0)
            antigenButton.interactable = true;
        else
            antigenButton.interactable = false;

        for (int i = 0; i < componentReserveQty.Length; i++)
        {
            componentReserveQty[i].text = componentReserve[i].ToString();

            if (componentReserve[i] > 0)
                componentButton[i].interactable = true;
            else
                componentButton[i].interactable = false;
        }
        int totalComponentSum = 0;

        for (int i = 0; i < componentAdded.Length; i++)
        {
            componentAddedQty[i].text = componentAdded[i].ToString();
            targetMixComponentBar[i].GetComponentInChildren<TextMeshProUGUI>().text = componentTarget[i].ToString();
            totalComponentSum += componentAdded[i];

        }
        componentAdded[5] = totalComponentSum / 5;

        antigenBarFill.fillAmount = (float)(antigenAdded / (float)currentAntigenTarget);

       // CalculateBarSizes();

    }
}
