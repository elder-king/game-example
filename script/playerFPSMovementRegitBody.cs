using UnityEngine;

public class playerFPSMovementRegitBody : MonoBehaviour
{
    float move_F;
    float move_S;
    float move_u;

    public bool is_grounded;
    public Transform ground_check;
    public float ground_Distens;
    public LayerMask ground_mask;
    public bool gravity;
    public float gravity_force = 20f;
    public float jump_Hight = 5f;

    public float walking_speed;
    public float sprint_speed;
    float current_speed;
    
    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        is_grounded = Physics.CheckSphere(ground_check.position, ground_Distens, ground_mask);

        if (is_grounded && move_u != 0)
        {
            rb.AddForce(transform.up * move_u, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (current_speed != sprint_speed)

                current_speed = sprint_speed;

        }
        else
        {
            current_speed = walking_speed;
        }
        move_F = Input.GetAxis("Vertical") * current_speed;
        move_S = Input.GetAxis("Horizontal") * current_speed;
        move_u = Input.GetAxis("Jump") * jump_Hight;
    }
    private void FixedUpdate()
    {
        rb.velocity = (transform.forward * move_F) + (transform.right * move_S) + (transform.up * rb.velocity.y);

        //if some color condition:
        if (gravity == true)
        {
            rb.AddForce(0, gravity_force * -2, 0.0f);
        }
        //makes the player jump's!
        if (is_grounded && move_u != 0)
        {
            rb.AddForce(transform.up * move_u, ForceMode.VelocityChange);
        }
    }
}
