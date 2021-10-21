using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

//Profiling.BeginSample("NOMEDOTRECHO")
//Profiling.EndSample();

public class CreatureBehavior : MonoBehaviour
{
    [Header("Atributes")]
    public bool behaviorOn = true;
    public float busyTime;

    [Header("Target Detection")]
    public float detectionRange;
    public List<Faction> factionsDetected = new List<Faction>();
    private Collider2D[] colliders = new Collider2D[0];
    public CreatureAtributes currentTarget;
    [SerializeField]
    private List<CreatureAtributes> possibleTargets = new List<CreatureAtributes>();
    private int targetCheckFrames;
    private int targetCheckInterval = 30;

    [Header("Action")]
    public float actionCooldown = 1;

    [Header("Components")]
    protected CreatureAtributes atributes;
    protected CreatureMovement movement;
    protected Animator anim;

    protected virtual void Start()
    {
        GetComponent<CircleCollider2D>().radius = detectionRange;
        atributes = GetComponentInParent<CreatureAtributes>();
        movement = GetComponentInParent<CreatureMovement>();
        anim = GetComponentInParent<Animator>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Is colliding object a creature?
        if (collision.gameObject.GetComponent<CreatureAtributes>())
        {
            CreatureAtributes creatureInRange = collision.gameObject.GetComponent<CreatureAtributes>();
            
            //Is not on the list AND is creature of detectable faction?
            if (possibleTargets.Contains(creatureInRange) == false && factionsDetected.Contains(creatureInRange.creatureFaction) && !creatureInRange.dead)
                possibleTargets.Add(creatureInRange);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CreatureAtributes>())
        {
            CreatureAtributes creatureOutOfRange = collision.gameObject.GetComponent<CreatureAtributes>();
            if (possibleTargets.Contains(creatureOutOfRange))
            {
                possibleTargets.Remove(creatureOutOfRange);

            }
        }
        
    }

    private void SelectTarget()
    {
        if (possibleTargets.Contains(currentTarget) == false) currentTarget = null;

        if (possibleTargets.Count > 0)
        { 
            if (targetCheckFrames > 0)
            targetCheckFrames--;

            else
            {
                targetCheckFrames = targetCheckInterval;
                
                CreatureAtributes finalTarget = null;
                float minDistance = Mathf.Infinity;
                
                foreach (CreatureAtributes creature in possibleTargets)
                {
                    float currentDistance = Vector2.Distance(creature.transform.position, transform.position);
                    
                    if (currentDistance < minDistance)
                    {
                        minDistance = currentDistance;
                        finalTarget = creature;

                    }

                }

                if (!finalTarget.dead)
                    currentTarget = finalTarget;
                //Debug.Log("Target changed to " + currentTarget);

            }
        }


    }

    public void RestartCollider()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Collider2D>().enabled = true;

    }

    public virtual void Attack()
    {
        busyTime += actionCooldown;
        if(anim != null) 
            anim.SetTrigger("attack");

    }

    private void BusyDecay()
    {
        busyTime = Mathf.Clamp(busyTime - Time.deltaTime, 0, Mathf.Infinity);
    }

    protected virtual void Act()
    {

    }

    protected virtual void Update()
    {
        SelectTarget();
        BusyDecay();

        if (!atributes.dead && busyTime == 0) Act();
    }

}
