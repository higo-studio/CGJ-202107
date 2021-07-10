﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public Rigidbody cube;
    public LineRenderer line;
    public GameObject plane;
    public GameObject stakes;

    public float Speed = 5;
    public TMPro.TMP_Text speedIndicator;

    // LayerMask raycastLayer;
    Vector3 anchorPoint;
    bool enableAnchor;
    float radius;

    // Start is called before the first frame update
    void Start()
    {
        cube.velocity = cube.transform.forward * Speed;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (enableAnchor)
        {
            var dir = (anchorPoint - cube.transform.position).normalized;
            // 角速度恒定
            // var omga = 1;
            // var fn = cube.mass * radius * Mathf.Pow(omga, 2);
            // cube.AddForce(fn * dir);
            // 切速度方向
            var dirQie = Vector3.Cross(dir, plane.transform.up);
            var speedQie = Vector3.Dot(cube.velocity, dirQie);
            var fn = cube.mass * speedQie * speedQie / radius;
            cube.AddForce(fn * dir);
        }

        cube.velocity = cube.velocity.normalized * Speed;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !enableAnchor)
        {

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject.layer == MyLayerMask.RaycasatBase)
                {
                    enableAnchor = true;
                    anchorPoint = hit.point + new Vector3(0, 0.5f, 0);
                    stakes.transform.position = anchorPoint;
                    radius = (anchorPoint - cube.transform.position).magnitude;
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            enableAnchor = false;
            line.enabled = false;
        }

        if (enableAnchor)
        {
            line.SetPositions(new Vector3[]
            {
                cube.transform.position,
                anchorPoint
            });
            line.enabled = true;
        }
        
        cube.transform.forward = Vector3.ProjectOnPlane(cube.velocity.normalized, plane.transform.up);
        speedIndicator.text = cube.velocity.magnitude.ToString();
    }
}
