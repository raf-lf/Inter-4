using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallParticle : MonoBehaviour
{
    public Sprite[] spriteVariants = new Sprite[0];
    public Vector2 angleVariance;
    public Vector2 sizeVariance;
    public Vector2 positionVariance;

    private void Start()
    {
        ShuffleSprite();
    }

    private void ShuffleSprite()
    {
        GameObject spriteObject = GetComponentInChildren<SpriteRenderer>().gameObject;
        GetComponentInChildren<SpriteRenderer>().sprite = spriteVariants[Random.Range(0,spriteVariants.Length)];

        spriteObject.transform.rotation = Quaternion.Euler(0, 0, Random.Range(angleVariance.x, angleVariance.y));

        float scaleRoll = Random.Range(sizeVariance.x, sizeVariance.y);
        spriteObject.transform.localScale = new Vector3(scaleRoll, scaleRoll, 0);

        spriteObject.transform.position += new Vector3(Random.Range(positionVariance.x, positionVariance.y), Random.Range(positionVariance.x, positionVariance.y), Random.Range(positionVariance.x, positionVariance.y));

    }
}
