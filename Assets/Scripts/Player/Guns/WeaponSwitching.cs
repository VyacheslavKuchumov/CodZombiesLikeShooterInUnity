using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Transform[] weapons;

    /*[Header("Keys")]
    [SerializeField] private KeyCode[] keys;*/

    [Header("Settings")]
    [SerializeField] private float switchTime;

    private int selectedWeapon = 0;
    private float timeSinceLastSwitch;

    private bool isSwithingWeapon;

    private void Start()
    {
        SetWeapons();
        Select(selectedWeapon);

        timeSinceLastSwitch = 0f;
    }

    private void SetWeapons()
    {
        weapons = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            weapons[i] = transform.GetChild(i);

        
    }

    private void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        timeSinceLastSwitch += Time.deltaTime;
        if (timeSinceLastSwitch >= switchTime && isSwithingWeapon)
        {
            timeSinceLastSwitch = 0;
            selectedWeapon = previousSelectedWeapon+1;
            if (selectedWeapon > weapons.Length-1)
            {
                selectedWeapon = 0;
            }
            

            Select(selectedWeapon);
            isSwithingWeapon = false;
        }
            

        

        
    }

    private void Select(int weaponIndex)
    {
        Debug.Log(weaponIndex);
        for (int i = 0; i < weapons.Length; i++)
            weapons[i].gameObject.SetActive(i == weaponIndex);

        timeSinceLastSwitch = 0f;

        OnWeaponSelected();
    }

    private void OnWeaponSelected() { }

    public void ChangeWeapon()
    {
        Debug.Log("Trying to change weapon");
        isSwithingWeapon = true;
    }
}