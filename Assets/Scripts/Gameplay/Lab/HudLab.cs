using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HudLab : MonoBehaviour
{
    public TextMeshProUGUI dataStoredQty;
    public TextMeshProUGUI scienceStoredQty;
    public TextMeshProUGUI antigenStoredQty;
    public TextMeshProUGUI[] componentStoredQty = new TextMeshProUGUI[4];

    public void UpdateValues()
    {
        dataStoredQty.text = LabManager.dataStored.ToString();
        scienceStoredQty.text = LabManager.scienceStored.ToString();
        antigenStoredQty.text = LabManager.antigenStored + "/" + LabManager.antigenStoredMax;
        for (int i = 0; i < componentStoredQty.Length; i++)
        {
            componentStoredQty[i].text = LabManager.componentStored[i].ToString();

        }
    }

    private void Update()
    {
        UpdateValues();
    }
}
