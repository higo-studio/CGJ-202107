using UnityEngine;
using System;

public class ResetBtn : MonoBehaviour
{
    public GameObject cube;
    public LineRenderer line;
    Vector3 initPos;

    public void Start()
    {
        initPos = cube.transform.position;
    }
    public void OnClick()
    {
        cube.GetComponent<TrailRenderer>()?.Clear();
        cube.transform.forward = Vector3.forward;
        cube.transform.position = initPos;
        var rigid = cube.GetComponent<Rigidbody>();
        rigid.velocity = cube.transform.forward * 5;
        line.enabled = false;
    }
}