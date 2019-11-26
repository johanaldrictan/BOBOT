using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float acceleration;
    public Vector2 maxVelocity;
    public float dashCooldown = .5f;
    Vector2 lastMoveDir;

    public float maxDash = 500;
    public float chargeRate = 100;
    public float initialDash = 100;
    float dashSpeed = 0;
    bool charging = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        dashSpeed = initialDash;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        HandleMovement();
        HandleDash();
    }
    private void HandleMovement()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (input.magnitude > 1)
            input.Normalize();
        if(input.magnitude != 0)
            lastMoveDir = input;
        if (Mathf.Abs(rb.velocity.x) < Mathf.Abs(maxVelocity.x) && Mathf.Abs(rb.velocity.y) < Mathf.Abs(maxVelocity.y))
            rb.velocity += input * acceleration;
    }
    private void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("Charging");
            charging = true;
        }
        //not doing time.deltatime here should be fine
        if (charging && dashSpeed < maxDash)
            dashSpeed += (chargeRate);
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //Debug.Log("Firing");
            Debug.Log("Dash Speed: " + dashSpeed);
            rb.AddForce(lastMoveDir * dashSpeed, ForceMode2D.Impulse);
            dashSpeed = initialDash;
            charging = false;
        }
    }
}
