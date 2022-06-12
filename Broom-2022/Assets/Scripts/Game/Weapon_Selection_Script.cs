using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This Script Handles Weapon Selection
public class Weapon_Selection_Script : MonoBehaviour
{

    public int selected_Weapon = 0; //The index of the selectedWeapon

    //Make a list of the current weapons held by the player
    private List<GameObject> current_Weapon_List;


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
