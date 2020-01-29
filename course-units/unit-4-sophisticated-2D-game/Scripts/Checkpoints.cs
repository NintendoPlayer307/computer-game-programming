using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    [Header("Status Flags")]
    public bool hasBeenChecked = false;

    private Animator _anim;
    private float waitTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hasBeenChecked == true)
        {
            //set the checkpoint on flag in the animator
            _anim.SetBool("hasBeenChecked", hasBeenChecked);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !hasBeenChecked)
        {
            hasBeenChecked = true;
            GameManger.instance.SetSpawnPoint(transform.position);
            
        }
    }
}
