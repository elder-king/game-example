using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFPSMovement : MonoBehaviour
{
    public CharacterController cont;
    public Transform ground_check;
    public float ground_Distens;
    public LayerMask ground_mask;
    public bool is_grounded;

    public float speed = 12f;
    public float jump_hight = 3f;
    Vector3 velocity;

    public float gravity = -9.81f;
    

    // Update is called once per frame
    void Update()
    {
    
        // check's if the plyer is grounded
        is_grounded = Physics.CheckSphere(ground_check.position, ground_Distens, ground_mask);

        //gather input from the player
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //makes the player jump

        if(Input.GetButtonDown("Jump") && is_grounded)
        {
            velocity.y = Mathf.Sqrt(jump_hight * -2f * gravity);
        }
        //resat the velocity to 0
        if(is_grounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        Vector3 move = transform.right * x + transform.forward * z;

        cont.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        cont.Move(velocity * Time.deltaTime);
    }
}
