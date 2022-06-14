using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile_Weapon_System_Script : MonoBehaviour
{

    //projectile
    public GameObject shooting_Projectile;

    //projectile forces
    //public float shoot_Force, upward_Force;

    //Gun Stats
    public float time_Between_Shooting, spread, reload_Time, time_Between_Shots;
    public int magazine_Size, bullets_Per_Tap;
    public int max_Reserve_Ammo, reserve_Ammo;
    public bool allow_Button_Hold;
    int bullets_Left, bullets_Shot;
    

    //bools 
    bool shooting, ready_To_Shoot, reloading;


    //References
    //To to link these in code
    private Camera player_Camera;
    public Transform attack_Point;
    public Text ammo_Text;


    //Graphics 
    public Camera_Shake_Script camera_Shake;
    public float camera_Shake_Magnitude, camera_Shake_Duration;

    //Bug Fixing
    public bool allow_Invoke = true;



    public void Start()
    {
        player_Camera = transform.root.GetComponentInChildren<Camera>();
        camera_Shake = transform.root.GetComponentInChildren<Camera_Shake_Script>();
        
        bullets_Left = magazine_Size;
        ready_To_Shoot = true;
    }

 

    // Update is called once per frame
    void Update()
    {
        MyInput();
        if (reserve_Ammo > max_Reserve_Ammo)
        {
            reserve_Ammo = max_Reserve_Ammo;
        }

        ammo_Text.text = "Ammo: " + bullets_Left + " / " + magazine_Size + " | " + reserve_Ammo ;

    }

    private void MyInput()
    {
        if (allow_Button_Hold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }


        if (Input.GetKeyDown(KeyCode.R) && bullets_Left < magazine_Size && !reloading)
        {
            Reload();
        }

        if(ready_To_Shoot && shooting && !reloading && bullets_Left <= 0)
        {
            Reload();
        }



        //Shoot
        if (ready_To_Shoot && shooting && !reloading && bullets_Left > 0)
        {
            bullets_Shot = bullets_Per_Tap;
            Shoot();
        }

    }

    private void Shoot()
    {
        ready_To_Shoot = false;

        //Find the exact hit position using a raycast
        Ray ray = player_Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 target_Point;

        if (Physics.Raycast(ray, out hit))
        {
            target_Point = hit.point;
        }
        else
        {
            target_Point = ray.GetPoint(75);  //Find a point at this distance //Will need to change later
        }


        //Calculate the direction from attackPoint to targetPoint
        Vector3 direction_Without_Spread = target_Point - attack_Point.position;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //calculate new direction with spread
        Vector3 direction_With_Spread = direction_Without_Spread + new Vector3(x, y, 0);


        //Get the bullet 
        GameObject fired_projectile = Object_Pooling_Script.Instance.GetProjectile(true, shooting_Projectile);


        fired_projectile.transform.position = attack_Point.position;
        fired_projectile.transform.rotation = Quaternion.identity;

     



        //Rotate projectile to shoot in direction
        fired_projectile.transform.forward = direction_With_Spread.normalized;

        //Add forces to the bullet
        //fired_projectile.GetComponent<Rigidbody>().AddForce(direction_With_Spread * shoot_Force, ForceMode.Impulse);
        //fired_projectile.GetComponent<Rigidbody>().AddForce(player_Camera.transform.up * upward_Force, ForceMode.Impulse);

        //Shake Camera
        StartCoroutine(camera_Shake.Shake(camera_Shake_Duration, camera_Shake_Magnitude));

        bullets_Left--;
        bullets_Shot--;

        if (allow_Invoke)
        {
            Invoke("ResetShot", time_Between_Shooting);
            allow_Invoke = false;
        }
        
        //if more than one bullet shot per tap
        if (bullets_Shot > 0 && bullets_Left > 0)
        {
            Invoke("Shoot", time_Between_Shots);
        }


    }

    private void ResetShot()
    {
        ready_To_Shoot = true;
        allow_Invoke = true;
    }

    private void Reload()
    {
        //Check if there is any ammo in reserve
        if (reserve_Ammo > 0)
        {
            reloading = true;
            Invoke("ReloadFinished", reload_Time);
        }
        else
        {
            //Replace this later
            Debug.Log("Out of Ammo");
            
        }

    }

    private void ReloadFinished()
    {
        int bullets_needed;
        if (bullets_Left > 0)
        {
            bullets_needed = magazine_Size - bullets_Left;

        }
        else
        {
            bullets_needed = magazine_Size;
        }

        //Check if there is enough bullets for a full magizine
        if (reserve_Ammo > bullets_needed)
        {
            bullets_Left = magazine_Size;
            reserve_Ammo -= bullets_needed;
            
        }
        else
        {
            bullets_Left = reserve_Ammo + bullets_Left;
            reserve_Ammo = 0;
        }



        reloading = false;
    }



}
