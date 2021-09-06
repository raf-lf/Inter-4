using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CreatureMovement
{
    public bool haltMovement;
    private Vector3 mouseTarget;


    private void PlayerMove()
    {
        mouseTarget = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

        MoveTowards(mouseTarget);
    }

    private void Update()
    {
        if (GameManager.PlayerControl)
        {
            if (!haltMovement)
            {
                if (Input.GetMouseButton(0)) PlayerMove();
                else StopMove();
            }
            else StopMove();
        }
        
    }
}
