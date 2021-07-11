using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum DeathType
{
    Succeed, Drop, CannotBreath
}

public class CubeController : MonoBehaviour
{
    public bool isOver = false;
    public LineRenderer line;
    public GameObject plane;
    public GameObject stakes;

    public bool drag = false;

    public Train train;
    public TMPro.TMP_Text speedIndicator;

    public Image houshijing;
    public float houshijingDistance;
    // LayerMask raycastLayer;
    Vector3 anchorPoint;
    bool enableAnchor;
    float radius;

    GameObject sandStorm;
    bool isOpenHoushijing;
    public float DebugDistance;

    // Start is called before the first frame update

    // Update is called once per frame

    private void Awake() {
        sandStorm = GameObject.Find("Stage/Sandstorm");
    }
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

        if (!isOver && train.transform.position.y < -2f)
        {
            isOver = true;
            EventSystem.current.BroadcastMessage("GameOver", DeathType.Drop);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !enableAnchor)
        {

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, MyLayerMask.CanAttackMask))
            {
                if (Physics.Raycast(hit.point + new Vector3(0, 100, 0), Vector3.down, out var hitt, 110f))
                {
                    if (MyLayerMask.IsInMask(MyLayerMask.CanAttackMask, hitt.transform.gameObject.layer))
                    {
                        enableAnchor = true;
                        anchorPoint = hit.point + new Vector3(0, 0.5f, 0);
                        stakes.transform.position = anchorPoint;
                        stakes.GetComponentInChildren<Animation>().Play("show");
                        radius = (anchorPoint - train.transform.position).magnitude;
                    }
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
    
        if (houshijing != null)
        {
            var delta = transform.position - sandStorm.transform.position;
            var distance = Vector3.ProjectOnPlane(delta, Vector3.up).magnitude;
            DebugDistance = distance;

            if (!isOpenHoushijing && distance < houshijingDistance)
            {
                isOpenHoushijing = true;
                houshijing.gameObject.SetActive(true);
            }
        }
    }
}
