using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public struct LayerMaxSpeed
{
    public LayerMask mask;
    public float maxSpeed;
}
public class Train : MonoBehaviour 
{
    public float initializedSpeed = 5;
    public LayerMaxSpeed[] layerMaxSpeeds;
    Dictionary<int, float> layerMaxSpeedMap = new Dictionary<int, float>();
    public float speed;

    public Rigidbody body;
    public Collider ccollider;
    public float acceleration;
    private void Awake() {
        speed = initializedSpeed;
        body = GetComponent<Rigidbody>();
        ccollider = GetComponent<Collider>();
    }
    public LayerMask upperLayer;

    private void Start() {
        body.velocity = body.transform.forward * speed;
        foreach(var entry in layerMaxSpeeds)
        {
            layerMaxSpeedMap[entry.mask] = entry.maxSpeed;
        }
    }

    private void FixedUpdate() {
        speed += acceleration * Time.fixedDeltaTime * Time.fixedDeltaTime;
        body.velocity = body.velocity.normalized * speed;
        var ray = new Ray(transform.position + new Vector3(0, 1, 0), Vector3.down);
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, MyLayerMask.CanAttackMask, QueryTriggerInteraction.Collide))
        {
            upperLayer = (1 << hitInfo.transform.gameObject.layer);
        }

        var maxSpeed = layerMaxSpeedMap[upperLayer];
        speed = Mathf.Min(maxSpeed, speed);
    }
}