using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchInt : MonoBehaviour
{
    public DoorInt linkOn;
    public DoorInt linkOff;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("switch ACTIVE");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //play noise?
            Debug.Log("switch ON!");
            linkOn.interactOn();
            Debug.Log("switch OFF!");
            linkOff.interactOff();
        }
    }


}
