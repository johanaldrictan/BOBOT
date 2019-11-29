using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashBar : MonoBehaviour
{
    Vector3 localScale;
    Vector3 maxScale;
    public PlayerController player;
    public float dashSpeed;
    public float maxSpeed;
    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
        maxScale = transform.localScale;
        
        maxSpeed = player.maxDash;
    }

    // Update is called once per frame
    void Update()
    {
        dashSpeed = player.dashSpeed;
        Debug.Log((dashSpeed - 100));
        Debug.Log((maxSpeed));
        Debug.Log(((dashSpeed - 100) / maxSpeed));
        localScale.x = .5f * ((dashSpeed - 100) / maxSpeed);
        transform.localScale = localScale;



    }
}
