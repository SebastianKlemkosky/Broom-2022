using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sprite_Look_Script : MonoBehaviour
{

    //get player's position
    private Transform target;
    public bool can_Look_Vertically;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {

        if (can_Look_Vertically)
        {
            transform.LookAt(target);

        }
        else
        {
            Vector3 modified_Target = target.position;
            modified_Target.y = transform.position.y;

            transform.LookAt(modified_Target);


        }


    }
}
