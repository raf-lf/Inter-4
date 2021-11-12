using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    private void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
    }
    public void Break()
    {
        GetComponentInChildren<Animator>().SetTrigger("break");

    }
}
