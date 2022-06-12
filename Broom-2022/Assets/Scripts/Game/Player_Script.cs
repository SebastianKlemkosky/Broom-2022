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

    public float knockback_Force = 10;
    private bool is_Hurt;
    public float hurt_Duration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        player_Camera = GetComponentInChildren<Camera>();
        player_health = intial_Health;
    }

    // Update is called once per frame
    void Update()
    {
        
       

        
    }


    //Check for collisions
    void OnTriggerEnter(Collider otherCollider)
    {
        //If player collides with a Ammo_crate
        if (otherCollider.GetComponent<Ammo_Crate>() != null)
        {
            
            Ammo_Crate ammo_Crate = otherCollider.GetComponent<Ammo_Crate>();
            

            //Award ammo to the correct weapon based on ammo crate type


            Destroy(ammo_Crate.gameObject);
            

            Debug.Log("Claimed Ammo Crate");


        }

        if (otherCollider.GetComponent<Health_Crate_Script>() != null)
        {
            Health_Crate_Script health_Crate = otherCollider.GetComponent<Health_Crate_Script>();
            player_health += health_Crate.health_Amount;
            Destroy(health_Crate.gameObject);
        }

        //Touching Enemies
        //If the player is getting hurt
        if (is_Hurt == false)
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
                is_Hurt = true;

                //Perform the knockback effect
                Vector3 hurtDirection = (transform.position - hazard.transform.position).normalized;
                Vector3 knockbackDirection = (hurtDirection + Vector3.up).normalized;
                GetComponent<ForceReceiver>().AddForce(knockbackDirection, knockback_Force);

                StartCoroutine(HurtRoutine());
            }
        }




    }

    IEnumerator HurtRoutine()
    {
        yield return new WaitForSeconds(hurt_Duration);
        is_Hurt = false;
    }


}
