using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VansManager : MonoBehaviour
{
    public float turnTime = 0f;
    public int vansCount = 1;
    public float vanSpacing = 0.5f;
    public Rigidbody train;
    public Rigidbody vanPrefab;


    int intervalIndex = 0;

    //储存车厢
    List<Rigidbody> vans;

    //保存车头的速度信息
    List<Vector3> hisVel;
    //保持车头的位置信息
    List<Vector3> hisPos;

    private void Start()
    {
        //初始化车厢
        vans = new List<Rigidbody>();
        intervalIndex = (int)Mathf.Ceil(turnTime / 0.02f);

        hisVel = new List<Vector3>();
        hisPos = new List<Vector3>();

        int hisLen = intervalIndex * (vansCount + 1);

        for (int i = 0; i< hisLen; i++)
        {
            Vector3 vel = train.velocity;
            Vector3 pos = train.position;
            hisPos.Add(pos);
            hisVel.Add(vel);
        }

        Rigidbody preVan = train;
        for(int i = 0; i < vansCount; i++)
        {
            Rigidbody van = Instantiate(vanPrefab);
            van.transform.parent = transform;
            Vector3 pos = (-train.velocity.normalized) * vanSpacing + preVan.transform.position;
            van.transform.position = pos;
            van.transform.forward = preVan.velocity.normalized;
            preVan = van;
            vans.Add(van);
        }
    }


    void UpdateVansVelocity()
    {
        Vector3 vel = train.velocity;
        Vector3 pos = train.position;
        hisPos.Add(pos);
        hisVel.Add(vel);

        hisPos.RemoveAt(0);
        hisVel.RemoveAt(0);

        Rigidbody pre = train;
        float speed = train.GetComponent<Train>().speed;
        int tscle = (int)((vanSpacing / speed));
        for (int i = 0; i < vansCount; i++)
        {

            vans[i].transform.forward = hisVel[i * intervalIndex];
            vans[i].transform.position = hisPos[i * intervalIndex];

            /*
            Vector3 dir = hisPos[i * intervalIndex] - vans[i].position;
            vans[i].velocity = dir.normalized * speed;
            vans[i].transform.forward = vans[i].velocity;
            */
            
        }

    }

    float time = 0f;
    private void FixedUpdate()
    {
        UpdateVansVelocity();
    }
    


}
