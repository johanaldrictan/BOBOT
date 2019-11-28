using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInt : MonoBehaviour
{

    private SpriteRenderer s;
    private Collider2D c;
    // Start is called before the first frame update

    private void Awake()
    {
        c = GetComponent<Collider2D>();
        s = GetComponent<SpriteRenderer>();
        Debug.Log("door ACTIVE");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void interactOn()
    {
        //play noise too?
        Debug.Log("door INACTIVE");
        c.enabled = false;
        s.enabled = false;
    }
    public void interactOff()
    {
        //play noise too?
        Debug.Log("door ACTIVE");
        c.enabled = true;
        s.enabled = true;
    }
}
