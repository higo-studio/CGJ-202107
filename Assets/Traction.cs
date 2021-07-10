using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traction : MonoBehaviour
{
    public CubeController controller;
    public float XrotateScale = 0f;
    public bool touchTrack = false;

    public float trackingAcceleration = 25;
    Transform track;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Track")
        {
            touchTrack = true;
            controller.train.acceleration += trackingAcceleration;
            track = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Track")
        {
            Debug.Log("LEAVING !!!!!!!!!!");
            controller.train.acceleration -= trackingAcceleration;
            touchTrack = false;
        }
    }

    private void FixedUpdate()
    {
        if (touchTrack && !controller.drag)
        {
            float xrate = track.position.x - controller.train.transform.position.x;
            if (Mathf.Abs(xrate) <= 0.1)
            {
                xrate = 0;
            }
            else
            {
                //xrate = xrate / Mathf.Abs(xrate);
            }
            
            
            
            Vector2 further = new Vector2(xrate * XrotateScale, 0.5f);
            
            Vector3 vel = new Vector3(further.x, 0, further.y);
            controller.train.body.velocity = (vel.normalized + controller.train.body.velocity.normalized).normalized * controller.train.speed;
        }
    }

}
