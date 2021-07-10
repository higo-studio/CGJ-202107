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
    [HideInInspector]
    public float speed;
    [Tooltip("减速度")]
    [Range(1, 1000)]
    public float passiveAcc = 40f;
    [Tooltip("加速度")]
    [Range(1, 1000)]
    public float positiveAcc = 100f;
    [HideInInspector]
    public Rigidbody body;
    [HideInInspector]
    public Collider ccollider;
    [Tooltip("最小速度")]
    public float minSpeed = 2f;

    private void Awake()
    {
        speed = initializedSpeed;
        body = GetComponent<Rigidbody>();
        ccollider = GetComponent<Collider>();
    }
    public LayerMask upperLayer;

    private void Start()
    {
        body.velocity = body.transform.forward * speed;
        foreach (var entry in layerMaxSpeeds)
        {
            layerMaxSpeedMap[entry.mask] = entry.maxSpeed;
        }
    }

    private void LayerTranspose(LayerMask from, LayerMask to)
    {
        // currentTransition = default;


        upperLayer = to;
    }

    private void FixedUpdate()
    {
        var ray = new Ray(transform.position + new Vector3(0, 1, 0), Vector3.down);
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, MyLayerMask.CanAttackMask, QueryTriggerInteraction.Collide))
        {
            LayerTranspose(upperLayer, 1 << hitInfo.transform.gameObject.layer);
        }

        if (upperLayer == MyLayerMask.NormalPlaneMask)
        {
            var maxSpeed = layerMaxSpeedMap[MyLayerMask.NormalPlaneMask];
            if (speed > maxSpeed)
            {
                speed -= passiveAcc * Time.fixedDeltaTime * Time.fixedDeltaTime;
            }
        }
        else if (upperLayer == MyLayerMask.RailRoadMask)
        {
            var maxSpeed = layerMaxSpeedMap[MyLayerMask.RailRoadMask];
            if (speed < maxSpeed)
            {
                speed += positiveAcc * Time.fixedDeltaTime * Time.fixedDeltaTime;
            }
        }
        if (speed < minSpeed)
        {
            speed = minSpeed;
        }
        body.velocity = body.velocity.normalized * speed;
    }
}