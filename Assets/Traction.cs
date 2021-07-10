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
            float xrate = track.position.x - cube.transform.position.x;
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
            cube.velocity = (vel.normalized + cube.velocity.normalized).normalized * controller.Speed;
        }
    }

}
