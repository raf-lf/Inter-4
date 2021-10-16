using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSweeper : MonoBehaviour
{
    private bool active;
    private Animator weaponAnim;
    public int sweepFrameInterval;
    public CircleCollider2D sweeperCollider;

    private void Start()
    {
        weaponAnim = GetComponent<Animator>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<VirusCorpse>())
        {
            collision.gameObject.GetComponent<VirusCorpse>().Collect();

        }
    }

    private void Sweep()
    {
        if(Time.frameCount % sweepFrameInterval == 0)
        {
            RaycastHit2D[] objectsSweeped = Physics2D.CircleCastAll(sweeperCollider.transform.position, sweeperCollider.radius, Vector2.zero);

            foreach(RaycastHit2D hit in objectsSweeped)
            {
                if(hit.collider.gameObject.GetComponent<VirusCorpse>())
                {
                    hit.collider.gameObject.GetComponent<VirusCorpse>().Collect();
                }
            }
        }
    }

    public void WeaponActivation(bool on)
    {
        active = on;
        weaponAnim.SetBool("on", on);
    }

    private void Update()
    {
        Sweep();
    }
}
