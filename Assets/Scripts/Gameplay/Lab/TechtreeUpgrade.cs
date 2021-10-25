using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class TechtreeUpgrade : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Upgrade")]
    public UpgradeBase upgradeScriptableObject;
    public int scienceCost;
    [HideInInspector] public int spentScience;
    [TextAreaAttribute(10, 15)]
    public string description;
    [HideInInspector] public bool available;
    [HideInInspector] public bool purchased;
    public TechtreeUpgrade[] prerequisites = new TechtreeUpgrade[0];

    [Header("Components")]
    [HideInInspector] public Image spentFill;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textCost;
    public ParticleSystem vfxUnlock;
    public ParticleSystem vfxPurchase;
    private Animator anim;
    private TechtreeManager scriptTechtree;

    public bool buttonHeld;


    public void OnPointerDown(PointerEventData eventData)
    {
        buttonHeld = true;

        UpdateDescription();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonHeld = false;

        if (!purchased || !available)
        {
            CheckSpentScience();
        }
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        scriptTechtree = GetComponentInParent<TechtreeManager>();

        if (GameManager.upgradesPurchased.Contains(upgradeScriptableObject))
            purchased = true;
        else
            purchased = false;

    }

    private void Start()
    {
        spentFill.fillAmount = 0;
        textName.text = gameObject.name;
        textCost.text = scienceCost.ToString();
        UpdateStatus();
        
    }

    public void UpdateDescription()
    {
        if(available)
        {
            scriptTechtree.textDesName.text = name;
            scriptTechtree.textDesCost.text = scienceCost.ToString();
            scriptTechtree.textDesDescription.text = description;

        }
        else
        {
            scriptTechtree.textDesName.text = "???";
            scriptTechtree.textDesCost.text = "???";
            scriptTechtree.textDesDescription.text = "É necessário desbloquear outras pesquisas antes!";

        }

        scriptTechtree.UpdateDescriptionPanel();


    }

    public void UpdateStatus()
    {

        if (prerequisites != null && !purchased)
        {
            int count = 0;
            foreach (var item in prerequisites)
            {
                if (item.purchased == false)
                    count++;
            }

            if (count > 0)
                available = false;
            else
            {
                vfxUnlock.Play();
                available = true;
            }
        }
        else
            available = true;

        anim.SetBool("available", available);
        anim.SetBool("purchased", purchased);
    }

    public void AttemptingPurchase()
    {
        if (available && !purchased)
        {
            if (spentScience == scienceCost)
                Purchase();

            if (buttonHeld && scriptTechtree.availableScience > 0 && spentScience < scienceCost)
            {
                scriptTechtree.availableScience -= scriptTechtree.spendRate;
                spentScience = Mathf.Clamp(spentScience + scriptTechtree.spendRate, 0, scienceCost);
                spentFill.fillAmount = (float)spentScience / (float)scienceCost;

            }

        }
    }

    public void CheckSpentScience()
    {
        if (spentScience == scienceCost)
        {
            Purchase();
        }
        else
        {
            scriptTechtree.availableScience += spentScience;
            spentFill.fillAmount = 0;
            spentScience = 0;

        }
    }

    public void Purchase()
    {
        GameManager.upgradesPurchased.Add(upgradeScriptableObject);
        vfxPurchase.Play();
        purchased = true;
        LabManager.scienceStored -= scienceCost;
        scriptTechtree.UpdateUpgradeStatus();



    }
    private void Update()
    {
        AttemptingPurchase();
    }

}
