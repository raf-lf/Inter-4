using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TechtreeManager : MonoBehaviour
{
    public int availableScience;
    public int spendRate;
    public TextMeshProUGUI textScience;
    public TextMeshProUGUI textDesName;
    public TextMeshProUGUI textDesDescription;
    public TextMeshProUGUI textDesCost;
    public GameObject descriptionPanel;

    private void Start()
    {
        UpdateScienceValues();
        UpdateDescriptionPanel();
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
    }

    public void UpdateDescriptionPanel()
    {
        if (descriptionPanel.GetComponentInChildren<TextMeshProUGUI>().text == null)
            descriptionPanel.SetActive(false);
        else
            descriptionPanel.SetActive(true);
    }

    private void Update()
    {
        textScience.text = availableScience.ToString();
        
    }
}
