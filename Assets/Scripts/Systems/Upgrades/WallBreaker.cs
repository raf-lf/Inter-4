using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBreaker : MonoBehaviour
{
    [Header("Wall Breaker")]
    public int energyCost;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.scriptPlayer.energy >= energyCost && collision.gameObject.GetComponentInChildren<BreakableWall>())
        {
            GameManager.scriptPlayer.EnergyChange(-energyCost);
            collision.gameObject.GetComponentInChildren<BreakableWall>().Break();
        }

    }
}
