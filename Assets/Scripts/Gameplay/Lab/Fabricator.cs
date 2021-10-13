using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fabricator : MonoBehaviour
{
    public Button[] componentButton = new Button[5];
    public TextMeshProUGUI[] componentQty = new TextMeshProUGUI[4];

    public GameObject targetMixBar;
    private Image[] targetMixComponentBar = new Image[6];
    public GameObject currentMixBar;
    private Image[] currentMixComponentBar = new Image[6];

    public int[] mixTarget = new int[6];
    public int[] mixCurrent = new int[6];

    private void Start()
    {
        targetMixComponentBar = targetMixBar.GetComponentsInChildren<Image>();
        currentMixComponentBar = currentMixBar.GetComponentsInChildren<Image>();
        UpdateValues();
    }

    public void UseComponent(int componentId)
    {
        GameManager.componentStored[componentId]--;
        UpdateValues();
    }

    private void UpdateValues()
    {
        //Updates components qty on buttons
        for (int i = 0; i < componentQty.Length; i++)
        {

            if (i != 4)
            {
                componentQty[i].text = GameManager.componentStored[i].ToString();

                if (GameManager.componentStored[i] > 0)
                    componentButton[i].interactable = true;
                else
                    componentButton[i].interactable = false;
            }
        }

        for (int i = 0; i < targetMixComponentBar.Length;i++)
        {
            targetMixComponentBar[i].GetComponentInChildren<TextMeshProUGUI>().text = mixTarget[i].ToString();
            currentMixComponentBar[i].GetComponentInChildren<TextMeshProUGUI>().text = mixCurrent[i].ToString();
        }

    }
}
