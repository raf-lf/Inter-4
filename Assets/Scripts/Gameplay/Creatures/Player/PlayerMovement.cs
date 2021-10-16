using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CreatureMovement
{
    public bool haltMovement;
    private Vector3 mouseTarget;


    private void MouseMove()
    {
      
            mouseTarget = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

            MoveTowards(mouseTarget);

    }

    public void JoystickMove(Vector2 joystickDirection)
    {
        if (!haltMovement && GameManager.PlayerControl)
        {
            direction = joystickDirection;
            Move();
        }
        else
            StopMove();
    }

    private void Update()
    {/*
        if (GameManager.PlayerControl)
        {
            if (!haltMovement)
            {

                if (Input.GetMouseButton(0)) MouseMove();
                else StopMove();
            }
            else StopMove();
        }
        */
        
    }
}
