using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("障碍物/可撞飞")]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class CanFlyObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    Collider _collider;
    Rigidbody _body;
    bool wantDestroy = false;
    public float delayToDestroy = 3f;
    float destroyAccumulator = 0f;
    void Start()
    {
        _collider = GetComponent<Collider>() ?? gameObject.AddComponent<BoxCollider>();
        _collider.isTrigger = true;

        _body = GetComponent<Rigidbody>() ?? gameObject.AddComponent<Rigidbody>();
        _body.useGravity = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == MyLayerMask.Character && !wantDestroy)
        {
            Debug.Log(this.name);
            var train = other.GetComponent<Train>();
            if (train != null)
            {
                var body = train.body;
                train.speed = train.speed / 3f * 2;
                _body.AddForce((body.velocity + new Vector3(0, 1, 0)) * 15, ForceMode.Impulse);
                _body.angularVelocity = new Vector3(100, 0, 0);
                _collider.enabled = false;
                wantDestroy = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (wantDestroy)
        {
            destroyAccumulator += Time.deltaTime;
            if (destroyAccumulator >= delayToDestroy)
            {
                Destroy(gameObject);
            }
        }
    }
}
