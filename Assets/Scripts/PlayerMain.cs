using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    // Start is called before the first frame update
   public float moveSpeed = 5f; // Movement speed
   private bool canMove=false;

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        // Get horizontal input (left and right arrows or A/D keys)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Define 2D movement (only affecting x-axis)
        Vector2 movement = new Vector2(horizontalInput, 0); 

        // Apply movement to the object
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
    

    
}
