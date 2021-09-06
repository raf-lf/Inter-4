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
        weapon.transform.rotation = Quaternion.Euler(0,0,Calculations.GetRotationZToTarget(weapon.transform.position, mouseTarget));
    }
        

    private void SwitchAttackEffect(bool on)
    {
        GetComponent<PlayerMovement>().haltMovement = on;
        attackAnimator.SetBool("toggle", on);

        weapon.GetComponentInChildren<DamageOnContact>().damage = (int)(inactivatorDamage * GetComponent<CreatureAtributes>().damageModifier);

    }

    private void Update()
    {

        if (Input.GetMouseButton(1))
        {
            SwitchAttackEffect(true);
            RotateWeapon();

        }
        else
        {
            SwitchAttackEffect(false);
        }
    }
}
