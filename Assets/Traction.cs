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

            //old
            /*
             
             */

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

            /*
            Vector3 train2track = controller.train.transform.forward;
            float cos = Vector3.Dot(train2track.normalized, track.forward.normalized);
            Vector3 cross = Vector3.Cross(train2track.normalized, track.forward.normalized);
            float sin = cross.magnitude * (cross.y / Mathf.Abs(cross.y));  

            if (cos <= 0)
                return;

            sin = sin >= 360 ? (sin - 360) : sin;
            sin = sin <= -360 ? (sin + 360) : sin;

            controller.train.body.velocity = Quaternion.Euler(0f, sin * 10, 0f) * controller.train.body.velocity;
            */
        }
    }

}
