using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    //Need to make add the components directly in code not in editor
    public Camera player_Camera; 
        


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // This will need to be changed for different weapon types //Make it so each weapon has it's own input settings
        if (Input.GetMouseButtonDown(0) )
        {
            
            GameObject fired_projectile = Object_Pooling_Script.Instance.GetProjectile();
            fired_projectile.transform.position = player_Camera.transform.position + player_Camera.transform.forward;
            fired_projectile.transform.forward = player_Camera.transform.forward;
        }
    }
}
