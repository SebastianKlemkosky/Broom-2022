using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast_Gun_System_Script : MonoBehaviour
{
    //Gun Stats
    public int damage;
    public float time_Between_Shooting, spread, range, reload_Time, time_Between_Shots;
    public int magazine_Size, bullets_Per_Tap;
    public bool allow_Button_Hold;
    int bullets_Left, bullets_Shot;

    //bools 
    bool shooting, ready_To_Shoot, reloading;


    //References
    //To to link these in code
    public Camera player_Camera;
    public Transform attack_Point;
    public RaycastHit ray_Hit;
    public LayerMask what_Is_Enemy;


    //Graphics 
    //Can Add Camera Shake later if needed at 5:20
    //Can Add Other Graphic stuff later

    private void Start()
    {
        player_Camera = transform.root.GetComponentInChildren<Camera>();
        bullets_Left = magazine_Size;
        ready_To_Shoot = true; 
    }

    private void Update()
    {
        MyInput();
        //Set the Ammo count and mag stuff later
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

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate Direction with Spread
        Vector3 direction = player_Camera.transform.forward + new Vector3(x, y, 0);


        //Raycast
        if(Physics.Raycast(player_Camera.transform.position, direction, out ray_Hit, range, what_Is_Enemy))
        {
            Debug.Log(ray_Hit.collider.name);


            //Here Apply Damage to enemy if hit
            
            

        }

        //Input Graphics Here



        bullets_Left--;
        bullets_Shot--;

        Invoke("ResetShot", time_Between_Shooting);


        if (bullets_Shot > 0 && bullets_Left > 0)
        {
            Invoke("Shoot", time_Between_Shots);
        }

    }


    private void ResetShot()
    {
        ready_To_Shoot = true;
    }


    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reload_Time);
    }

    private void ReloadFinished()
    {
        bullets_Left = magazine_Size;
        reloading = false;
    }






}
