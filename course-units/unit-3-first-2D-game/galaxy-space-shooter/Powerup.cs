using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    //ID for Powerups: 0 = Sheilds, 1 = Speed, 2 = Tripleshot
    [SerializeField] private int _powerupID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -7f)
        {
            Destroy(this.gameObject);
        }
    }
}
