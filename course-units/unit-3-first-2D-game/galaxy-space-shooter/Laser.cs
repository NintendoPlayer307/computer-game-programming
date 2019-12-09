using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;
    private bool _isEnemyLaser = false;

    // Update is called once per frame
    void Update()
    {
        if(_isEnemyLaser == false)
        {
            moveUp();
        }
        else
        {
            moveDown();
        }
    }

    void moveUp()
    {
        //translate laser up
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        //if laser position is greater than 8 on the y
        //destroy the object
        if (transform.position.y > 8f)
        {
            //check if this object has a parent.
            //destroy the parent too.
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    void moveDown()
    {
        //translate laser up
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //if laser position is greater than 8 on the y
        //destroy the object
        if (transform.position.y < -8f)
        {
            //check if this object has a parent.
            //destroy the parent too.
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void AssignEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && _isEnemyLaser == true)
        {
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }
        }
    }
}
