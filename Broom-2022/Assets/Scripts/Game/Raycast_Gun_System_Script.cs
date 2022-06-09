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
    public Camera_Shake_Script camera_Shake;
    public float camera_Shake_Magnitude, camera_Shake_Duration;

    public ParticleSystem shooting_System;
    public ParticleSystem impact_Particles_System;
    public TrailRenderer bullet_Trail;


    

    private void Start()
    {
        player_Camera = transform.root.GetComponentInChildren<Camera>();
        camera_Shake = transform.root.GetComponentInChildren<Camera_Shake_Script>();
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

            //Play Shooting system
            shooting_System.Play();

            Debug.Log(ray_Hit.collider.name);

            TrailRenderer trail = Instantiate(bullet_Trail, attack_Point.transform.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, ray_Hit));

            //Here Apply Damage to enemy if hit
            if (ray_Hit.collider.GetComponent<Enemy_Script>() != null)
            {
                Enemy_Script enemy = ray_Hit.collider.GetComponent<Enemy_Script>();

                enemy.OnRayHit(damage);
                
            }


        }

        //Input Graphics Here

        //Shake Camera
        StartCoroutine(camera_Shake.Shake(camera_Shake_Duration, camera_Shake_Magnitude));


        //Bullet Graphics

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



    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit Hit)
    {
        float time = 0;
        Vector3 start_Position = trail.transform.position;


        while(time < 1)
        {
            trail.transform.position = Vector3.Lerp(start_Position, Hit.point, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }


        trail.transform.position = Hit.point;
        Instantiate(impact_Particles_System, Hit.point, Quaternion.LookRotation(Hit.normal));


        Destroy(trail.gameObject, trail.time);


    }


}
