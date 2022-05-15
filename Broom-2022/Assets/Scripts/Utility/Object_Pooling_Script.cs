using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//This Script handles object pooling for the game
public class Object_Pooling_Script : MonoBehaviour
{

    private static Object_Pooling_Script instance;
    public static Object_Pooling_Script Instance { get { return instance; } }


    
    public int intial_projectile_Amount = 8;   //every projectile prefab intial amount

    private List<GameObject> projectile_List;     //List of each projectile in the projectile folder in prefabs

    private List<List<GameObject>> projectile_List_Of_Lists = new List<List<GameObject>>();

    void Awake()
    {
        instance = this;

        
        //Load All The projectiles in the projectile Lists from the Resources/Projectiles Folder
        projectile_List = new List<GameObject>(Resources.LoadAll<GameObject>("Projectiles"));

        //Iterate through each projectile
        for (int j = 0; j < projectile_List.Count; j++)
        {
            //Added a list for each projectile in projectile list
            projectile_List_Of_Lists.Add(new List<GameObject>());
            

            for (int i = 0; i < intial_projectile_Amount; i++)
            {
                GameObject prefabInstance = Instantiate(projectile_List[j]);
                prefabInstance.transform.SetParent(transform);
                prefabInstance.SetActive(false);

                //Added the projectiles into the list of list
                projectile_List_Of_Lists[j].Add(prefabInstance);
                
            }
        }


        
        


        
        
    }


    //Get a inactive Projectile from the pooling manager
    //Defaults with first projectile prefab list
   public GameObject GetProjectile(bool shot_By_Player)
    {

        

        //Search for the specific projectile in the list of list
        //Then Search for inactive projectile within this list




        //Search for a inactive projectile
        foreach(GameObject projectile_Prefab in projectile_List_Of_Lists[0])
        {
            if (!projectile_Prefab.activeInHierarchy)
            {
                projectile_Prefab.SetActive(true);
                projectile_Prefab.GetComponent<Projectile_Bullet_Script>().Shot_By_Player = shot_By_Player; //The projectile was shot by the player
                return projectile_Prefab;
            }
        }


        //Create a new projectile if non is available
        GameObject prefabInstance = Instantiate(projectile_List[0]);
        prefabInstance.transform.SetParent(transform);
        projectile_List_Of_Lists[0].Add(prefabInstance);

        


        prefabInstance.GetComponent<Projectile_Bullet_Script>().Shot_By_Player = shot_By_Player; //The projectile was shot by the player
        return prefabInstance;
    }


    public GameObject GetProjectile(bool shot_By_Player, GameObject projectile_prefab)
    {

        int index = 0;
        

        //Search for the specific projectile in the list of list

        for (int i = 0; i < projectile_List.Count; i++)
        {
            if (projectile_prefab.name == projectile_List[i].name)
            {
                index = i;
            }
       
        }


        //Then Search for inactive projectile within this list




        //Search for a inactive projectile
        foreach (GameObject projectile_Prefab in projectile_List_Of_Lists[index])
        {
            if (!projectile_Prefab.activeInHierarchy)
            {
                projectile_Prefab.SetActive(true);
                projectile_Prefab.GetComponent<Projectile_Bullet_Script>().Shot_By_Player = shot_By_Player; //The projectile was shot by the player
                return projectile_Prefab;
            }
        }


        //Create a new projectile if non is available
        GameObject prefabInstance = Instantiate(projectile_List[index]);
        prefabInstance.transform.SetParent(transform);
        projectile_List_Of_Lists[index].Add(prefabInstance);




        prefabInstance.GetComponent<Projectile_Bullet_Script>().Shot_By_Player = shot_By_Player; //The projectile was shot by the player
        return prefabInstance;
    }

}
