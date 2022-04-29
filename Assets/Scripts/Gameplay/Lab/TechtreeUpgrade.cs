using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class TechtreeUpgrade : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool baseKit;
    [HideInInspector] public bool available;
    [HideInInspector] public bool purchased;

    [Header("Upgrade")]
    public UpgradeBase upgradeScriptableObject;
    [Tooltip("How much science is needed to unlock the skill.")]
    public int scienceCost;
    [HideInInspector] 
    public int spentScience;
    public string upgradeName;
    [TextArea(10, 15)]
    public string description;
    public TechtreeUpgrade[] prerequisites = new TechtreeUpgrade[0];
    [HideInInspector]
    public LineRenderer[] lineConnections;

    [Header("Components")]
    [HideInInspector] public Image spentFill;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textCost;
    public ParticleSystem vfxUnlock;
    public ParticleSystem vfxPurchase;
    private Animator anim;
    private Element_Techtree scriptTechtree;
    public bool buttonHeld;
    public PlaySfx sfxPurchase;
    public PlaySfx sfxPurchaseCancel;
    public AudioSource sfxPurchasingLoop;

    [Header("Connections")]
    public RectTransform connectionsParent;
    public GameObject linePrefab;
    public Color connectionColorLocked;
    public Color connectionColorAvailable;
    public Color connectionColorPurchased;



    public void OnPointerDown(PointerEventData eventData)
    {
        buttonHeld = true;

        UpdateDescription();

        scriptTechtree.spendRate = scienceCost / 120;
        if (scriptTechtree.spendRate < 1)
            scriptTechtree.spendRate = 1;
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
        scriptTechtree = GetComponentInParent<Element_Techtree>();

        if(baseKit)
        {
            purchased = true;

            if(!GameManager.upgradesPurchasedId.Contains(upgradeScriptableObject.upgradeId))
            GameManager.upgradesPurchasedId.Add(upgradeScriptableObject.upgradeId);

            /*
            if (!GameManager.upgradesPurchased.Contains(upgradeScriptableObject))
                GameManager.upgradesPurchased.Add(upgradeScriptableObject);
            */
        }

        //if (GameManager.upgradesPurchased.Contains(upgradeScriptableObject))
        if (GameManager.upgradesPurchasedId.Contains(upgradeScriptableObject.upgradeId))
            purchased = true;
        else
            purchased = false;

    }

    private void SetupConnections()
    {
        if(prerequisites != null)
        { 
            lineConnections = new LineRenderer[prerequisites.Length];

            for (int i = 0; i < lineConnections.Length; i++)
            {
                if(lineConnections[i] == null)
                {
                    GameObject lineObject = Instantiate(linePrefab, connectionsParent);
                    lineConnections[i] = lineObject.GetComponent<LineRenderer>();

                }

                lineConnections[i].SetPosition(0, prerequisites[i].transform.position);
                lineConnections[i].SetPosition(1, transform.position);

                if(purchased)
                {
                    lineConnections[i].startColor = connectionColorPurchased;
                    lineConnections[i].endColor = connectionColorPurchased;

                }
                else if(prerequisites[i].purchased)
                {
                    lineConnections[i].startColor = connectionColorAvailable;
                    lineConnections[i].endColor = connectionColorAvailable;
                }
                else
                {
                    lineConnections[i].startColor = connectionColorLocked;
                    lineConnections[i].endColor = connectionColorLocked;

                }
            }
        }

    }

    private void Start()
    {
        spentFill.fillAmount = 0;
        textName.text = upgradeName;
        textCost.text = scienceCost.ToString();
        //UpdateStatus();
        
    }

    public void UpdateDescription()
    {
        scriptTechtree.descriptionPanel.SetActive(true);

        if (available)
        {
            scriptTechtree.textDesName.text = upgradeName;
            scriptTechtree.textDesCost.text = scienceCost.ToString();
            scriptTechtree.textDesDescription.text = description;

        }
        else
        {
            scriptTechtree.textDesName.text = "???";
            scriptTechtree.textDesCost.text = "???";
            scriptTechtree.textDesDescription.text = "É necessário desbloquear outras pesquisas antes!";

        }



    }

    public void UpdateStatus()
    {
        SetupConnections();

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
                sfxPurchasingLoop.mute = false;
                sfxPurchasingLoop.pitch = 1 + ((float)spentScience / (float)scienceCost) *2;
                scriptTechtree.availableScience = Mathf.Clamp(scriptTechtree.availableScience - scriptTechtree.spendRate, 0, scriptTechtree.availableScience);
                spentScience = Mathf.Clamp(spentScience + scriptTechtree.spendRate, 0, scienceCost);
                spentFill.fillAmount = (float)spentScience / (float)scienceCost;

            }
            else
                sfxPurchasingLoop.mute = true;

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
            sfxPurchaseCancel.PlayInspectorSfx();
        }
    }

    public void Purchase()
    {
        sfxPurchase.PlayInspectorSfx();

        // GameManager.upgradesPurchased.Add(upgradeScriptableObject);
        GameManager.upgradesPurchasedId.Add(upgradeScriptableObject.upgradeId);

        vfxPurchase.Play();
        purchased = true;
        LabManager.scienceStored -= scienceCost;
        scriptTechtree.UpdateUpgradeStatus();



    }
    private void Update()
    {
        if (scriptTechtree.elementActive)
        {
            AttemptingPurchase();
        }
    }

}
