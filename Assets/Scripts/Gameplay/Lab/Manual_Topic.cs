using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manual_Topic : MonoBehaviour
{
    private Element_Manual scriptManual;
    public GameObject contentPrefab;
    public AudioClip sfxOpen;

    private void Awake()
    {
        scriptManual = GetComponentInParent<Element_Manual>();
    }

    public void SelectContent()
    {
        GameManager.scriptAudio.PlaySfxSimple(sfxOpen);
        scriptManual.SetContent(contentPrefab);
    }
}
