//撞墙反射

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflexByCollision : MonoBehaviour
{
    public Rigidbody cube;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Reflective")
        {
            Vector3 normal = (cube.position - collision.transform.position).normalized;
            normal.y = 0;
            Vector3 refletion = Vector3.Reflect(cube.velocity, normal);
            refletion.y = 0;
            float cos = Vector3.Dot(-normal.normalized, cube.velocity.normalized);
            // 角度大于60  注意90度垂直
            if(cos < 0.154251449f)  
            {
                normal.x = Mathf.Abs(Mathf.Abs(normal.x) - 1);
                normal.z = Mathf.Abs(Mathf.Abs(normal.z) - 1);
                refletion.x *= normal.x;
                refletion.z *= refletion.z;
            }
            cube.velocity = refletion.normalized * 5;

        }
    }
}
