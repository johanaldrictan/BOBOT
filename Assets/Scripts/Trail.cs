using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    public float trailDelay;
    private float trailDelaySeconds;
    public GameObject trail;
    public bool makeTrail;
    public float trailDecay;
    // Start is called before the first frame update
    void Start()
    {
        trailDelaySeconds = trailDelay;
        makeTrail = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (makeTrail)
        {
            if (trailDelaySeconds > 0)
            {
                trailDelaySeconds -= Time.deltaTime;
            }
            else
            {
                //create trail
                GameObject currentTrail = Instantiate(trail, transform.position, transform.rotation);
                trailDelaySeconds = trailDelay;
                Destroy(currentTrail, trailDecay);
            }
        }
    }
}
