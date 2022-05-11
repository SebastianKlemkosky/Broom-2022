using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Crate : MonoBehaviour
{

    public GameObject container;
    public float rotation_Speed = 180f;
    public int ammo = 12;

    private void Start()
    {
        container = transform.GetChild(0).gameObject;   //This should get the first child object of the AmmoCrate Script

    }

    void Update()
    {
        container.transform.Rotate(Vector3.up * rotation_Speed * Time.deltaTime);
    }
}
