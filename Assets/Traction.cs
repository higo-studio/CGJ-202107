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
          

            Vector3 train2track = controller.train.transform.forward;
            float cos = Vector3.Dot(train2track.normalized, track.forward.normalized);
            Vector3 cross = Vector3.Cross(train2track.normalized, track.forward.normalized);
            float sin = cross.magnitude * (cross.y / Mathf.Abs(cross.y));  

            if (cos <= 0)
                return;

            controller.train.body.velocity = Quaternion.Euler(0, sin * 10, 0) * controller.train.body.velocity;
        }
    }

}
