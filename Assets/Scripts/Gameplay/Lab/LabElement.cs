using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabElement : MonoBehaviour
{
    public void OpenCloseElement(bool open)
    {
        GetComponent<Animator>().SetBool("active", open);
    }
    public void OpenOtherElement(Animator elementAnim)
    {
        elementAnim.SetBool("active", true);
    }
    public void CloseOtherElement(Animator elementAnim)
    {
        elementAnim.SetBool("active", false);
    }
}
