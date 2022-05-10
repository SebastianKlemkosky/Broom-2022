using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//This Script handles object pooling for the game
public class Object_Pooling_Script : MonoBehaviour
{

    private static Object_Pooling_Script instance;
    public static Object_Pooling_Script Instance { get { return instance; } }

    //This is for the Projectile Prefab
    public GameObject projectile_Prefab; //Need to make add the components directly in code not in editor
    public int projectile_Amount = 20;   //Create 20 projectile prefabs
    private List<GameObject> projectile_List;   //List of the projectile prefabs

    void Awake()
    {
        instance = this;

        //Preload Projectiles
        projectile_List = new List<GameObject>(projectile_Amount);              //Instantiate a empty list of size projectile amount

        for (int i = 0; i < projectile_Amount; i++)
        {
            GameObject prefabInstance = Instantiate(projectile_Prefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);


            projectile_List.Add(prefabInstance);

        }

    }


    //Get a inactive Projectile from the pooling manager
   public GameObject GetProjectile()
    {

        //Search for a inactive projectile
        foreach(GameObject projectile_Prefab in projectile_List)
        {
            if (!projectile_Prefab.activeInHierarchy)
            {
                projectile_Prefab.SetActive(true);
                return projectile_Prefab;
            }
        }


        //Create a new projectile if non is available
        GameObject prefabInstance = Instantiate(projectile_Prefab);
        prefabInstance.transform.SetParent(transform);
        projectile_List.Add(prefabInstance);
        return prefabInstance;
    }
}
