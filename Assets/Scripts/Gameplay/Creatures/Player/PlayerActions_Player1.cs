using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions_Player1 : PlayerActions
{
    [Header("IA")]
    public int currentWeapon;

    [Header("Components")]
    public float weaponRotationLerp = .03f;
    public float weaponRestRotationLerp = .3f;
    public GameObject weaponAxis;
    public CreatureBehavior behavior;
    private CreatureMovement movement;
    public GameObject[] weapon = new GameObject[2];
    public Animator[] weaponAnim = new Animator[2];

    //[Header ("Inactivator")]
    //public int inactivatorDamage = 5;

    private void Start()
    {
        behavior = GetComponentInChildren<CreatureBehavior>();
        movement = GetComponentInChildren<CreatureMovement>();
        currentWeapon = 0;
    }

    protected override void PlayerDead()
    {
        weaponAxis.SetActive(false);
        base.PlayerDead();

    }  

    private void AutoRotateWeapon(bool target)
    {
        float rotationZ;

        float lerpSpeed;

        if (target)
        {
            lerpSpeed = weaponRotationLerp;
            rotationZ = Calculations.GetRotationZToTarget(weaponAxis.transform.position, behavior.currentTarget.transform.position);
        }
        else
        {
            lerpSpeed = weaponRestRotationLerp;
            rotationZ = 0;
        }

        /*
        Vector3 targetRotation;
        if (rotationZ > 90 || rotationZ < -90)
            targetRotation = new Vector3 (0, 180, -rotationZ + 180);
        else
            targetRotation =  new Vector3 (0, 0, rotationZ);

        weaponAxis.transform.rotation = Quaternion.Euler(targetRotation);
        */


        //weaponAxis.transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, weaponAxis.transform.rotation.z), Quaternion.Euler(0, 0, rotationZ), lerpSpeed);

        weaponAxis.transform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }

    private void ToggleAttackEffect(bool on)
    {
        weaponAnim[currentWeapon].SetBool("toggle", on);
    }

    public void SwitchWeapons()
    {
        if (currentWeapon == 0) currentWeapon = 1;
        else currentWeapon = 0;

        //Activate appropriate weapon object
        for (int i = 0; i < weapon.Length; i++)
        {
            if (i == currentWeapon)
                weapon[i].SetActive(true);
            else
            {
                weapon[i].SetActive(false);
                weaponAnim[i].SetBool("toggle", false);
            }
        }

        //Reset targeting depending on weapon
        behavior.currentTarget = null;
        behavior.RestartCollider();
        behavior.factionsDetected.Clear();

        switch (currentWeapon)
        {
            //Inactivator
            case 0:
                behavior.factionsDetected.Add(Faction.Virus);
                break;
            //Sweeper
            case 1:
                behavior.factionsDetected.Add(Faction.Corpse);
                break;

        }

    }
    
    private void AutoAttack()
    {
        if (behavior.currentTarget != null)
        {
            AutoRotateWeapon(true);
            ToggleAttackEffect(true);

        }
        else
        {
            AutoRotateWeapon(false);
            ToggleAttackEffect(false);
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        AutoAttack();
    }
}
