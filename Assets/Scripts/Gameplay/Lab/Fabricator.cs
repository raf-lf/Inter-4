using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fabricator : MonoBehaviour
{
    public Button[] componentButton = new Button[4];
    public TextMeshProUGUI[] componentQty = new TextMeshProUGUI[4];

    public int[] mixTarget = new int[4];
    public int[] mixCurrent = new int[4];

    private void Start()
    {
        UpdateValues();
    }

    public void UseComponent(int componentId)
    {
        GameManager.componentStored[componentId]--;
        UpdateValues();
    }

    private void UpdateValues()
    {
        for (int i = 0; i < componentQty.Length; i++)
        {
            componentQty[i].text = GameManager.componentStored[i].ToString();

            if (GameManager.componentStored[i] > 0) 
                componentButton[i].interactable = true;
            else 
                componentButton[i].interactable = false;
        }

    }
}
