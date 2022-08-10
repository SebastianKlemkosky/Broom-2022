using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Pickup_Script : MonoBehaviour
{
    public GameObject weapon_Type;
    public GameObject weaponModel;
    
    public float rotation_Speed = 180f;
    


    private void Start()
    {

        

    }

    void Update()
    {
        weaponModel.transform.Rotate(Vector3.up * rotation_Speed * Time.deltaTime);
    }
}
