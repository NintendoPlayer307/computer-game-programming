using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _playerLives = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamage()
    {
        if(_playerLives > 0)
        {
            _playerLives--; // _playerLives = _playerLives - 1;
        }
        else
        {
            Debug.Log("Player lives are at 0, Game is Over!");
        }
    }
}
