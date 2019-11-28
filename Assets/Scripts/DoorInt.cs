using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInt : MonoBehaviour
{

    private SpriteRenderer s;
    private Rigidbody2D rb;
    // Start is called before the first frame update

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        s = GetComponent<SpriteRenderer>();
        Debug.Log("door ACTIVE");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void interact()
    {
        //play noise too?
        Debug.Log("door INACTIVE");
        rb.isKinematic = true;
        s.enabled = false;
    }
}
