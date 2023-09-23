using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Serialized Public variables can be used to change the values of the variable in the Unity editor.
    public float thrustSpeed = 1.0f;
    public float turnSpeed = 1.0f;
    public Bullet bulletPrefab;
    
    private Rigidbody2D rb;
    private bool thrusting;
    private float turnDirection;

    //Getting the Rigidbody Component from the GameObject that the script is attached to on game awake. Required to call any rigidbody functions such as AddForce.
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Determines whether or not the player is pushing the key we want to associate with thrusting or going forward with a simple boolean.
        thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        //Detects whether the player is pushing one of keys associated with turning left and sets the value of the turnDirection.
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) 
            turnDirection = 1.0f;
        //Detects whether the player is pushing one of keys associated with turning right and sets the value of the turnDirection.
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) 
            turnDirection = -1.0f;
        //Sets the variable's value to zero when not pushing either of the turn keys.
        else 
            turnDirection = 0.0f;
 
        //Detects if the player pushes the space key and executes the function each time the key is pushed down.
        if (Input.GetKeyDown(KeyCode.Space))
            Shoot();
    }

    //Similar function to Update but is best used for physics based calculations because it is called after physics tics.
    private void FixedUpdate()
    {
        //Adds a force to game object and multiplies the force by a speed variable when the boolean "thrusting" is true.
        if (thrusting)
            rb.AddForce(this.transform.up * this.thrustSpeed);
        //Waits until the turnDirection variable doesn't equal zero and adds torque based off of that value and multiples it by the turnSpeed.
        if (turnDirection != 0.0f)
            rb.AddTorque(turnDirection * this.turnSpeed);
    }
    
    //Custom function that is called when the player presses the space key, referenced above.
    private void Shoot()
    {
        //Instantiates (creates) a prefab, set in the editor, and passes in the same position and rotation values of the object this script is attached to and assigns it to "bullet".
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        //Using a function from Bullet.cs, projects the prefab forward.
        bullet.Project(this.transform.up);
    }
}
