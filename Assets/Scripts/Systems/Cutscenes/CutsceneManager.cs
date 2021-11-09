using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public GameObject[] scenes = new GameObject[0];

    private void Awake()
    {
        
    }

    private void Start()
    {
        PlayScene(scenes[0]);
    }

    public void PlayScene(GameObject sceneObject)
    {
        foreach (var item in sceneObject.GetComponentsInChildren<CutsceneElement>())
        {
            item.Animate();
        }
    }
}
