using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    private Player _player;
    private Animator _enemyAnim;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(0, 8, 0);
        _player = GameObject.Find("Player").GetComponent<Player>();
        _enemyAnim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        
        if (_player == null)
        {
            Debug.LogError("Player is NULL.");
        }

        if(_enemyAnim == null)
        {
            Debug.LogError("Enemy Animation is NULL.");
        }

        if(_audioSource == null)
        {
            Debug.LogError("Explosion Audio is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        //move down at 4 meters per second
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
        //if bottom of screen
        //respawn at top with new random
        if(transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 8, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if other is Player, destroy us, damage Player
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }

            _speed = 0;
            _enemyAnim.SetTrigger("OnEnemyDeath");
            _audioSource.Play();
            Destroy(this.gameObject, 1f);
        }
        //if other is laser, destroy both
        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if(_player != null)
            {
                _player.AddScore(10);
            }
            
            _speed = 0;
            _enemyAnim.SetTrigger("OnEnemyDeath");
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 1f);
        }

    }
}
