using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillManager : MonoBehaviour
{
    public List<GameObject> player1Skills = new List<GameObject>();

    private void Start()
    {
        List<GameObject> skillsToAdd = new List<GameObject>();

        switch (GameManager.CurrentPlayerClass)
        {
            case 0:
                skillsToAdd.AddRange(player1Skills);
                break;
        }

        foreach (GameObject item in skillsToAdd)
        {
            Instantiate(item, transform);

        }
    }
}
