using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    public float speed;
    public Transform upPoint, downPoint;
    public bool isMovingDown = false;
    public float moveTime, waitTime;
    private float moveCount, waitCount;

    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        upPoint.parent = null;
        downPoint.parent = null;

        isMovingDown = true;

        moveCount = moveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCount > 0)
        {
            moveCount -= Time.deltaTime;

            if (isMovingDown)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, -speed);

                if (transform.position.y < downPoint.position.y)
                {
                    isMovingDown = false;
                }
            }
            else
            {
                _rb.velocity = new Vector2(_rb.velocity.x, speed);

                if (transform.position.y > upPoint.position.y)
                {
                    isMovingDown = true;
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
            _rb.velocity = new Vector2(_rb.velocity.x, 0f);

            if (waitCount <= 0)
            {
                moveCount = Random.Range(moveTime * 0.75f, moveTime * 1.25f);
            }

        }
    }
}
