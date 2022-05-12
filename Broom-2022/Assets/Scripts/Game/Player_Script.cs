using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    //Need to make add the components directly in code not in editor
    private Camera player_Camera;

    public int intial_Health = 100;
    private int player_health; 
    public int Player_Health { get { return player_health; } }


    public int initial_Projectile_Ammo = 12;
    private int projectile_Ammo;
    public int Ammo { get { return projectile_Ammo; } }


    public float knockbackForce = 10;
    private bool isHurt;
    public float hurtDuration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        player_Camera = GetComponentInChildren<Camera>();
        projectile_Ammo = initial_Projectile_Ammo;
        player_health = intial_Health;
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
                GameObject fired_projectile = Object_Pooling_Script.Instance.GetProjectile(true);
                fired_projectile.transform.position = player_Camera.transform.position + player_Camera.transform.forward;
                fired_projectile.transform.forward = player_Camera.transform.forward;
            }

        }
    }




    //Check for collisions
    void OnTriggerEnter(Collider otherCollider)
    {
        //If player collides with a Ammo_crate
        if (otherCollider.GetComponent<Ammo_Crate>() != null)
        {
            Ammo_Crate ammo_Crate = otherCollider.GetComponent<Ammo_Crate>();
            projectile_Ammo += ammo_Crate.ammo;
            Destroy(ammo_Crate.gameObject);


        }

        if (otherCollider.GetComponent<Health_Crate_Script>() != null)
        {
            Health_Crate_Script health_Crate = otherCollider.GetComponent<Health_Crate_Script>();
            player_health += health_Crate.health_Amount;
            Destroy(health_Crate.gameObject);
        }

        //Touching Enemies
        //If the player is getting hurt
        if (isHurt == false)
        {
            GameObject hazard = null;
            if (otherCollider.GetComponent<Enemy_Script>() != null)
            {
                
                Enemy_Script enemy = otherCollider.GetComponent<Enemy_Script>();
                hazard = enemy.gameObject;
                player_health -= enemy.damage;

            } else if(otherCollider.GetComponent<Projectile_Bullet_Script>() != null)
            {
                Projectile_Bullet_Script projectile = otherCollider.GetComponent<Projectile_Bullet_Script>();
                if(projectile.Shot_By_Player == false)
                {
                    hazard = projectile.gameObject;
                    player_health -= projectile.damage;
                    projectile.gameObject.SetActive(false);
                }
            }

            if(hazard != null)
            {
                isHurt = true;

                //Perform the knockback effect
                Vector3 hurtDirection = (transform.position - hazard.transform.position).normalized;
                Vector3 knockbackDirection = (hurtDirection + Vector3.up).normalized;
                GetComponent<ForceReceiver>().AddForce(knockbackDirection, knockbackForce);

                StartCoroutine(HurtRoutine());
            }
        }




    }

    IEnumerator HurtRoutine()
    {
        yield return new WaitForSeconds(hurtDuration);
        isHurt = false;
    }


}
