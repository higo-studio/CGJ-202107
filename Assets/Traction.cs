using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traction : MonoBehaviour
{
    public CubeController controller;
    public float XrotateScale = 0f;
    public bool touchTrack = false;

    Transform track;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Track")
        {
            touchTrack = true;
            track = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Track")
        {
            Debug.Log("LEAVING !!!!!!!!!!");
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

            sin = sin >= 360 ? (sin - 360) : sin;
            sin = sin <= -360 ? (sin + 360) : sin;

            controller.train.body.velocity = Quaternion.Euler(0f, sin * 10, 0f) * controller.train.body.velocity;
        }
    }

}
