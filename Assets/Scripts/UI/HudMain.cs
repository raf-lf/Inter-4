using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudMain : MonoBehaviour
{
    public TextMeshProUGUI antigenCounter;
    public GameObject[] componentSlot = new GameObject[4];

    private void Awake()
        => GameManager.scriptHud = this;

    private void Start()
        => UpdateHud();

    public void AntigenChange(int value)
    {
        GameManager.antigen += value;
        UpdateHud();

    }

    public void UpdateHud()
    {
        antigenCounter.text = GameManager.antigen.ToString();

        for (int i = 0; i < componentSlot.Length; i++)
        {
            componentSlot[i].GetComponentInChildren<TextMeshProUGUI>().text = GameManager.component[i].ToString();

        }
    }
}
