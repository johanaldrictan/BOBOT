using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TestPlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float acceleration;
    public Vector2 maxVelocity;
    public float dashCooldown = .5f;
    public float rotationSpeed;
    Vector2 lastMoveDir;

    public float maxDash = 500;
    public float chargeRate = 100;
    public float initialDash = 100;
    public float dashSpeed = 0;
    bool charging = false;
    public Trail trail;
    public float trailDuration;

    //justin's vars
    public float dmg;
    public bool dashUP = false;
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
        if (Mathf.Abs(rb.velocity.x) > Mathf.Abs(maxVelocity.x * 1.2f) ||
            Mathf.Abs(rb.velocity.y) > Mathf.Abs(maxVelocity.y * 1.2f))
        {
            dashUP = true;
        }
        else
        {
            dashUP = false;
        }
    }
    private void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
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
    private void HandleRotation()
    {
        Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = new Vector2(mp.x - this.transform.position.x, mp.y - this.transform.position.y);
        this.transform.up = dir;
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
            StartCoroutine(MakeTrail());
            //Debug.Log("Firing");
            Debug.Log("Dash Speed: " + dashSpeed);
            rb.AddForce(transform.up * dashSpeed, ForceMode2D.Impulse);
            dashSpeed = initialDash;
            charging = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        if(dashUP == true)
        {
            Debug.Log("enemy hit");
            if(collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<enemyTest>().takeDmg(dmg);
            }
            
        }
            

    }
        IEnumerator MakeTrail()
    {
        Debug.Log("Making Trail");
        trail.makeTrail = true;
        yield return new WaitForSeconds(trailDuration);
        trail.makeTrail = false;
    }
}
