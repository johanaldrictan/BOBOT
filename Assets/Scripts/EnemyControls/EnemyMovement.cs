using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemyVision enemyVision;
    public MovementType movementType;
    public float enemyVelocity;
    public float rotationDegreesPerSecond;

    public Vector2[] pointsToVisit;

    private PlayerController player;

    private float step;
    private bool canMove = true;
    private float killSpeed = 5f;

    Coroutine EnemyMoving;
    Coroutine EnemyRotate;

    void Start()
    {
        step = enemyVelocity * Time.deltaTime;
        StartCoroutine(moveEnemy(movementType));
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log();
    }

    public enum MovementType
    {
        Circuit, Snake, Reverse, Rotate
    }

    IEnumerator moveEnemy(MovementType movementType)
    {
        switch (movementType)
        {
            case MovementType.Circuit:
                {
                    while (true)
                    {
                        for (int i = 0; i < pointsToVisit.Length; ++i)
                        {
                            EnemyRotate = StartCoroutine(RotateEnemy(i));

                            EnemyMoving = StartCoroutine(Moving(i));
                            yield return EnemyMoving;
                        }
                    }

                }
            case MovementType.Reverse:
                {
                    while (true)
                    {
                        for (int i = pointsToVisit.Length - 1; i >= 0; --i)
                        {
                            EnemyRotate = StartCoroutine(RotateEnemy(i));

                            EnemyMoving = StartCoroutine(Moving(i));
                            yield return EnemyMoving;
                        }
                    }

                }
            case MovementType.Snake:
                {
                    while (true)
                    {

                        for (int i = 0; i < pointsToVisit.Length; ++i)
                        {
                            EnemyRotate = StartCoroutine(RotateEnemy(i));

                            EnemyMoving = StartCoroutine(Moving(i));
                            yield return EnemyMoving;
                        }

                        for (int i = pointsToVisit.Length - 1; i >= 0; --i)
                        {
                            EnemyRotate = StartCoroutine(RotateEnemy(i));

                            EnemyMoving = StartCoroutine(Moving(i));
                            yield return EnemyMoving;
                        }
                    }

                }
            case MovementType.Rotate:
                {
                    while (true)
                    {
                        for (int i = 0; i < pointsToVisit.Length; ++i)
                        {
                            EnemyRotate = StartCoroutine(RotateEnemy(i));


                            yield return EnemyMoving;
                        }
                    }
                }
            default: break;
        }

    }

    private IEnumerator RotateEnemy(int point)
    {
        float pointX, pointY;

        pointX = pointsToVisit[point].x;
        pointY = pointsToVisit[point].y;

        pointX -= transform.position.x;
        pointY -= transform.position.y;

        float angle = (Mathf.Atan2(pointY, pointX) * Mathf.Rad2Deg);
        float originalAngle = transform.rotation.eulerAngles.z;
        var a = Mathf.Abs(originalAngle - angle);
        if (a > 180)
        {
            a -= 180;
        }

        float timeToRotateInSeconds = a / rotationDegreesPerSecond;
        Quaternion nextPointRotate = Quaternion.Euler(0, 0, angle);
        Quaternion startRotation = transform.rotation;
        float timePassedSoFar = 0;
        //to readd speed, figure out how far to rotate in angles, and use that to calc total time needed here
        //slerp clamps t to 0-1 range, so when this goes above that range it will end
        canMove = false;
        while (transform.rotation != nextPointRotate)
        {
            transform.rotation =
                Quaternion.Slerp(
                    startRotation,
                    nextPointRotate,
                    timePassedSoFar / timeToRotateInSeconds);

            timePassedSoFar += Time.deltaTime;
            enemyVision.setAngle(transform.rotation.eulerAngles.z);
            yield return null;
        }
        canMove = true;
        transform.rotation = nextPointRotate;
    }

    private IEnumerator Moving(int point)
    {
        while ((Vector2)transform.position != pointsToVisit[point])
        {
            enemyVision.SetOrigin(transform.position);
            //Debug.Log(transform.position + "Movement");
            Debug.Log(canMove);
            if (canMove)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                    pointsToVisit[point], step);
            }

            yield return null;
        }
        transform.position = pointsToVisit[point];
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"));
        {
            Debug.Log(collision.rigidbody.velocity.magnitude);
            if (collision.rigidbody && collision.rigidbody.velocity.magnitude > killSpeed)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }


}

