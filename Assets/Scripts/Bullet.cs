using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Serialized Public variables can be used to change the values of the variable in the Unity editor.
    public float bulletVelocity = 500.0f;
    public float maxLifetime = 10.0f;
    
    //This just makes it so that you can reference the Rigidbody2D.
    private Rigidbody2D rb;

    //Getting the Rigidbody Component from the GameObject that the bullet script is attached to on game awake. Required to call any rigidbody functions such as AddForce.
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //Custom function that uses the direction to apply a force to the rigidbody, function is made public and called within the PlayerController.cs.
    public void Project(Vector2 direction)
    {
        //Calls a rigidbody function to add force to the bullet and multiplies it by the bulletVelocity variable.
        rb.AddForce(direction * this.bulletVelocity);

        //Destroys the game object after a set time.
        Destroy(this.gameObject, this.maxLifetime);
    }

    //This function is called whenever the game object collides with another collider.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Simply destroys the game object.
        Destroy(this.gameObject);
    }
}
