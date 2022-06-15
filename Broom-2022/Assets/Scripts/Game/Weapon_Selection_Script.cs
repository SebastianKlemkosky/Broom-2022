using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This Script Handles Weapon Selection
public class Weapon_Selection_Script : MonoBehaviour
{

    public int selected_Weapon = 0; //The index of the selectedWeapon

    public List<GameObject> current_Weapon_List;
    private List<GameObject> complete_Weapon_List;

    private void Awake()
    {
        complete_Weapon_List = new List<GameObject>(Resources.LoadAll<GameObject>("Weapons"));
        IntializeCurrentWeaponList();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
        
        SelectedWeapon();
    }





    // Update is called once per frame
    void Update()
    {

        //This handles Weapon Switching with the mouse scroll wheel
        int previous_Selected_Weapon = selected_Weapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {

            if (selected_Weapon >= transform.childCount - 1)
            {
                selected_Weapon = 0;
            }
            else
            {
                selected_Weapon++;
            }
            
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {

            if (selected_Weapon <= 0)
            {
                selected_Weapon = transform.childCount - 1;
            }
            else
            {
                selected_Weapon--;
            }

        }

        if (previous_Selected_Weapon != selected_Weapon)
        {
            SelectedWeapon();
        }


    }


    void SelectedWeapon()
    {
        //Loop through all weapons and disable all except the current weapon
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if (i == selected_Weapon)
            {
                weapon.gameObject.SetActive(true);

            }
            else
            {
                weapon.gameObject.SetActive(false);

            }

            i++;
        }
    }

   private void IntializeCurrentWeaponList()
    {
        current_Weapon_List = new List<GameObject>();
        foreach (Transform child in transform)
        {
            current_Weapon_List.Add(child.gameObject);
        }


    }

    public void AddAmmo(GameObject w, int ammo)
    {

        foreach (GameObject weapon in current_Weapon_List)
        {
            if (weapon.name == w.name)
            {
                if(weapon.GetComponent<Raycast_Gun_System_Script>() != null)
                {
                    weapon.GetComponent<Raycast_Gun_System_Script>().reserve_Ammo += ammo;

                    if (weapon.GetComponent<Raycast_Gun_System_Script>().reserve_Ammo 
                        > weapon.GetComponent<Raycast_Gun_System_Script>().max_Reserve_Ammo)
                    {
                        weapon.GetComponent<Raycast_Gun_System_Script>().reserve_Ammo
                            = weapon.GetComponent<Raycast_Gun_System_Script>().max_Reserve_Ammo;
                    }
                }
                else if(weapon.GetComponent<Projectile_Weapon_System_Script>() != null)
                {
                    weapon.GetComponent<Projectile_Weapon_System_Script>().reserve_Ammo += ammo;

                    if (weapon.GetComponent<Projectile_Weapon_System_Script>().reserve_Ammo 
                        > weapon.GetComponent<Projectile_Weapon_System_Script>().max_Reserve_Ammo)
                    {
                        weapon.GetComponent<Projectile_Weapon_System_Script>().reserve_Ammo 
                            = weapon.GetComponent<Projectile_Weapon_System_Script>().max_Reserve_Ammo;
                    }
                    

                }


            }
        }





    }

}
