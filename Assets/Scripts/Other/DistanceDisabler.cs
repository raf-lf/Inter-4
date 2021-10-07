using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceDisabler : MonoBehaviour
{
    public float disableDistance;
    public GameObject disableableObject;

    void Start()
    {
        GetComponent<CircleCollider2D>().radius = disableDistance;

        disableableObject.SetActive(false);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            disableableObject.SetActive(true);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            disableableObject.SetActive(false);

        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, disableDistance);
    }
}
