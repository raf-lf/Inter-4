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
    [HideInInspector]
    public float cooldownTimer;
    public TextMeshProUGUI cooldownTimerText;
    public Image cooldownTimerFill;
    private Button button;

    public Color energyOk;
    public Color energyNo;

    private void Awake()
    {
        button = GetComponentInChildren<Button>();
    }
    protected virtual void Start()
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

    public virtual void SkillUse()
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

    public void CooldownChange(float value)
    {
        cooldownTimer = Mathf.Clamp(cooldownTimer + value, 0, cooldown);
        CooldownUpdate();

    }
    private void CooldownDecay()
    {
        cooldownTimer = Mathf.Clamp(cooldownTimer - Time.deltaTime, 0, cooldown);
        CooldownUpdate();
    }

    private void CooldownUpdate()
    {
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

        if (GameManager.scriptPlayer.energy < energyCost)
        {
            energyCostText.rectTransform.parent.GetComponent<Image>().color = energyNo;
            button.interactable = false;
        }
        else
        {
            energyCostText.rectTransform.parent.GetComponent<Image>().color = energyOk;

            if (cooldownTimer == 0)
                button.interactable = true;
        }

    }
}
