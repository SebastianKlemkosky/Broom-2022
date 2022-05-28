using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie_Enemy_Script : Enemy_Script
{

    


    public float chasing_Interval = 2f;
    public float chasing_Distance = 12f;

    private Player_Script player;
    
    private float chasing_Timer;
    private NavMeshAgent agent;




    // Start is called before the first frame update
    void Start()
    {
        //Find the player by searching for a game object with this name
        player = GameObject.Find("Player").GetComponent<Player_Script>();
        agent = GetComponent<NavMeshAgent>();       // get the NavMeshComponent from the Enemy



    }

    // Update is called once per frame
    void Update()
    {
   

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
