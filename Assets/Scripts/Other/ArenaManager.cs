using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaManager : MonoBehaviour
{

    public float timeBeforeReload = 3;

    private void Start()
    {
        GameManager.scriptArena = this;
    }

    private void OnEnable()
    {
        PlayerAtributes.PlayerDeath += GameOver;
    }

    private void OnDisable()
    {
        PlayerAtributes.PlayerDeath -= GameOver;

    }

    public void GameOver()
    {
        Invoke(nameof(LoadScene), timeBeforeReload);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("lab", LoadSceneMode.Single);
    }
}
