using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchInt : MonoBehaviour
{
    public DoorInt[] linkOn;
    public DoorInt[] linkOff;
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

            for (int i = 0; i < linkOn.Length; i++)
            {
                linkOn[i].interactOn();
            }


            for (int i = 0; i < linkOff.Length; i++)
            {
                linkOff[i].interactOff();
            }
            
        }
    }


}
