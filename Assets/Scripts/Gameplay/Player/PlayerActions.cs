using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerAtributes.PlayerDeath += PlayerDead;
    }

    private void OnDisable()
    {
        PlayerAtributes.PlayerDeath -= PlayerDead;

    }

    protected virtual void PlayerDead()
    {
        this.enabled = false;

    }

    protected virtual void Attack()
    {
    }

    protected virtual void Update()
    {

    }
}
