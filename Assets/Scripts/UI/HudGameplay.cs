using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudGameplay : MonoBehaviour
{
    public TextMeshProUGUI antigenCounter;
    public GameObject[] componentSlot = new GameObject[4];
    public GameObject dataSlot;

    private void Awake()
        => GameManager.scriptHud = this;

    private void Start()
        => UpdateHud();

    public void AntigenChange(int value)
    {
        ArenaManager.antigenCollected += value;
        UpdateHud();

    }


    public void UpdateHud()
    {
        antigenCounter.text = ArenaManager.antigenCollected + " / " + ArenaManager.antigenCapacity;

        dataSlot.SetActive(ArenaManager.dataCollected);

        for (int i = 0; i < componentSlot.Length; i++)
        {
            componentSlot[i].GetComponentInChildren<TextMeshProUGUI>().text = ArenaManager.componentsInInventory[i].ToString();

        }
    }
}
