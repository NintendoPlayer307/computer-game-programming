using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public bool isItem, isHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(isItem)
        {
            //Debug.Log("You collided with your collectible.");
            GameManger.instance.AddToItemCount();
            Destroy(this.gameObject);
        }
    }
}
