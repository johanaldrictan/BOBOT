using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTest : MonoBehaviour
{
    public float hp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
