using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private float _canFire = -1.0f;
    [SerializeField] private int _lives = 3;
    [SerializeField] private Vector3 _laserOffset = new Vector3(0, 1.0f, 0);
    [SerializeField] private bool _isShieldPowerupActive = false;
    [SerializeField] private bool _isSpeedPowerupActive = false;
    [SerializeField] private bool _isTripleshotActive = false;

    private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        
        //keep the player bound to a set area on the Y axis
        if(transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if(transform.position.y <= -3.5f)
        {
            transform.position = new Vector3(transform.position.x, -3.5f, 0);
        }

        //keep the player bound to a set area on the X axis
        if (transform.position.x >= 9f)
        {
            transform.position = new Vector3(9f, transform.position.y, 0);
        }
        else if (transform.position.x <= -9f)
        {
            transform.position = new Vector3(-9f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + _laserOffset, Quaternion.identity); 
    }

    public void Damage()
    {
        //if player is hit by enemy -1 from lives
        _lives--; //_lives = _lives - 1;

        //if player has 0 lives, destroy the player
        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
}
