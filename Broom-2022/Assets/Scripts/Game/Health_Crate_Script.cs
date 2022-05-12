using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Crate_Script : MonoBehaviour
{
    
    public GameObject container;
    public float rotationSpeed = 180f;

    public int health_Amount = 25;

    private void Start()
    {
        container = transform.GetChild(0).gameObject;   

    }
    void Update()
    {
        container.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
