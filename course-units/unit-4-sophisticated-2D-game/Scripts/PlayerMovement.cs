//This script controls the player's movement and physics within the game

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Properties")]
    public float speed = 5f;                    //Player Speed

    [Header("Jump Properties")]
    public float jumpForce = 10f;               //Initial force of jump
    public bool canDoubleJump;                  //If player can have a double jump      

    [Header("Environment Check Properties")]
    public float groundDistance = 0.2f;         //Distance player is considered to be on the ground
    public LayerMask groundLayer;               //Layer of the ground
    public Transform groundCheckPoint;          //Object to detect if player is on ground

    [Header("Status Flags")]
    public bool isOnGround;                     //Is player on the ground?
    public bool isJumping;                      //Is player jumping?

    private Rigidbody2D _rb;                    //The rigidbody component
    private SpriteRenderer _sr;                 //The sprite renderer component
    private Animator _anim;                     //The animator component

    // Start is called before the first frame update
    void Start()
    {
        //Get a reference to the required components
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();

        //Set players starting point
        transform.position = GameManger.instance.spawnPoint;
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, _rb.velocity.y);

        isOnGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundDistance, groundLayer);

        if (isOnGround)
        {
            canDoubleJump = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isOnGround == true)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            }
            else
            {
                if (canDoubleJump == true)
                {
                    _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
                    canDoubleJump = false;
                }
            }
        }

        if (_rb.velocity.x < 0)
        {
            _sr.flipX = true;
        }
        if (_rb.velocity.x > 0)
        {
            _sr.flipX = false;
        }

        _anim.SetFloat("moveSpeed", Mathf.Abs(_rb.velocity.x));
        _anim.SetBool("isOnGround", isOnGround);
    }
}
