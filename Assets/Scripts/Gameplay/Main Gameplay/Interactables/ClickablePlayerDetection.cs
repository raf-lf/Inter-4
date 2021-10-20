using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickablePlayerDetection : MonoBehaviour
{
    private Clickable scriptClickable;

    private void Start()
    {
        scriptClickable = GetComponentInParent<Clickable>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerAtributes>())
            scriptClickable.PlayerNearby(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerAtributes>())
            scriptClickable.PlayerNearby(false);

    }
}
