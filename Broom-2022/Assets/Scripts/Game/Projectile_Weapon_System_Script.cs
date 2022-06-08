using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Weapon_System_Script : MonoBehaviour
{

    public GameObject shooting_Projectile;

    private Camera player_Camera;

    public int initial_Projectile_Ammo = 12;
    private int projectile_Ammo;

    public int Ammo { get { return projectile_Ammo; } }



    //Graphics 
    public Camera_Shake_Script camera_Shake;
    public float camera_Shake_Magnitude, camera_Shake_Duration;

    // Start is called before the first frame update
    void Start()
    {
        player_Camera = transform.root.GetComponentInChildren<Camera>();
        camera_Shake = transform.root.GetComponentInChildren<Camera_Shake_Script>();
        projectile_Ammo = initial_Projectile_Ammo;
    }

    // Update is called once per frame
    void Update()
    {
        // This will need to be changed for different weapon types //Make it so each weapon has it's own input settings
        if (Input.GetMouseButtonDown(0))
        {
            if (projectile_Ammo > 0)
            {
                projectile_Ammo--;
                GameObject fired_projectile = Object_Pooling_Script.Instance.GetProjectile(true, shooting_Projectile);
                fired_projectile.transform.position = player_Camera.transform.position + player_Camera.transform.forward;
                fired_projectile.transform.forward = player_Camera.transform.forward;

                //Shake Camera
                StartCoroutine(camera_Shake.Shake(camera_Shake_Duration, camera_Shake_Magnitude));


            }

        }
    }
}
