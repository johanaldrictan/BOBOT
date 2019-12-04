using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class EnemyMovement : MonoBehaviour
{
    public MovementType movementType;
    public float enemyVelocity;

    public Vector2 enemyStartingPosition;
    public Vector2 enemyNextPosition;

    public Vector2[] pointsToVisit;

    [SerializeField] private EnemyVision enemyVision;

    private float step;
    private Vector2 currentPosition;
    public float rotationSpeed;
    Coroutine EnemyMoving;
    Coroutine EnemyRotate;


    // Start is called before the first frame update
    void Start()
    {
        step = enemyVelocity * Time.deltaTime;
        transform.position = enemyStartingPosition;
        currentPosition = transform.position;
        StartCoroutine(moveEnemy(movementType));
    }

    // Update is called once per frame
    void Update()
    {
        //enemyVision.ChangeViewDirection();
        //enemyVision.SetOrigin(transform.position);
    }

    public enum MovementType
    {
        Circuit, Snake, Reverse
    }

    IEnumerator moveEnemy(MovementType movementType)
    {
        switch(movementType)
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
                    break;
                }
            case MovementType.Reverse:
                {
                    while (true)
                    {
                        for (int i = pointsToVisit.Length; i >= 0; --i)
                        {
                            EnemyRotate = StartCoroutine(RotateEnemy(i));

                            EnemyMoving = StartCoroutine(Moving(i));
                            yield return EnemyMoving;
                        }

                    }
                    break;
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

                        for (int i = pointsToVisit.Length; i >= 0; --i)
                        {
                            EnemyRotate = StartCoroutine(RotateEnemy(i));

                            EnemyMoving = StartCoroutine(Moving(i));
                            yield return EnemyMoving;
                        }

                    }

                    break;
                }
            default: break;

        }
        
    }

    

    private IEnumerator RotateEnemy(int point)
    {
        float pointX, pointY;

        pointX = pointsToVisit[point].x;
        pointY = pointsToVisit[point].y;

        pointX -= currentPosition.x;
        pointY -= currentPosition.y;

        float angle = (Mathf.Atan2(pointY, pointX) * Mathf.Rad2Deg);
        Quaternion nextPointRotate = Quaternion.AngleAxis(angle, Vector3.forward);

        while (transform.rotation != nextPointRotate)
        {

            transform.rotation = Quaternion.Slerp(transform.rotation, nextPointRotate, Time.deltaTime * rotationSpeed);

            //enemyVision.ChangeViewDirection(transform.rotation);
            yield return null;
        }
        transform.rotation = nextPointRotate;
    }

    private IEnumerator Moving(int point)
    {
        while ((Vector2)transform.position != pointsToVisit[point])
        {
            transform.position = Vector2.MoveTowards(transform.position, 
                pointsToVisit[point], step);
            //enemyVision.SetOrigin(transform.position);
            yield return null;
        }
        currentPosition = pointsToVisit[point];
    }
    
}

