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
    public int buffVfxId;
    public Image durationFill;
    private EffectManager scriptVfx;


    private void Awake()
    {
        durationMax = duration;

        scriptVfx = GetComponentInParent<CreatureAtributes>().gameObject.GetComponentInChildren<EffectManager>();
    }

    private void Start()
    {
        ApplyBuff();
    }

    public virtual void ApplyBuff()
    {
        active = true;
        duration = durationMax;
        scriptVfx.VfxBuff(buffVfxId, true);
        gameObject.SetActive(true);

    }

    public virtual void RemoveBuff()
    {
        active = false;
        scriptVfx.VfxBuff(buffVfxId, false);
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
                    durationFill.fillAmount = duration / durationMax;

                if (duration == 0)
                    RemoveBuff();
            }
        }
    }

    private void Update()
    {
        BuffDecay();
        
    }

}
