using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Damage : MonoBehaviour
{
    // if we wanted to give the player level health
    // private PlayerHealth _player;

    // Start is called before the first frame update
    void Start()
    {
        // if we wanted to give the player level health
        // _player = GameObject.Find("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            // _player.DealDamage(); // if we wanted to give the player level health

            // Communicate with the Game Manager that the player hit this object
            // take 1 from Player's Life Count
            GameManger.instance.ProcessPlayerDeath();
        }
    }
}
