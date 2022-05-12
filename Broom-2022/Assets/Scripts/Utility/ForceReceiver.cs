using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manager of Forces on character controllers
public class ForceReceiver : MonoBehaviour
{
    
    public float decleration = 5;       //How quickly back to zero
    public float mass = 3;              // mass of the object

    private Vector3 intensity;
    private CharacterController character;

    // Start is called before the first frame update
    void Start()
    {
        intensity = Vector3.zero;
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //If the intensity is greater than the threshold
        if (intensity.magnitude > 0.2f)
        {
            character.Move(intensity * Time.deltaTime);
        }

        intensity = Vector3.Lerp(intensity, Vector3.zero, decleration * Time.deltaTime);
    }


    public void AddForce(Vector3 direction, float force)
    {
        intensity += direction.normalized * force / mass;
    }

}
