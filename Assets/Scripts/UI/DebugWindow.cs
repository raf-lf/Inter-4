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

            for (int i = 0; i < LabManager.componentStored.Length; i++)
            {
                LabManager.componentStored[i] += 1000;
            }
        }
        else
        {
            ArenaManager.antigenCollected += 1000;

            for (int i = 0; i < ArenaManager.componentsInInventory.Length; i++)
            {
                ArenaManager.componentsInInventory[i] += 1000;
            }

            for (int i = 0; i < GameManager.itemConsumable.Length; i++)
            {
                GameManager.itemConsumable[i] += 1000;
            }

            GameManager.scriptHud.UpdateHud();
            GameManager.scriptInventory.UpdateItems();
        }
    }

    public void ReloadSpecificStage(int stage)
    {
        GameManager.currentGameStage = stage;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);

    }
}
