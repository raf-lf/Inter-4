using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Briefing : MonoBehaviour
{
    public Dialogue[] stageBriefings = new Dialogue[0];


    public void Debrief()
    {
        if (!GameManager.briefingPlayed)
        {
            GameManager.briefingPlayed = true;
            GameManager.scriptDialogue.SetupDialogue(stageBriefings[GameManager.currentGameStage], DialogueType.briefing);
        }
    }

}
