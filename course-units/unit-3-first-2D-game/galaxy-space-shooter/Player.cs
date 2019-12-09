using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    private float _speedModifier = 2f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    private Vector3 _laserOffset = new Vector3(0, 1.05f, 0);
    private float _fireRate = 0.5f;
    private float _canFire = -1.0f;
    [SerializeField] private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField] private bool _isTripleShotActive = false;
    [SerializeField] private bool _isSpeedBoostActive = false;
    [SerializeField] private bool _isShieldsActive = false;
    [SerializeField] private GameObject _shieldVisualizer;
    [SerializeField] private GameObject _leftEngine;
    [SerializeField] private GameObject _rightEngine;
    [SerializeField] private int _score;
    private UIManager _uiManager;
    [SerializeField] private AudioClip _laserAudio;
    private AudioSource _playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        //take the current position = new position (0, 0, 0)
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _playerAudio = GetComponent<AudioSource>();

        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is null.");
        }

        if(_uiManager == null)
        {
            Debug.LogError("The UI Manager is null.");
        }

        if(_playerAudio == null)
        {
            Debug.LogError("Player Audio is NULL");
        }

        _playerAudio.clip = _laserAudio;
        
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
        //local variables only accessible in the Update()
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        /*
        Vector3.right == new Vector3(1,0,0);
        transform.Translate(Vector3.right * horizontalInput *_speed * Time.deltaTime);
        Vector3.up == new Vector3(0,1,0);
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        below is a short hand of the 2 above lines of code
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);
        */

        //optimized code for above content
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);
        
        /*
        //keeps the player bound to a set area on the Y axis
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }
        */
        //optimized code from above content
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y,-3.5f, 0), 0);

        //keep the player bound to a set area on the X axis
        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if(_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + _laserOffset, Quaternion.identity);
        }

        _playerAudio.Play();
        
    }

    public void Damage()
    {
        //if shields is active
        //do nothing...
        //deactivate shields
        //return;
        if(_isShieldsActive == true)
        {
            _isShieldsActive = false;
            //disable the visualizer
            _shieldVisualizer.SetActive(false);
            return;
        }

        _lives--;

        _uiManager.UpdateLives(_lives);

        //turn on damage
        if(_lives == 2)
        {
            _leftEngine.SetActive(true);
        }
        else if(_lives == 1)
        {
            _rightEngine.SetActive(true);
        }

        //check if dead
        //destroy us
        if(_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        //tripleShotAcitve becomes true
        //start the power down coroutine for triple shot
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    //IEnumerator TripleShotPowerDownRountine
    //wait 5 seconds
    //set the triple shot to false
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        //speedBoostActive becomes true
        //start the speed cool down coroutine for speed boost
        _isSpeedBoostActive = true;
        _speed *= _speedModifier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isSpeedBoostActive = false;
        _speed /= _speedModifier;
    }

    public void ShieldsActive()
    {
        _isShieldsActive = true;
        //enable visualize
        _shieldVisualizer.SetActive(true);
    }
    
    //method to add points to the score
    //communicate with the UI to update the score
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

}
