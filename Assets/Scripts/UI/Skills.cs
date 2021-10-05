using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public void Player1_SwapWeapon()
    {
        GameManager.scriptPlayer.GetComponent<PlayerActions_Player1>().SwitchWeapons();
    }
}
