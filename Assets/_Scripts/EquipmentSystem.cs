using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSystem: MonoBehaviour
{
    [SerializeField] GameObject weaponHeld;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject weaponSlot;

    GameObject currentWeaponInHand;
    GameObject currentWeaponInSlot;

    //[SerializeField] GameObject Fire;
    ParticleSystem currentFire;

    // Start is called before the first frame update
    void Start()
    {
        //currentFire = Fire.GetComponent<ParticleSystem>();
        
        currentWeaponInSlot = Instantiate(weapon, weaponSlot.transform);
    }

    public void DrawWeapon()
    {
        if (weapon == null || weaponHeld == null)
        {
            Debug.LogError("EquipmentSystem: weapon or weaponHeld is null", gameObject);
            return;
        }

        currentWeaponInHand = Instantiate(weapon, weaponHeld.transform);
        if (currentWeaponInSlot != null)
            Destroy(currentWeaponInSlot);
    }

    public void SlotWeapon()
    {
        if (weapon == null || weaponSlot == null)
        {
            Debug.LogError("EquipmentSystem: weapon or weaponSlot is null", gameObject);
            return;
        }

        currentWeaponInSlot = Instantiate(weapon, weaponSlot.transform);
        if (currentWeaponInHand != null)
            Destroy(currentWeaponInHand);
    }
}
