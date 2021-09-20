using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public Animator attackAnimator;
    public GameObject weapon;

    [Header("Attacks")]
    public int inactivatorDamage = 5;


    private void RotateWeapon()
    {
        Vector3 mouseTarget = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        weapon.transform.rotation = Quaternion.Euler(0, 0, Calculations.GetRotationZToTarget(weapon.transform.position, mouseTarget));
    }
    private void AutoRotateWeapon()
    {
        weapon.transform.rotation = Quaternion.Lerp(weapon.transform.rotation, Quaternion.Euler(0, 0, Calculations.GetRotationZToTarget(weapon.transform.position, GetComponentInChildren<CreatureBehavior>().currentTarget.transform.position)),.1f);
      //  weapon.transform.rotation = Quaternion.Euler(0, 0, Calculations.GetRotationZToTarget(weapon.transform.position, GetComponentInChildren<CreatureBehavior>().currentTarget.transform.position));
    }

    private void ResetWeaponRotation()
    {
        if (weapon.transform.rotation.z != 0) weapon.transform.rotation = Quaternion.Lerp(weapon.transform.rotation, Quaternion.Euler(0, 0, 0), .03f);

    }

    private void SwitchAttackEffect(bool on)
    {
        attackAnimator.SetBool("toggle", on);

        if (on)
            weapon.GetComponentInChildren<DamageCreature>().damage = (int)(inactivatorDamage * GetComponent<CreatureAtributes>().damageModifier);

    }
    /*
    private void SwitchAttackEffect(bool on)
    {
        GetComponent<PlayerMovement>().haltMovement = on;
        attackAnimator.SetBool("toggle", on);

        weapon.GetComponentInChildren<DamageOnContact>().damage = (int)(inactivatorDamage * GetComponent<CreatureAtributes>().damageModifier);

    }
    */

    private void AutoAttack()
    {
        if (GetComponentInChildren<CreatureBehavior>().currentTarget != null)
        {
            if (!GetComponent<PlayerMovement>().moving)
            {
                AutoRotateWeapon();
                SwitchAttackEffect(true);
            }
            else
            {
                ResetWeaponRotation();
                SwitchAttackEffect(false);
            }
        }
        else
        {
            ResetWeaponRotation();
            SwitchAttackEffect(false);
        }
    }

    private void Attack()
    {
        SwitchAttackEffect(true);
        RotateWeapon();
    }

    private void Update()
    {
        AutoAttack();

        /*
        if (Input.GetMouseButton(1))
        {
            Attack();

        }
        else
        {
            SwitchAttackEffect(false);
        }
        */
    }
}
