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
        Debug.Log("weapon picked");
        currentWeaponInHand = Instantiate(weapon, weaponHeld.transform);
        Destroy(currentWeaponInSlot);
    }

    public void SlotWeapon()
    {
        currentWeaponInSlot = Instantiate(weapon, weaponSlot.transform);
        Destroy(currentWeaponInHand);
    }
    // Update is called once per frame

    //public void CastFire()
    //{
    //    currentFire.Play();
    //}
    void Update()
    {
        
    }
}
