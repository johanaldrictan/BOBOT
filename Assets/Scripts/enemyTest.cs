﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTest : MonoBehaviour
{
<<<<<<< HEAD:Assets/Scripts/enemyTest.cs
    public float hp;
=======
    public GameObject player;
    public GameObject EnemyPrefab;


    [System.NonSerialized]
    public Transform checkPoint;

    public Vector2[] pointsToVisit;

>>>>>>> This one is maybe the problem:Assets/Scripts/EnemyControls/EnemyVision.cs

    // Start is called before the first frame update
    void Start()
    {
        var enemyObject = Instantiate(EnemyPrefab, )
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == player)
        {
            
        }
    }

    private void resetPlayerPositon()
    {
        //player.transform.position = currentCheckPoint();
    }

    public void takeDmg(float dmg)
    {
        hp -= dmg;
        if(hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
