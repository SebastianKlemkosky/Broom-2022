using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private Player_Script player;

    //Add Headers here later
    //Need to somehow links these later
    public Text ammo_Text;

    public Text player_Health_Text;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInChildren<Player_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        //Need to change how we update this
        //Only update when the player shoots, or compare values between ammo in player vs ammo in manager
        ammo_Text.text = "Ammo: " + player.Ammo;

        player_Health_Text.text = "Health: " + player.Player_Health;

    }
}
