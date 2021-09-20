using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    /*
     * 0 - Health Recovery
     * 1 - Energy Recovery
     * 2 - Damage Buff
     * 3 - Defense Buff
     * 4 - Speed Buff
     */
    [Header("Item Effect")]
    public float hpRecover = .3f;
    public float epRecover = .5f;
    public BuffBase buffDamage;
    public BuffBase buffDefense;
    public BuffBase buffSpeed;

    public GameObject[] itemIcon = new GameObject[5];
    public Text[] itemQtyText = new Text[5];
   // public Image[] buffBarFill = new Image[3];


    private void Awake()
    {
        GameManager.scriptInventory = this;
        
    }

    private void Start()
    {
        UpdateItems();

    }

    public void UpdateItems()
    {
        for (int i = 0; i < GameManager.itemConsumable.Length; i++)
        {
            itemQtyText[i].text = GameManager.itemConsumable[i].ToString();

            if (GameManager.itemConsumable[i] != 0)
                itemIcon[i].SetActive(true);
            else
                itemIcon[i].SetActive(false);
        }
        
    }

    public void ConsumableUse(int id)
    {
        if (GameManager.itemConsumable[id] > 0)
        {
            GameManager.itemConsumable[id]--;

            switch (id)
            {
                case 0:
                    ItemHealthRecover();
                    break;
                case 1:
                    ItemEnergyRecover();
                    break;
                case 2:
                    GameManager.scriptPlayer.GetComponentInChildren<BuffManager>().BuffApply(buffDamage);
                    break;
                case 3:
                    GameManager.scriptPlayer.GetComponentInChildren<BuffManager>().BuffApply(buffDefense);
                    break;
                case 4:
                    GameManager.scriptPlayer.GetComponentInChildren<BuffManager>().BuffApply(buffSpeed);
                    break;
            }
        }

        UpdateItems();
    }


    public void ItemHealthRecover()
    {
        GameManager.scriptPlayer.HealthChange((int)(GameManager.scriptPlayer.hpMax * hpRecover));

    }

    public void ItemEnergyRecover()
    {
        GameManager.scriptPlayer.EnergyChange((int)(GameManager.scriptPlayer.energyMax * epRecover));

    }
    /*
    public void BuffDurationBar(int buffId, bool on)
    {
        buffBarFill[buffId].transform.parent.gameObject.SetActive(on);

    }
    private void BuffDurationUpdate()
    {
        for(int i = 0; i < buffBarFill.Length;i++)
        {
            if (scriptBuff.buffActive[i])
                buffBarFill[i].fillAmount = buffDuration[i] / buffDurationMax[i];
        }

    }

    private void Update()
    {
        BuffDurationUpdate();
    }
    */
}
