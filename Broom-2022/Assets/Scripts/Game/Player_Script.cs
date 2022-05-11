using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    //Need to make add the components directly in code not in editor
    private Camera player_Camera;



    public int initial_Projectile_Ammo = 12;
    private int projectile_Ammo;
    public int Ammo { get { return projectile_Ammo; } }

    // Start is called before the first frame update
    void Start()
    {
        player_Camera = GetComponentInChildren<Camera>();
        projectile_Ammo = initial_Projectile_Ammo;
    }

    // Update is called once per frame
    void Update()
    {
        // This will need to be changed for different weapon types //Make it so each weapon has it's own input settings
        if (Input.GetMouseButtonDown(0) )
        {
            if(projectile_Ammo > 0)
            {
                projectile_Ammo--;
                GameObject fired_projectile = Object_Pooling_Script.Instance.GetProjectile();
                fired_projectile.transform.position = player_Camera.transform.position + player_Camera.transform.forward;
                fired_projectile.transform.forward = player_Camera.transform.forward;
            }

        }
    }




    //Check for collisions
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //If player collides with a Ammo_crate
        if (hit.collider.GetComponent<Ammo_Crate>() != null)
        {
            Ammo_Crate ammo_Crate = hit.collider.GetComponent<Ammo_Crate>();
            projectile_Ammo += ammo_Crate.ammo;
            Destroy(ammo_Crate.gameObject);
        }

    }

}
