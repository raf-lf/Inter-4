using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalTracker : MonoBehaviour
{
    public Transform targetTransform;
    public Transform icon;
    private Animator anim;
    public float fadeDistance;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        transform.position = GameManager.scriptPlayer.transform.position;
        transform.parent = GameManager.scriptPlayer.transform;
    }

    private void Update()
    {
        if (targetTransform != null)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, Calculations.GetRotationZToTarget(transform.position, targetTransform.position)));

            if (Vector2.Distance(GameManager.scriptPlayer.transform.position, targetTransform.position) <= fadeDistance)
                anim.SetBool("hide", true);
            else
                anim.SetBool("hide", false);

        }
        else
            anim.SetBool("hide", true);
    }

}
