using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Bullet_Script : MonoBehaviour
{

    public float speed = 8f;    //Speed of the projectile
    public float duration = 2f; //duration of the bullet after firing
    public int damage = 5;
    private float life_Timer;

    private bool shot_By_Player;
    public bool Shot_By_Player { get { return shot_By_Player; } set { shot_By_Player = value; } }

    void OnEnable()
    {
        life_Timer = duration;

    }

    // Update is called once per frame
    void Update()
    {

       //Make the projectile move if there is no rigidbody component
      
       transform.position += transform.forward * speed * Time.deltaTime;
        
        

        //Check if the bullet should be set inactive
        life_Timer -= Time.deltaTime;
        //If the life_Time reaches zero than inactivate the projectile
        if (life_Timer <= 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
