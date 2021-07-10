using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traction : MonoBehaviour
{
    public Rigidbody cube;
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
            Vector3 train2track = cube.transform.forward;
            float cos = Vector3.Dot(train2track.normalized, track.forward.normalized);
            float sin = Vector3.Cross(train2track.normalized, track.forward.normalized).magnitude;

            if (cos <= 0)
                return;

            cube.velocity = Quaternion.Euler(0, - sin * 10, 0) * cube.velocity;

        }
    }

}
