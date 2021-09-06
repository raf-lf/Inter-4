using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float currentMoveSpeed;
    private Vector2 direction;
    
    [Header("Components")]
    private Rigidbody2D rb;
    public bool moving;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        UpdateMoveSpeed();
    }

    public void UpdateMoveSpeed()
    {
        currentMoveSpeed = GetComponent<CreatureAtributes>().moveSpeed * GetComponent<CreatureAtributes>().moveSpeedModifier;
    }

    public void MoveTowards(Vector2 moveTarget)
    {
        moving = true;

        direction = Calculations.GetDirectionToTarget(transform.position, moveTarget);
        rb.velocity = (currentMoveSpeed) * direction;
    }
    public void StopMove()
    {
        moving = false;
    }
}
