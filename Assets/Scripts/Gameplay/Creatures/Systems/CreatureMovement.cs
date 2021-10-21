using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float currentMoveSpeed;
    public Vector2 direction;
    public bool lookingLeft;

    [Header("Components")]
    private Rigidbody2D rb;
    public bool moving;
    private Animator anim;
    public GameObject spriteBody;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInParent<CreatureAtributes>().anim;
        UpdateMoveSpeed();
    }

    public void UpdateMoveSpeed()
    {
        currentMoveSpeed = GetComponent<CreatureAtributes>().moveSpeed * GetComponent<CreatureAtributes>().moveSpeedModifier;
    }

    public void MoveTowards(Vector2 moveTarget)
    {
        direction = Calculations.GetDirectionToTarget(transform.position, moveTarget);
        Move();

    }
    public void MoveAway(Vector2 moveTarget)
    {
        direction = (Calculations.GetDirectionToTarget(transform.position, moveTarget) * -1);
        Move();

    }



    public void Move()
    {

        if (!moving)
        {
            moving = true; 
            /*
            if (anim != null)   
                anim.SetBool("moving", moving);
            */
        }

        rb.velocity = (currentMoveSpeed) * direction;

        if (rb.velocity.x > 0)
        {
            spriteBody.transform.rotation = Quaternion.Euler(new Vector3(spriteBody.transform.rotation.x, 0, spriteBody.transform.rotation.z));
            lookingLeft = false;
        }
        else if (rb.velocity.x < 0)
        {
            spriteBody.transform.rotation = Quaternion.Euler(new Vector3(spriteBody.transform.rotation.x, 180, spriteBody.transform.rotation.z));
            lookingLeft = true;
        }

    }

    public void StopMove()
    {
        if(moving)
        {
            moving = false;

            /*
            if (anim != null)
                anim.SetBool("moving", moving);
            */
        }
    }

}
