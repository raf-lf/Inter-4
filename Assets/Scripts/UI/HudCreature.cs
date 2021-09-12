using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class HudCreature : MonoBehaviour
{
    public GameObject hpBar;
    public Image hpBarFill;
    public float uiDuration;

    public void UpdateValues()
    {
        CreatureAtributes creature = GetComponentInParent<CreatureAtributes>();
        hpBarFill.fillAmount = (float)creature.hp / (float)creature.hpMax;
        ShowBar();
    }

    private void ShowBar()
    {
        StopAllCoroutines();
        hpBar.SetActive(true);
        StartCoroutine(HideBar());
    }

    IEnumerator HideBar()
    {
        yield return new WaitForSeconds(uiDuration);
        hpBar.SetActive(false);

    }
}
