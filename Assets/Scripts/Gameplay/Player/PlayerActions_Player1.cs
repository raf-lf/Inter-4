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
    public GameObject[] weapon = new GameObject[2];
    public Animator[] weaponAnim = new Animator[2];

    //[Header ("Inactivator")]
    //public int inactivatorDamage = 5;



    protected override void Attack()
    {
        base.Attack();

        ToggleAttackEffect(true);
        RotateWeapon();
    }

    private void RotateWeapon()
    {
        Vector3 mouseTarget = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        weaponAxis.transform.rotation = Quaternion.Euler(0, 0, Calculations.GetRotationZToTarget(weaponAxis.transform.position, mouseTarget));
    }

    private void AutoRotateWeapon()
    {
        weaponAxis.transform.rotation = Quaternion.Lerp(weaponAxis.transform.rotation, Quaternion.Euler(0, 0, Calculations.GetRotationZToTarget(weaponAxis.transform.position, GetComponentInChildren<CreatureBehavior>().currentTarget.transform.position)), weaponRotationLerp);
        //  weapon.transform.rotation = Quaternion.Euler(0, 0, Calculations.GetRotationZToTarget(weapon.transform.position, GetComponentInChildren<CreatureBehavior>().currentTarget.transform.position));
    }

    private void ResetWeaponRotation()
    {
        if (weaponAxis.transform.rotation.z != 0) weaponAxis.transform.rotation = Quaternion.Lerp(weaponAxis.transform.rotation, Quaternion.Euler(0, 0, 0), weaponRestRotationLerp);

    }

    private void ToggleAttackEffect(bool on)
    {
        weaponAnim[currentWeapon].SetBool("toggle", on);
                

    }
    /*
    private void SwitchAttackEffect(bool on)
    {
        GetComponent<PlayerMovement>().haltMovement = on;
        attackAnimator.SetBool("toggle", on);

        weapon.GetComponentInChildren<DamageOnContact>().damage = (int)(inactivatorDamage * GetComponent<CreatureAtributes>().damageModifier);

    }
    */
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
        GetComponentInChildren<CreatureBehavior>().currentTarget = null;
        GetComponentInChildren<CreatureBehavior>().RestartCollider();
        GetComponentInChildren<CreatureBehavior>().factionsDetected.Clear();

        switch (currentWeapon)
        {
            //Inactivator
            case 0:
                GetComponentInChildren<CreatureBehavior>().factionsDetected.Add(Faction.Virus);
                break;
            //Sweeper
            case 1:
                GetComponentInChildren<CreatureBehavior>().factionsDetected.Add(Faction.Corpse);
                break;

        }

    }
    private void AutoAttack()
    {
        if (GetComponentInChildren<CreatureBehavior>().currentTarget != null)
        {
            AutoRotateWeapon();
            ToggleAttackEffect(true);

            /*
            if (!GetComponent<PlayerMovement>().moving)
            {
                AutoRotateWeapon();
                ToggleAttackEffect(true);
            }
            else
            {
                ResetWeaponRotation();
                ToggleAttackEffect(false);
            }
            */
        }
        else
        {
            ResetWeaponRotation();
            ToggleAttackEffect(false);
        }
    }

    protected override void Update()
    {
        base.Update();

        AutoAttack();
    }
}
