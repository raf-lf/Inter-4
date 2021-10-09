using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallParticle : MonoBehaviour
{
    public Vector2 angleVariance;
    public Vector2 sizeVariance;
    public Vector2 positionVariance;

    public Sprite[] spriteVariants = new Sprite[0];

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        ShuffleSprite();
    }

    private void ShuffleSprite()
    {
        //Randomizes sprite
        sr.sprite = spriteVariants[Random.Range(0,spriteVariants.Length)];

        //Randomizes all of sprite's transform. Note that collider is unnafected.
        sr.gameObject.transform.rotation = Quaternion.Euler(0, 0, Random.Range(angleVariance.x, angleVariance.y));

        float scaleRoll = Random.Range(sizeVariance.x, sizeVariance.y);
        sr.gameObject.transform.localScale = new Vector3(scaleRoll, scaleRoll, 0);

        sr.gameObject.transform.position += new Vector3(Random.Range(positionVariance.x, positionVariance.y), Random.Range(positionVariance.x, positionVariance.y), Random.Range(positionVariance.x, positionVariance.y));

        //Randomizes sprite's sorting, to minimize texture clipping
        sr.sortingOrder = Random.Range(0, 100);
    }
}
