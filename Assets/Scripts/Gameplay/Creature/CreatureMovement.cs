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
    private Animator anim;
    public GameObject spriteBody;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInParent<CreatureAtributes>().animator;
        UpdateMoveSpeed();
    }

    public void UpdateMoveSpeed()
    {
        currentMoveSpeed = GetComponent<CreatureAtributes>().moveSpeed * GetComponent<CreatureAtributes>().moveSpeedModifier;
    }

    public void MoveTowards(Vector2 moveTarget)
    {
        moving = true;
        if (anim != null) anim.SetBool("moving", moving);

        direction = Calculations.GetDirectionToTarget(transform.position, moveTarget);
        rb.velocity = (currentMoveSpeed) * direction;

        if (rb.velocity.x > 0) spriteBody.transform.rotation = Quaternion.Euler(new Vector3(spriteBody.transform.rotation.x, 0, spriteBody.transform.rotation.z));
        else if (rb.velocity.x < 0) spriteBody.transform.rotation = Quaternion.Euler(new Vector3(spriteBody.transform.rotation.x, 180, spriteBody.transform.rotation.z));
    }
    public void StopMove()
    {
        moving = false;
        if (anim != null) anim.SetBool("moving", moving);
    }
}
