using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DebugWindow : MonoBehaviour
{
    [Header("Debug")]
    public TextMeshProUGUI fpsBox;
    public float fpsInterval;

    private void Start()
    {
        if (Debug.isDebugBuild)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }

    private void Update()
    {
        if (fpsBox != null && fpsBox.isActiveAndEnabled)
        {
            if (Time.frameCount % fpsInterval == 0)
                fpsBox.text = ("FPS " + (int)(1f / Time.unscaledDeltaTime)).ToString();
        }
    }

    public void AddResources()
    {
        if (SceneManager.GetActiveScene().name == "lab")
        {
            LabManager.antigenStored += 1000;
            LabManager.scienceStored += 1000;
            LabManager.dataStored += 1;
            for (int i = 0; i < LabManager.componentStored.Length; i++)
            {
                LabManager.componentStored[i] += 100;
            }
        }
        else
        {
            GameManager.scriptArena.AntigenChange(100);

            for (int i = 0; i < ArenaManager.componentsInInventory.Length; i++)
            {
                GameManager.scriptArena.ComponentChange(i, 10);
            }

            for (int i = 0; i < ArenaManager.consumablesInInventory.Length; i++)
            {
                ArenaManager.consumablesInInventory[i] += 10;
            }

            GameManager.scriptHud.UpdateHud();
            GameManager.scriptInventory.UpdateItems();
        }
    }

    public void ReloadSpecificStage(int stage)
    {
        GameManager.currentGameStage = stage;
        SceneManager.LoadScene("arena", LoadSceneMode.Single);

    }
}
