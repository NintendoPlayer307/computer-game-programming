using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAir : MonoBehaviour
{
    public float speed = 2.5f;
    public Transform leftPoint, rightPoint;
    public bool isMovingRight = false;
    public float moveTime, waitTime;
    private float moveCount, waitCount;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Animator _anim;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        //_anim = GetComponent<Animator>();

        leftPoint.parent = null;
        rightPoint.parent = null;

        isMovingRight = true;

        moveCount = moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCount > 0)
        {
            moveCount -= Time.deltaTime;

            if (isMovingRight)
            {
                _rb.velocity = new Vector2(speed, _rb.velocity.y);

                _sr.flipX = true;

                if (transform.position.x > rightPoint.position.x)
                {
                    isMovingRight = false;
                }
            }
            else
            {
                _rb.velocity = new Vector2(-speed, _rb.velocity.y);

                _sr.flipX = false;

                if (transform.position.x < leftPoint.position.x)
                {
                    isMovingRight = true;
                }
            }

            if (moveCount <= 0)
            {
                waitCount = Random.Range(waitTime * 0.75f, waitTime * 1.25f);
            }

        }
        else if (waitCount > 0)
        {
            waitCount -= Time.deltaTime;
            _rb.velocity = new Vector2(0f, _rb.velocity.y);

            if (waitCount <= 0)
            {
                moveCount = Random.Range(moveTime * 0.75f, moveTime * 1.25f);
            }

        }
    }
}
