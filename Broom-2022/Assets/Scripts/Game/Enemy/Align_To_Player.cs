using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align_To_Player : MonoBehaviour
{

    private Transform player;
    private Vector3 target_Position;
    private Vector3 target_Direction;


    //temp
    public SpriteRenderer sprite_Renderer;
    public Sprite[] sprites;

    private float angle;
    private int last_Index;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        sprite_Renderer = GetComponentInChildren<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //Get Target Postion and Direction
        target_Position = new Vector3(player.position.x, transform.position.y, player.position.z);
        target_Direction = target_Position - transform.position;


        //Get Angle 
        angle = Vector3.SignedAngle(target_Direction, transform.forward, Vector3.up);



        //Flip Sprite if needed
        Vector3 temp_Scale = Vector3.one;
        if(angle > 0)
        {
            temp_Scale.x *= -1;
        }


        sprite_Renderer.transform.localScale = temp_Scale;

        last_Index = GetIndex(angle);

        sprite_Renderer.sprite = sprites[last_Index];
    }


    //Get the Index in relation to the player from the angles
    private int GetIndex(float angle)
    {
        //front
        if (angle > -22.5f && angle < 22.6f)
            return 0;
        if (angle >= 22.5f && angle < 67.5f)
            return 7;
        if (angle >= 67.5f && angle < 112.5f)
            return 6;
        if (angle >= 112.5f && angle < 157.5f)
            return 5;


        //back
        if (angle <= -157.5 || angle >= 157.5f)
            return 4;
        if (angle >= -157.4f && angle < -112.5f)
            return 3;
        if (angle >= -112.5f && angle < -67.5f)
            return 2;
        if (angle >= -67.5f && angle <= -22.5f)
            return 1;

        return last_Index;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, target_Position);
    }
}
