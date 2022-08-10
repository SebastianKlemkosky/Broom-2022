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


    //Check if the weapon exists in the players inventory
    //If it doesnt then instantiate it into the inventory
    //else just add some ammo 
    public void AddWeapon(Weapon_Pickup_Script weaponPickup)
    {

        GameObject w = weaponPickup.weapon_Type;
        foreach (GameObject weapon in current_Weapon_List)
        {
            if (weapon.name == w.name)
            {
                Debug.Log("Player is already carrying this weapon");
                //Add Ammo or something here later
                return;
            }
        }


        for(int i = 0; i < complete_Weapon_List.Count; i++)
        {
            if (complete_Weapon_List[i].name == w.name)
            {
                GameObject newWeapon = Instantiate(complete_Weapon_List[i], transform.position, transform.rotation, transform);
                newWeapon.name = w.name;
                IntializeCurrentWeaponList();
                selected_Weapon = i;
                SelectedWeapon();
                return;
            }
        }
        



 
    }

    public void AddAmmo(Ammo_Crate ammo_Crate)
    {

        int ammo = ammo_Crate.container_Ammo;
        GameObject w = ammo_Crate.weapon_Type;

  


        foreach (GameObject weapon in current_Weapon_List)
        {
            if (weapon.name == w.name)
            {
                //for raycast weapons
                if (weapon.GetComponent<Raycast_Gun_System_Script>() != null)
                {
                    Raycast_Gun_System_Script current_Weapon = weapon.GetComponent<Raycast_Gun_System_Script>();

                    //Check if the player already has max ammo in the gun
                    if (current_Weapon.reserve_Ammo != current_Weapon.max_Reserve_Ammo)
                    {
                        current_Weapon.reserve_Ammo += ammo;
                        Debug.Log("Claimed Ammo Crate");
                        Destroy(ammo_Crate.gameObject);
                    }

                    if (current_Weapon.reserve_Ammo
                        > current_Weapon.max_Reserve_Ammo)
                    {
                        current_Weapon.reserve_Ammo
                            = current_Weapon.max_Reserve_Ammo;
                    }
                }
                //For projectile weapons
                else if (weapon.GetComponent<Projectile_Weapon_System_Script>() != null)
                {
                    Projectile_Weapon_System_Script current_Weapon = weapon.GetComponent<Projectile_Weapon_System_Script>();

                    //Check if the player already has max ammo in the gun
                    if (current_Weapon.reserve_Ammo != current_Weapon.max_Reserve_Ammo)
                    {
                        current_Weapon.reserve_Ammo += ammo;
                        Debug.Log("Claimed Ammo Crate");
                        Destroy(ammo_Crate.gameObject);
                    }

                    if (current_Weapon.reserve_Ammo
                        > current_Weapon.max_Reserve_Ammo)
                    {
                        current_Weapon.reserve_Ammo
                            = current_Weapon.max_Reserve_Ammo;
                    }


                }
                



            }
        }




    }



}
