using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Vehicle : MonoBehaviour
{
    // Movement fields
    public float speed = 1f;
    public Vector2 direction = Vector2.right;
    public Vector2 velocity = Vector2.zero;
    public GameObject vehicle;
    private Vector2 movementInput;

    void Update()
    {
        direction = movementInput;

        // Update our velocity
        // Veclocity is our direction * speed
        velocity = direction * speed * Time.deltaTime;

        //Add our velocity to our position
        transform.position += (Vector3)velocity;

        if (direction!= Vector2.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }

        //Wrap around screen
        if (transform.position.x > 8.25)
        {
            transform.position = new Vector3(-8.25f,(transform.position.y),0);
        }
        else if (transform.position.x < -8.25)
        {
            transform.position = new Vector3(8.25f,(transform.position.y), 0);
        }
        else if (transform.position.y > 5.5)
        {
            transform.position = new Vector3(transform.position.x,-5.5f, 0);
        }
        else if (transform.position.y < -5.5)
        {
            transform.position = new Vector3(transform.position.x, 5.5f, 0);
        }
    }

    public void OnMove(InputAction.CallbackContext moveContext)
    {
        movementInput = moveContext.ReadValue<Vector2>();
    }


}
