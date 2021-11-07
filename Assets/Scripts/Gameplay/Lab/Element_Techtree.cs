using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Element_Techtree : LabElement
{
    public int availableScience;
    public int spendRate;
    public TextMeshProUGUI textScience;
    public TextMeshProUGUI textDesName;
    public TextMeshProUGUI textDesDescription;
    public TextMeshProUGUI textDesCost;
    public GameObject descriptionPanel;
    public List<LineRenderer> upgradeConnections = new List<LineRenderer>();

    public override void OpenElement()
    {
        base.OpenElement();

        StopCoroutine(ConnectionsFade(true));
        StartCoroutine(ConnectionsFade(true));
    }
    public override void CloseElement()
    {
        base.CloseElement();

        StopCoroutine(ConnectionsFade(false));
        StartCoroutine(ConnectionsFade(false));
    }

    public override void StartupElement()
    {
        base.StartupElement();
        UpdateScienceValues();
        descriptionPanel.SetActive(false);
        UpdateUpgradeStatus();

    }
    public void UpdateScienceValues()
    {
        availableScience = LabManager.scienceStored;

    }

    public void UpdateUpgradeStatus()
    {
        TechtreeUpgrade[] allUpgrades = GetComponentsInChildren <TechtreeUpgrade>();

        foreach (var item in allUpgrades)
        {
            item.UpdateStatus();

        }

        upgradeConnections.Clear();
        upgradeConnections.AddRange(GetComponentsInChildren<LineRenderer>()); 
        
    }


    private void Update()
    {
        if (elementActive)
        {
            textScience.text = availableScience.ToString();
        }
        
    }

    public IEnumerator ConnectionsFade(bool appear)
    {
        float alphaValue = 0;
        float targetValue = 0;

        if (appear)
        {
            alphaValue = 0;
            targetValue = 1;
        }
        else
        {
            alphaValue = 1;
            targetValue = 0;
        }

        while (alphaValue != targetValue)
        {
            alphaValue = Mathf.Clamp(alphaValue + .3f, 0, targetValue);

            foreach (var item in upgradeConnections)
            {
                Color lineColorA = new Color (item.startColor.r, item.startColor.g, item.startColor.b, alphaValue);
                Color lineColorB = new Color (item.endColor.r, item.endColor.g, item.endColor.b, alphaValue);
                item.startColor = lineColorA;
                item.endColor = lineColorB;
            }

            yield return new WaitForEndOfFrame();

        }

    }
}
