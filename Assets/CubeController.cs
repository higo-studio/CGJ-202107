using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public LineRenderer line;
    public GameObject plane;
    public GameObject stakes;

    public bool drag = false;

    public Train train;
    public TMPro.TMP_Text speedIndicator;

    public AnimationClip showAimClip;
    public AnimationClip hideAimmClip;
    // LayerMask raycastLayer;
    Vector3 anchorPoint;
    bool enableAnchor;
    float radius;

    // Start is called before the first frame update
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (enableAnchor)
        {
            var dir = (anchorPoint - train.transform.position).normalized;
            // 角速度恒定
            // var omga = 1;
            // var fn = cube.mass * radius * Mathf.Pow(omga, 2);
            // cube.AddForce(fn * dir);
            // 切速度方向
            var dirQie = Vector3.Cross(dir, plane.transform.up);
            var speedQie = Vector3.Dot(train.body.velocity, dirQie);
            var fn = train.body.mass * speedQie * speedQie / radius;
            train.body.AddForce(fn * dir);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !enableAnchor)
        {

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
            {
                if (MyLayerMask.IsInMask(MyLayerMask.CanAttackMask, hit.transform.gameObject.layer))
                {
                    enableAnchor = true;
                    anchorPoint = hit.point + new Vector3(0, 0.5f, 0);
                    stakes.transform.position = anchorPoint;
                    stakes.GetComponentInChildren<Animation>().Play("show");
                    radius = (anchorPoint - train.transform.position).magnitude;
                }
            }
            drag = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            enableAnchor = false;
            line.enabled = false;
            stakes.GetComponentInChildren<Animation>().Play("hide");
            drag = false;
        }

        if (enableAnchor)
        {
            line.SetPositions(new Vector3[]
            {
                train.transform.position,
                anchorPoint
            });
            line.enabled = true;
            stakes.transform.forward = (train.transform.position - stakes.transform.position).normalized;
        }
        
        train.transform.forward = Vector3.ProjectOnPlane(train.body.velocity.normalized, plane.transform.up);
        if (speedIndicator)
        {
            speedIndicator.text = train.body.velocity.magnitude.ToString();
        }
    }
}
