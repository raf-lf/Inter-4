using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CanvasGameplay : MonoBehaviour
{
    public Animator overlayAnimator;
    [Header("Debug")]
    public TextMeshProUGUI fpsBox;
    public float fpsInterval;

    private void OnEnable()
    {
        GameManager.scriptCanvas = this;
    }

    private void Update()
    {
        if (fpsBox != null && fpsBox.isActiveAndEnabled)
        {
            if(Time.frameCount % fpsInterval == 0)
                fpsBox.text = ("FPS " + (int)(1f / Time.unscaledDeltaTime)).ToString();
        }
    }

    /*
    public void ReloadStage()
    {
        int result;

        if (int.TryParse(stageBox.text, out result))
        {
            GameManager.currentGameStage = result;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
        else
            Debug.LogError(stageBox.text + " não é um int, seu idiota.");


    }
    */

    public void ReloadSpecificStage (int stage)
    {
        GameManager.currentGameStage = stage;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);

    }


}
