using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float enemyVelocity;
    public float bounce;

    private float xDir;
    private float yDir;
    private float directionAngle;
    private Vector2 movementDirection;
    private Vector2 movementSpeed;


    // Start is called before the first frame update
    void Start()
    {
        changeMovementDirection();
    }

    void changeMovementDirection()
    {
        xDir = Random.Range(-1.0f, 1.0f);
        yDir = Random.Range(-1.0f, 1.0f);
        movementDirection = new Vector2(xDir, yDir).normalized;
        //directionAngle = Vector2.Angle(xDir, yDir);
        transform.Rotate(movementDirection);
        movementSpeed = movementDirection * enemyVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = new Vector2(transform.position.x + (movementSpeed.x * Time.deltaTime), 
            transform.position.y + (Time.deltaTime * movementSpeed.y));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.name);
        transform.position -= bounce * (Vector3)movementDirection;
        changeMovementDirection();
        
    }
}
