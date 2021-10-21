using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Turbidity : Buff_AtributeChange
{
    [Header("Turbidity")]
    public float fovChange;

    public override void ApplyBuff()
    {
        GameManager.scriptCanvas.overlayAnimator.SetBool("turbid", true);
        GameManager.scriptCamera.ChangeCameraOffset(new Vector3(0, 0, fovChange));
        base.ApplyBuff();
    }

    public override void RemoveBuff()
    {
        GameManager.scriptCanvas.overlayAnimator.SetBool("turbid", false);
        GameManager.scriptCamera.ResetCameraOffset();
        base.RemoveBuff();
    }
}
