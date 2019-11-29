using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHP : MonoBehaviour
{
    Vector3 localScale;
    Vector3 maxScale;
    public enemyTest enemy;
    public float maxHP;
    public float prevHP;
    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
        maxScale = transform.localScale;
        maxHP = enemy.hp;
        prevHP = enemy.hp;
    }

    // Update is called once per frame
    void Update()
    {
        if(prevHP != enemy.hp)
        {
            Debug.Log((enemy.hp / maxHP));
            localScale.x = maxScale.x * (enemy.hp / maxHP);
            transform.localScale = localScale;
            prevHP = enemy.hp;

        }

    }
}
