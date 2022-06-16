using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour
{

    public int health = 5;
    public int damage = 5;

    private bool killed = false;
 

    private void OnTriggerEnter(Collider otherCollider)
    {
        //If the enemy gets hit by a projectile
        //Will need to make this work with all projectile types
        if (otherCollider.GetComponent<Projectile_Bullet_Script>() != null)
        {
            Projectile_Bullet_Script projectile = otherCollider.GetComponent<Projectile_Bullet_Script>();

            if (projectile.Shot_By_Player == true)
            {
                health -= projectile.damage;

                projectile.gameObject.SetActive(false);


                if (health <= 0)
                {
                    if (killed == false)
                    {
                        killed = true;
                        OnKill();
                    }
                    
                    
                }
            }
            
       

        }
    }

    //When the enemy gets hit by a raycast apply the damage and Kill the enemy if needed
    public void OnRayHit(int d)
    {
        health -= d;
        if (health <= 0)
        {
            if (killed == false)
            {
                killed = true;
                OnKill();
            }


        }
    }


    protected virtual void  OnKill()
    {

    }

}
