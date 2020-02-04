//This script controls the camera movement within the game

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float minHeight = 0.5f, maxHeight = 1.75f, minWidth = -2.75f, maxWidth = 110f;
    public float lastXPos;

    // Start is called before the first frame update
    void Start()
    {
        lastXPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);

        float clampedY = Mathf.Clamp(target.position.y, minHeight, maxHeight);
        transform.position = new Vector3(target.position.x, clampedY, transform.position.z);

        float amountToMoveX = transform.position.x - lastXPos;
        //farBackground.position += new Vector3(amountToMoveX, 0f, 0f);
        //middleBackground.position += new Vector3(amountToMoveX * 0.5f, 0f, 0f);

        lastXPos = transform.position.x;

        if (lastXPos <= minWidth)
        {
            transform.position = new Vector3(minWidth, transform.position.y, transform.position.z);
        }
        
    }
}
