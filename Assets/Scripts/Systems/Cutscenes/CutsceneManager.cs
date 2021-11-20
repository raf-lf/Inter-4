using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class CutsceneManager : MonoBehaviour
{
    public GameObject[] scenes = new GameObject[0];
    public List<GameObject> currentFrames = new List<GameObject>();
    public int currentFrameIndex;
    public Animator animOverlay;
    private GameObject currentCutscene;

    private void Start()
    {
        CutsceneSelect();
    }

    public void CutsceneSelect()
    {
        if (GameManager.endingPlayed)
        {
            PlayCutscene(scenes[6]);
        }
        else
        {
            switch (GameManager.currentGameStage)
            {
                case 0:
                    PlayCutscene(scenes[0]);
                    break;
                case 1:
                    PlayCutscene(scenes[1]);
                    break;
                case 2:
                    PlayCutscene(scenes[2]);
                    break;
                case 3:
                    PlayCutscene(scenes[3]);
                    break;
                case 4:
                    PlayCutscene(scenes[4]);
                    break;
                case 5:
                    PlayCutscene(scenes[5]);
                    break;

            }
        }

    }
    public void PlayCutscene(GameObject sceneObject)
    {
        currentCutscene = sceneObject;

        if (currentFrames != null)
        {
            foreach (var item in currentFrames)
            {
                item.SetActive(false);
            }
        }
        Cutscene cutscene = sceneObject.GetComponent<Cutscene>();
        GameManager.scriptAudio.PlayBgm(cutscene.cutsceneBgm, 1);

        currentFrameIndex = -1;
        currentFrames.Clear();
        currentFrames.AddRange(cutscene.frames);
        NextFrame();
    }

    public void CheckNextFrame()
    {
        if (currentFrameIndex + 1 < currentFrames.Count)
            NextFrame();
        else
            EndCutscene();

    }
    public void NextFrame()
    {
        currentFrameIndex++;

        for (int i = 0; i < currentFrames.Count; i++)
        {
            if (i == currentFrameIndex)
                currentFrames[i].SetActive(true);
            else
                currentFrames[i].SetActive(false);

        }

        if (currentCutscene == scenes[6] && currentFrameIndex == 1)
            GameManager.scriptAudio.bgmSource.mute = true;
        else
            GameManager.scriptAudio.bgmSource.mute = false;


    }
    public void EndCutscene()
    {
        if (GameManager.currentGameStage == 5 && !GameManager.endingPlayed)
        {
            GameManager.endingPlayed = true;
            PlayCutscene(scenes[6]);
        }
        else
        {
            GameManager.scriptAudio.FadeBgm(0, .05f);
            animOverlay.SetBool("blackOut", true);
            Invoke(nameof(ReturnToLab), 2);
        }
    }

    public void ReturnToLab()
    {
        SceneManager.LoadScene("lab", LoadSceneMode.Single);
    }

}
