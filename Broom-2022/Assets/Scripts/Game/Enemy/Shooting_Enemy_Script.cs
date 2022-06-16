using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shooting_Enemy_Script : Enemy_Script
{

    public GameObject shooting_Projectile;

    public float shooting_Interval = 4f;
    public float shooting_Distance = 3f;

    public float chasing_Interval = 2f;
    public float chasing_Distance = 12f;

    private Player_Script player;
    private float shooting_Timer;
    private float chasing_Timer;
    private NavMeshAgent agent;




    // Start is called before the first frame update
    void Start()
    {
        //Find the player by searching for a game object with this name
        player = GameObject.Find("Player").GetComponent<Player_Script>();
        agent = GetComponent<NavMeshAgent>();       // get the NavMeshComponent from the Enemy
        shooting_Timer = Random.Range(0, shooting_Interval);

       

    }

    // Update is called once per frame
    void Update()
    {
        //Shooting Logic
        shooting_Timer -= Time.deltaTime;
        //If the shooting timer and the enemy is close enough to shoot
        if (shooting_Timer <= 0 && Vector3.Distance(transform.position, player.transform.position) <= shooting_Distance)
        {
            shooting_Timer = shooting_Interval;

            //Get the projectile from the pooling manager
            //Then get the direction for it to be fired
            GameObject projectile = Object_Pooling_Script.Instance.GetProjectile(false, shooting_Projectile);
            projectile.transform.position = transform.position;
            projectile.transform.forward = (player.transform.position - transform.position).normalized;
            
            
            

        }

        //Chasing Logic
        chasing_Timer -= Time.deltaTime;
        if (chasing_Timer <= 0 && Vector3.Distance(transform.position, player.transform.position) <= chasing_Distance)
        {
            
            chasing_Timer = chasing_Interval;
            agent.SetDestination(player.transform.position); //Move to the player

        }
    }

    protected override void OnKill()
    {
        base.OnKill();
        agent.enabled = false;
        this.enabled = false;

        transform.localEulerAngles = new Vector3(10, transform.localEulerAngles.y, transform.localEulerAngles.z);

    }
}
