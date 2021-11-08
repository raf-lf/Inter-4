using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Element_Hatch : LabElement
{
    public void StartExpedition()
    {
        SceneManager.LoadScene("arena", LoadSceneMode.Single);
    }
}
