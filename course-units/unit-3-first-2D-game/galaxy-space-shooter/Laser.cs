using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //create speed variable
    [SerializeField] private float _speed = 8f;


    // Update is called once per frame
    void Update()
    {
        //move laser on its own in the upward direction
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        //if laser is outside game canvas destroy object
        if(transform.position.y > 7.5f)
        {
            Destroy(this.gameObject);
        }
    }
}
