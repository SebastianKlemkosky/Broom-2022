using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This Script Handles Weapon Selection
public class Weapon_Selection_Script : MonoBehaviour
{

    //This script needs hold a reference to all the weapon models
    //Get a list of all the child objects in the weapons GameObject 


    //activates the current weapon, the deactivates the others

    //Using said list do this ^

    //Also needs to switch the weapons projectile/ bullets / method of damage to the current weapon's one and deactives the others
    //Will need to make use of the object pooling manager, and projectile bullet scripts

    public int selected_Weapon = 0; //The index of the selectedWeapon




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

}
