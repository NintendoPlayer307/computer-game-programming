using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolGround : MonoBehaviour
{
    public float speed = 2.5f;                      //how fast the object move
    public Transform leftPoint, rightPoint;         //end points for object to move to
    public bool isMovingRight = false;              //what direction is the object moving in
    public float moveTime, waitTime;                //
    private float moveCount, waitCount;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;                     //only need the Sprit Renderer if you are flipping your object
    private Animator _anim;                         //only need the Animator if you have animations


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();

        leftPoint.parent = null;
        rightPoint.parent = null;

        isMovingRight = true;

        moveCount = moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveCount > 0)
        {
            moveCount -= Time.deltaTime;

            if(isMovingRight)
            {
                _rb.velocity = new Vector2(speed, _rb.velocity.y);

                _sr.flipX = true; //only if you need it

                if(transform.position.x > rightPoint.position.x)
                {
                    isMovingRight = false;
                }
            }
            else
            {
                _rb.velocity = new Vector2(-speed, _rb.velocity.y);

                _sr.flipX = false;

                if(transform.position.x < leftPoint.position.x)
                {
                    isMovingRight = true;
                }
            }

            if(moveCount <= 0)
            {
                waitCount = Random.Range(waitTime * 0.75f, waitTime * 1.25f);
            }

            _anim.SetBool("isMoving", true);
        }
        else if (waitCount > 0)
        {
            waitCount -= Time.deltaTime;
            _rb.velocity = new Vector2(0f, _rb.velocity.y);

            if(waitCount <= 0)
            {
                moveCount = Random.Range(moveTime * 0.75f, moveTime * 1.25f);
            }

            _anim.SetBool("isMoving", false); // for animation only
        }
    }

}
