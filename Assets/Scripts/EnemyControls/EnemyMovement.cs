using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class EnemyMovement : MonoBehaviour
{
    public float enemyVelocity;

    public Vector2 enemyStartingPosition;
    public Vector2 enemyNextPosition;

    public Vector2[] pointsToVisit;

    private float step;
    private Vector2 currentPosition;

    Coroutine EnemyMoving;
    Coroutine EnemyRotate;


    // Start is called before the first frame update
    void Start()
    {
        step = enemyVelocity * Time.deltaTime;
        transform.position = enemyStartingPosition;
        currentPosition = transform.position;
        StartCoroutine(moveEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator moveEnemy()
    {
        for(int i = 0; i< pointsToVisit.Length; ++i)
        {
            EnemyRotate = StartCoroutine(RotateEnemy(i));

            EnemyMoving = StartCoroutine(Moving(i));
            yield return EnemyMoving;
        }
    }

    private IEnumerator RotateEnemy(int point)
    {
        float pointX, pointY;

        pointX = pointsToVisit[point].x;
        pointY = pointsToVisit[point].y;

        pointX -= currentPosition.x;
        pointY -= currentPosition.y;

        float angle = Mathf.Atan2(pointY, pointX) * Mathf.Rad2Deg;
        Quaternion nextPointRotate = Quaternion.Euler(0, 0, angle);

        while (transform.rotation != nextPointRotate)
        {
            transform.rotation = nextPointRotate;
            yield return null;
        }
    }

    private IEnumerator Moving(int point)
    {
        while ((Vector2)transform.position != pointsToVisit[point])
        {
            transform.position = Vector2.MoveTowards(transform.position, 
                pointsToVisit[point], step);
            yield return null;
        }
        currentPosition = pointsToVisit[point];
    }
}

