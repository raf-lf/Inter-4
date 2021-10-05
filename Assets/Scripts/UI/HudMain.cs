using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudMain : MonoBehaviour
{
    public TextMeshProUGUI antigenCounter;

    private void Awake()
        => GameManager.scriptHud = this;

    public void AntigenChange(int value)
    {
        GameManager.antigen += value;
        UpdateHud();

    }

    public void UpdateHud()
    {
        antigenCounter.text = GameManager.antigen.ToString();
        //antigenCounter.text = GameManager.scienceCollected.ToString();
    }
}
