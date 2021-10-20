using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillBase : MonoBehaviour
{
    public int energyCost;
    public TextMeshProUGUI energyCostText;
    public float cooldown;
    private float cooldownTimer;
    public TextMeshProUGUI cooldownTimerText;
    public Image cooldownTimerFill;

    private void Start()
    {
        energyCostText.text = energyCost.ToString();

        if (energyCost == 0)
            energyCostText.transform.parent.gameObject.SetActive(false);

        ResetCooldown();
    }

    public void ResetCooldown()
    {
        cooldownTimer = 0;
        CooldownDecay();
    }

    public void SkillUse()
    {
        if (GameManager.scriptPlayer.SpendEnergy(energyCost))
        {
            Activate();
        }

    }

    public virtual void Activate()
    {
        GetComponentInChildren<Button>().interactable = false;
        cooldownTimer = cooldown;
    }

    private void CooldownDecay()
    {
        cooldownTimer = Mathf.Clamp(cooldownTimer - Time.deltaTime, 0, cooldown);
        cooldownTimerFill.fillAmount = (cooldownTimer / cooldown);
        cooldownTimerText.text = ((int)(1 + cooldown - (cooldown - cooldownTimer))).ToString();
        if (cooldownTimer == 0)
        {
            cooldownTimerText.text = "";
            GetComponentInChildren<Button>().interactable = true;
        }
    }

    private void Update()
    {
        if (cooldownTimer > 0)
            CooldownDecay();
    }
}
