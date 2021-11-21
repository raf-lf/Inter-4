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


    public void IncreaseFeedbackAntigen()
        => antigenCounter.GetComponentInParent<Animator>().SetTrigger("increase");
    public void IncreaseFeedbackComponent(int id)
        => componentSlot[id].GetComponent<Animator>().SetTrigger("increase");
    public void IncreaseFeedbackData()
        => dataSlot.GetComponent<Animator>().SetTrigger("increase");


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
