using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class BuffBase : MonoBehaviour
{
    [Header("Duration")]
    public bool hideDuration;
    public bool infiniteDuration;
    public float duration;
    private float durationMax;
    protected bool active;

    [Header("Components")]
    public int buffId;
   // public int buffVfxId;
    public GameObject buffVfx;
    public Image durationFill;
    private EffectManager scriptVfx;
    protected BuffManager manager;


    private void Awake()
    {
        durationMax = duration;

        scriptVfx = GetComponentInParent<CreatureAtributes>().gameObject.GetComponentInChildren<EffectManager>();
        manager = GetComponentInParent<BuffManager>();
    }

    private void Start()
    {
        ApplyBuff();
    }

    public virtual void ApplyBuff()
    {
        active = true;
        duration = durationMax;
        scriptVfx.AddBuff(buffId, buffVfx);
        gameObject.SetActive(true);

    }

    public virtual void RemoveBuff()
    {
        active = false;
        scriptVfx.RemoveBuff(buffId);
        gameObject.SetActive(false);

    }

    private void BuffDecay()
    {
        if (!infiniteDuration)
        {
            if (duration > 0)
            {
                duration = Mathf.Clamp(duration -= Time.deltaTime, 0, durationMax);

                if (!hideDuration)
                    durationFill.fillAmount = (1 - duration / durationMax);

                if (duration == 0)
                    RemoveBuff();
            }
        }
    }

    protected virtual void Update()
    {
        BuffDecay();
        
    }

}
