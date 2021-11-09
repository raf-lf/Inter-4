using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillManager : MonoBehaviour
{
    public List<GameObject> player1Skills = new List<GameObject>();

    private void Awake()
    {
        GameManager.scriptSkill = this;
    }

    private void Start()
    {
      //  List<GameObject> skillsToAdd = player1Skills;

        /*
        switch (GameManager.CurrentPlayerClass)
        {
            case 0:
                skillsToAdd.AddRange(player1Skills);
                break;
        }

        foreach (GameObject item in player1Skills)
        {
            GameObject newitem = Instantiate(item, transform);
            Debug.Log(newitem.name + " added");

        }
        */
    }
}
