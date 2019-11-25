using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float acceleration;
    public float dashSpeed;
    public Vector2 maxVelocity;
    public float dashCooldown = .5f;
    Vector2 lastMoveDir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        lastMoveDir = input;
        if (Mathf.Abs(rb.velocity.x) < Mathf.Abs(maxVelocity.x) && Mathf.Abs(rb.velocity.y) < Mathf.Abs(maxVelocity.y))
            rb.velocity += input * acceleration;
    }
    private void HandleDash()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(lastMoveDir * dashSpeed);
        }
    }
}
